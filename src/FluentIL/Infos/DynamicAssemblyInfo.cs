using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace FluentIL.Infos
{
    public class DynamicAssemblyInfo
    {
        private readonly string _assemblyFileName;
        private readonly ModuleBuilder _moduleBuilder;
        private readonly AssemblyBuilder _assemblyBuilder;
        private readonly List<DynamicTypeInfo> _types = new List<DynamicTypeInfo>();

        public DynamicAssemblyInfo(string assemblyFileName)
        {
            _assemblyFileName = assemblyFileName;
            var assemblyName = new AssemblyName(
                assemblyFileName
                );

            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                assemblyName,
                AssemblyBuilderAccess.Run
                );

            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(
                _assemblyBuilder.GetName().Name);
        }

        public DynamicTypeInfo WithType(string typeName)
        {
            var result = new DynamicTypeInfo(typeName, _moduleBuilder);
            _types.Add(result);
            return result;

        }

        public void SetEntryPoint(DynamicMethodInfo method)
        {
            throw new NotImplementedException();
            /*
            AssemblyBuilder.SetEntryPoint(method.MethodBuilder);
             */
        }

        public void Save()
        {
            throw new NotImplementedException();
            /*
            _types.ForEach(t => t.Complete());
            AssemblyBuilder.Save(_assemblyFileName);
             */
        }
    }
}
