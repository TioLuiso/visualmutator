﻿using System.Drawing;
namespace VisualMutator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using CommonUtilityInfrastructure;
    using CommonUtilityInfrastructure.DependencyInjection;
    using CommonUtilityInfrastructure.WpfUtils;

    using VisualMutator.Infrastructure;
    using VisualMutator.Model;
    using VisualMutator.Model.Mutations;
    using VisualMutator.Model.Mutations.Structure;
    using VisualMutator.Model.Mutations.Types;
    using VisualMutator.Model.Tests;
    using VisualMutator.Model.Tests.TestsTree;
    using VisualMutator.ViewModels;

    using log4net;

    using Switch = CommonUtilityInfrastructure.Switch;

    public class MainController : Controller
    {
        private ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly MainViewModel _viewModel;

        private readonly IFactory<SessionCreationController> _mutantsCreationFactory;

        private readonly IFactory<OnlyMutantsCreationController> _onlyMutantsCreationFactory;

        private readonly IFactory<SessionController> _sessionControllerFactory;

        private readonly IFactory<ResultsSavingController> _resultsSavingFactory;

        private readonly MutantDetailsController _mutantDetailsController;

        private readonly CommonServices _svc;

        private SessionController _sessionController;

        private List<IDisposable> _subscriptions; 


        public MainController(
            MainViewModel viewModel,
            IFactory<SessionCreationController> mutantsCreationFactory,
            IFactory<OnlyMutantsCreationController> onlyMutantsCreationFactory,
            IFactory<SessionController> sessionControllerFactory,
            IFactory<ResultsSavingController> resultsSavingFactory,
            
            MutantDetailsController mutantDetailsController,
            CommonServices svc)
        {
            _viewModel = viewModel;
            _mutantsCreationFactory = mutantsCreationFactory;
            _onlyMutantsCreationFactory = onlyMutantsCreationFactory;
            _sessionControllerFactory = sessionControllerFactory;
            _resultsSavingFactory = resultsSavingFactory;
            _mutantDetailsController = mutantDetailsController;
            _svc = svc;


            _viewModel.CommandCreateNewMutants = new BasicCommand(RunMutationSession,
                () => _viewModel.OperationsState.IsIn(OperationsState.None, OperationsState.Finished, OperationsState.Error))
                .UpdateOnChanged(_viewModel, () => _viewModel.OperationsState);


            _viewModel.CommandOnlyCreateMutants = new BasicCommand(OnlyCreateMutants,
                () => _viewModel.OperationsState.IsIn(OperationsState.None, OperationsState.Finished, OperationsState.Error))
                .UpdateOnChanged(_viewModel, () => _viewModel.OperationsState);


            _viewModel.CommandPause = new BasicCommand(PauseOperations, 
                () => _viewModel.OperationsState.IsIn(OperationsState.Testing))
                .UpdateOnChanged(_viewModel, () => _viewModel.OperationsState);

            _viewModel.CommandStop = new BasicCommand(StopOperations,
                () => _viewModel.OperationsState.IsIn(OperationsState.Mutating, OperationsState.PreCheck,
                    OperationsState.Testing, OperationsState.TestingPaused, OperationsState.Pausing))
                .UpdateOnChanged(_viewModel, () => _viewModel.OperationsState);

            _viewModel.CommandContinue = new BasicCommand(ResumeOperations,
                () => _viewModel.OperationsState == OperationsState.TestingPaused)
                .UpdateOnChanged(_viewModel, () => _viewModel.OperationsState);


            _viewModel.CommandSaveResults = new BasicCommand(SaveResults, () =>
                _viewModel.OperationsState == OperationsState.Finished)
                .UpdateOnChanged(_viewModel, () => _viewModel.OperationsState);


            _viewModel.RegisterPropertyChanged(vm => vm.SelectedMutationTreeItem).OfType<Mutant>()
                .Subscribe(mutant => _mutantDetailsController.LoadDetails(mutant, 
                    _sessionController.Session.OriginalAssemblies));

            _viewModel.MutantDetailsViewModel = _mutantDetailsController.ViewModel;

        }

   
        private void SetState(OperationsState state)
        {
            _svc.Threading.InvokeOnGui(() =>
            {
                if (_viewModel.OperationsState != state)
                {
                    _viewModel.OperationsState = state;
                    _viewModel.OperationsStateDescription = Functional.ValuedSwitch<OperationsState, string>(state)
                        .Case(OperationsState.None, "")
                        .Case(OperationsState.TestingPaused, "Paused")
                        .Case(OperationsState.Finished, "Finished")
                        .Case(OperationsState.PreCheck, "Pre-check...")
                        .Case(OperationsState.Mutating, "Creating mutants...")
                        .Case(OperationsState.Pausing, "Pausing...")
                        .Case(OperationsState.Stopping, "Stopping...")
                        .Case(OperationsState.SavingMutants, "Saving mutants...")
                        .Case(OperationsState.Error, "Error occurred.")
                        .GetResult();
                }
            });
        }


       
        public void Subscribe(SessionController sessionController)
        {
            _subscriptions = new List<IDisposable>
            {
                sessionController.SessionEventsObservable
                    .OfType<MinorSessionUpdateEventArgs>()
                    .Subscribe(args =>
                    {
                        SetState(args.EventType);
                        Switch.On(args.ProgressUpdateMode)
                            .Case(ProgressUpdateMode.SetValue, () =>
                            {
                                _viewModel.IsProgressIndeterminate = false;
                                _viewModel.Progress = args.PercentCompleted;
                            })
                            .Case(ProgressUpdateMode.Indeterminate, () =>
                            {
                                _viewModel.IsProgressIndeterminate = true;
                            })
                            .Case(ProgressUpdateMode.PreserveValue, ()=>
                            {
                               
                            });
                        
                    }),

                sessionController.SessionEventsObservable
                 .OfType<MutationFinishedEventArgs>()
                 .Subscribe(args => _viewModel.Operators.ReplaceRange(args.MutantsGroupedByOperators)),

                sessionController.SessionEventsObservable
                 .OfType<TestingProgressEventArgs>()
                 .Subscribe(args =>
                 {
                     _svc.Threading.InvokeOnGui(()=>
                     {
                         _viewModel.OperationsState = OperationsState.Testing;
                         _viewModel.OperationsStateDescription = "Running tests... ({0}/{1})"
                             .Formatted(args.NumberOfAllMutantsTested + 1,
                                 args.NumberOfAllMutants);

                         _viewModel.MutantsRatio = string.Format("Mutants killed: {0}/{1}", args.NumberOfMutantsKilled, args.NumberOfAllMutantsTested);
                         _viewModel.MutationScore = string.Format(@"Mutation score: {0}%", args.MutationScore.AsPercentageOf(1.0d));
                         _viewModel.Progress = args.NumberOfAllMutantsTested.AsPercentageOf(args.NumberOfAllMutants);
                     });
                 }),

               
            };
          
        }

        public void RunMutationSession()
        {
            var mutantsCreationController = _mutantsCreationFactory.Create();
             mutantsCreationController.Run();
            if (mutantsCreationController.HasResults)
            {
                MutationSessionChoices choices = mutantsCreationController.Result;

                Clean();

                _sessionController = _sessionControllerFactory.Create();
                Subscribe(_sessionController);
                _sessionController.RunMutationSession(choices);
            }
        }

        public void OnlyCreateMutants()
        {
            var onlyMutantsController = _onlyMutantsCreationFactory.Create();
            onlyMutantsController.Run();

            if (onlyMutantsController.HasResults)
            {
                MutationSessionChoices choices = onlyMutantsController.Result;

                Clean();

                _sessionController = _sessionControllerFactory.Create();
                Subscribe(_sessionController);
                _sessionController.OnlyCreateMutants(choices);
            }
        }

        public void PauseOperations()
        {
            SetState(OperationsState.Pausing);
            _sessionController.PauseOperations();
        }
        public void ResumeOperations()
        {
           _sessionController.ResumeOperations();
        }

        public void StopOperations()
        {
        
            _sessionController.StopOperations();

        }
        public void SaveResults()
        {
            var resultsSavingController = _resultsSavingFactory.Create();
            resultsSavingController.Run(_sessionController.Session);
        }

        public void Initialize()
        {
            _viewModel.IsVisible = true;
        }

        public void Deactivate()
        {
            if (_sessionController != null)
            {
                _sessionController.StopOperations();
            }
            Clean();
            _viewModel.IsVisible = false;
        }

        private void Clean()
        {
            _viewModel.Clean();

            _mutantDetailsController.Clean();
            if (_subscriptions!=null)
            {
                foreach (var subscription in _subscriptions)
                {
                    subscription.Dispose();
                }
            }
            _sessionController = null;
            SetState(OperationsState.None);
        }

        public MainViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
        }
    }
}