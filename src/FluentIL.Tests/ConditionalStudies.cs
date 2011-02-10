﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using SharpTestsEx;
using System.Reflection.Emit;

namespace FluentIL.Tests
{
    [TestFixture]
    public class ConditionalStudies
    {
        [Test]
        public void TwoNumbersAreEquals_TrueReturnsYesFalseReturnsNo_Reference()
        {
            var dm = IL.NewMethod(typeof(string), typeof(int), typeof(int))
                .Ldarg(0, 1)
                .Beq("if_true")
                .Ldstr("No")
                .Br("done")
                .MarkLabel("if_true")
                .Ldstr("Yes")
                .MarkLabel("done")
                .Ret();

            dm.Invoke(2, 2).Should().Be("Yes");
            dm.Invoke(2, 3).Should().Be("No");
        }

        [Test]
        public void TwoNumbersAreEquals_TrueReturnsYesFalseReturnsNo_Reference2()
        {
            var dm = IL.NewMethod(typeof(string), typeof(int), typeof(int))
                .Ldarg(0, 1)
                .Ceq()
                .Brfalse("if_false")
                .Ldstr("Yes")
                .Br("done")
                .MarkLabel("if_false")
                .Ldstr("No")
                .MarkLabel("done")
                .Ret();

            dm.Invoke(2, 2).Should().Be("Yes");
            dm.Invoke(2, 3).Should().Be("No");
        }

        [Test]
        public void TwoNumbersAreEquals_TrueReturnsYesFalseReturnsNo()
        {
            var dm = IL.NewMethod(typeof(string), typeof(int), typeof(int))
                .Ldarg(0, 1)
                .Ifeq()
                    .Ldstr("Yes")
                .Else()
                    .Ldstr("No")
                .EndIf()
                .Ret();

            dm.Invoke(2, 2).Should().Be("Yes");
            dm.Invoke(2, 3).Should().Be("No");
        }

        [Test]
        public void EnsureLimits_Min10Max20_Reference()
        {

            var dm = IL.NewMethod()
                .WithParameter(typeof(int), "value")
                .WithParameter(typeof(int), "min")
                .WithParameter(typeof(int), "max")
                .WithVariable(typeof(int), "result")
                .Returns(typeof(int))
                .Ldarg("value")
                .Ldarg("min")
                .Iflt()
                    .Ldarg("min")
                .Else()
                    .Ldarg("value")
                .EndIf()
                .Stloc("result")
                .Ldloc("result")
                .Ldarg("max")
                .Ifgt()
                    .Ldarg("max")
                .Else()
                    .Ldloc("result")
                .EndIf()
                .Ret();
            dm.Invoke(5,10,20).Should().Be(10);
            dm.Invoke(21, 10, 20).Should().Be(20);
            dm.Invoke(13, 10, 20).Should().Be(13);
        }

        [Test]
        public void EnsureLimits_Min10Max20_Reference2()
        {

            var dm = IL.NewMethod()
                .WithParameter(typeof(int), "value")
                .WithParameter(typeof(int), "min")
                .WithParameter(typeof(int), "max")
                .Returns(typeof(int))
                .Ldarg("value")
                .Ldarg("min")
                .Iflt()
                    .Ldarg("min")
                .Else()
                    .Ldarg("value")
                    .Ldarg("max")
                    .Ifgt()
                        .Ldarg("max")
                    .Else()
                        .Ldarg("value")
                    .EndIf()
                .EndIf()
                .Ret();
            dm.Invoke(5, 10, 20).Should().Be(10);
            dm.Invoke(21, 10, 20).Should().Be(20);
            dm.Invoke(13, 10, 20).Should().Be(13);
        }


        [Test]
        public void EnsureLimits_Min10Max20_Reference3()
        {

            var dm = IL.NewMethod()
                .WithParameter(typeof(int), "value")
                .Returns(typeof(int))
                .Ldarg("value")
                .LdcI4(10)
                .Iflt()
                    .LdcI4(10)
                .Else()
                    .Ldarg("value")
                    .LdcI4(20)
                    .Ifgt()
                        .LdcI4(20)
                    .Else()
                        .Ldarg("value")
                    .EndIf()
                .EndIf()
                .Ret();
            dm.Invoke(5).Should().Be(10);
            dm.Invoke(21).Should().Be(20);
            dm.Invoke(13).Should().Be(13);
        }

        [Test]
        public void EnsureLimits_Min10Max20_Reference4()
        {

            var dm = IL.NewMethod()
                .WithParameter(typeof(int), "value")
                .Returns(typeof(int))
                .Ldarg("value")
                .Dup()
                .LdcI4(10)
                .Iflt()
                    .Pop()
                    .LdcI4(10)
                .Else()
                    .Dup()
                    .LdcI4(20)
                    .Ifgt()
                        .Pop()
                        .LdcI4(20)
                    .EndIf()
                .EndIf()
                .Ret();
            dm.Invoke(5).Should().Be(10);
            dm.Invoke(21).Should().Be(20);
            dm.Invoke(13).Should().Be(13);
        }

        [Test]
        public void EnsureLimits_Min10Max20_Reference5()
        {

            var dm = IL.NewMethod()
                .WithParameter(typeof(int), "value")
                .Returns(typeof(int))
                .Ldarg("value")
                .EnsureLimits(10, 20)
                .Ret();
            dm.Invoke(5).Should().Be(10);
            dm.Invoke(21).Should().Be(20);
            dm.Invoke(13).Should().Be(13);
        }
    }
}
