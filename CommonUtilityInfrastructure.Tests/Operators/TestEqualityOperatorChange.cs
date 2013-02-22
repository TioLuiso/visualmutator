﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualMutator.OperatorTests
{
    using System.IO;
    using CommonUtilityInfrastructure;
    using Extensibility;
    using Microsoft.Cci;
    using Microsoft.Cci.MutableCodeModel;
    using Model;
    using Model.CodeDifference;
    using Model.Exceptions;
    using Model.Mutations;
    using Model.Mutations.Structure;
    using Model.Mutations.Types;
    using Mono.Cecil;
    using NUnit.Framework;
    using OperatorsStandard;
    using Roslyn.Compilers;
    using Roslyn.Compilers.CSharp;
    using Tests.Util;

    using VisualMutator.OperatorsObject.Operators;

    using Decompiler = Microsoft.Cci.ILToCodeModel.Decompiler;


    [TestFixture]
    public class TestEqualityOperatorChange
    {
        [SetUp]
        public void Setup()
        {
            log4net.Config.BasicConfigurator.Configure(
              new log4net.Appender.ConsoleAppender
              {
                  Layout = new log4net.Layout.SimpleLayout()
              });
        }


        [Test]
        public void NormalEqualityIsMutated()
        {

            const string code = 
@"using System;
namespace Ns
{
    public class Test
    {
        public bool Method1(Test test)
        {
            return test == new Test();
        }
        public override bool Equals(object obj)
        {
            return true;
        }
    }
}";

            List<Mutant> mutants;
            AssembliesProvider original;
            CodeDifferenceCreator diff;
            Common.RunMutations(code, new EqualityOperatorChange(), out mutants, out original, out diff);

            foreach (var mutant in mutants)
            {
                var codeWithDifference = diff.CreateDifferenceListing(CodeLanguage.CSharp, mutant, original);
                Console.WriteLine(codeWithDifference.Code);
                Assert.AreEqual(codeWithDifference.LineChanges.Count, 2);
            }

            Assert.AreEqual(mutants.Count, 2);
        }


        [Test]
        public void NormalEqualsIsMutated()
        {

            const string code = 
@"using System;
namespace Ns
{
    public class Test
    {
        public bool Method1(Test test)
        {
            return test.Equals(test);
        }
        public override bool Equals(object obj)
        {
            return false;
        }
    }
}";

            List<Mutant> mutants;
            AssembliesProvider original;
            CodeDifferenceCreator diff;
            Common.RunMutations(code, new EqualityOperatorChange(), out mutants, out original, out diff);

            foreach (var mutant in mutants)
            {
                var codeWithDifference = diff.CreateDifferenceListing(CodeLanguage.CSharp, mutant, original);
                Console.WriteLine(codeWithDifference.Code);
                Assert.AreEqual(codeWithDifference.LineChanges.Count, 2);
            }

            Assert.IsTrue(mutants.Count == 1);
        }



        [Test]
        public void InvalidEqualityIsNotMutated()
        {

            const string code =
@"using System;
namespace Ns
{
    public class Test
    {
        public bool Method1(Test test)
        {
            return 3 == 4;
        }
        public override bool Equals(object obj)
        {
            return true;
        }
    }
}";
            
            List<Mutant> mutants;
            AssembliesProvider original;
            CodeDifferenceCreator diff;
            Common.RunMutations(code, new EqualityOperatorChange(), out mutants, out original, out diff);

            Assert.AreEqual(mutants.Count, 0);

        
        }


        [Test]
        public void Equality_Is_Not_Mutated_When_Equals_Not_Overriten()
        {

            const string code =
@"using System;
namespace Ns
{
    public class Test
    {
        public bool Method1(Test test)
        {
            return new Test() == test;
        }

    }
}";

            List<Mutant> mutants;
            AssembliesProvider original;
            CodeDifferenceCreator diff;
            Common.RunMutations(code, new EqualityOperatorChange(), out mutants, out original, out diff);

            Assert.AreEqual(mutants.Count, 0);

        }

        [Test]
        public void Equals_Is_Not_Mutated_When_Equals_Not_Overriten()
        {

            const string code =
@"using System;
namespace Ns
{
    public class Test
    {
        public bool Method1(Test test)
        {
            return new Test().Equals(test);
        }

    }
}";

            List<Mutant> mutants;
            AssembliesProvider original;
            CodeDifferenceCreator diff;
            Common.RunMutations(code, new EqualityOperatorChange(), out mutants, out original, out diff);

            Assert.AreEqual(mutants.Count, 0);

          
        }


        [Test]
        public void Equality_Is_Not_Mutated_When_One_Type_Does_Not_Have_Equals()
        {

            const string code =
@"using System;
namespace Ns
{
    public class TestBase
    {
    }
    public class Test : TestBase
    {
        public bool Method1(Test test)
        {
            return new TestBase() == test;
        }
        public override bool Equals(object obj)
        {
            return true;
        }
    }
}";

            List<Mutant> mutants;
            AssembliesProvider original;
            CodeDifferenceCreator diff;
            Common.RunMutations(code, new EqualityOperatorChange(), out mutants, out original, out diff);

            Assert.AreEqual(mutants.Count, 1);
            Assert.AreEqual(mutants[0].MutationTarget.PassInfo, "Right");
            foreach (var mutant in mutants)
            {
                var codeWithDifference = diff.CreateDifferenceListing(CodeLanguage.CSharp, mutant, original);
                Console.WriteLine(codeWithDifference.Code);
                Assert.AreEqual(codeWithDifference.LineChanges.Count, 2);
            }
        }

    }
}
