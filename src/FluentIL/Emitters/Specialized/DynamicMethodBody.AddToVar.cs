﻿// ReSharper disable CheckNamespace

using FluentIL.Numbers;

namespace FluentIL.Emitters
// ReSharper restore CheckNamespace
{
    partial class DynamicMethodBody
    {
        public DynamicMethodBody AddToVar(string varname, Number constant)
        {
            return Ldloc(varname)
                .Add(constant)
                .Stloc(varname);
        }

        public DynamicMethodBody AddToVar(string varname)
        {
            return Ldloc(varname)
                .Add()
                .Stloc(varname);
        }

        public DynamicMethodBody Inc(string varname)
        {
            return AddToVar(varname, 1);
        }
    }
}