﻿namespace VisualMutator.Model.Mutations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using CommonUtilityInfrastructure;

    using ICSharpCode.Decompiler;

    using Mono.Cecil;

    using VisualMutator.Extensibility;
    using VisualMutator.Model.Mutations.Structure;
    public enum CodeLanguage
    {
        CSharp,
        IL
    }

    public interface ICodeVisualizer
    {


        CodePair CreateCodesToCompare(MutationTarget target, AssembliesProvider originalAssemblies,
            AssembliesProvider mutatedAssemblies);
    }

    public class CodeVisualizer : ICodeVisualizer
    {
        private IDecompiler _decompiler;

        public CodeVisualizer(CodeLanguage language)
        {
            _decompiler = new Decompiler(language);
        }



        public string Visualize(MutationTarget target, AssembliesProvider assemblies)
        {
            var sb = new StringBuilder();
         /*   foreach (IMutationElement mutationElement in target.RetrieveNonHidden())
            {


                var output = Switch.Into<string>().FromTypeOf(mutationElement)
                    .Case<MutationElementMethod>(elem => _decompiler.DecompileMethod(elem.FindIn(assemblies)))
                    .Case<MutationElementType>(elem => _decompiler.DecompileType(elem.FindIn(assemblies)))
                    .Case<MutationElementProperty>(elem => _decompiler.DecompileProperty(elem.FindIn(assemblies)))
                    .Case<MutationElementField>(elem => _decompiler.DecompileField(elem.FindIn(assemblies)))
                    .GetResult();

                sb.Append(output);
            }*/
            return sb.ToString();
        }
        public CodePair CreateCodesToCompare(MutationTarget target,
            AssembliesProvider originalAssemblies, AssembliesProvider mutatedAssemblies)
        {
            return new CodePair
            {
                OriginalCode = Visualize(target, originalAssemblies),
                MutatedCode = Visualize(target, mutatedAssemblies),
            };


        }
    }
 
}