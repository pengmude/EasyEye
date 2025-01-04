using System;
using System.Reflection;
using System.Reflection.Emit;

namespace SmartLib
{
    internal static class TypeCreationHelper
    {
        private static ModuleBuilder _moduleBuilder;

        internal static ModuleBuilder GetModuleBuilder()
        {
            // todo: think about multithread
            if (_moduleBuilder == null)
            {
                var aName = new AssemblyName("DeepClonerCode");
                var ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
                var mb = ab.DefineDynamicModule(aName.Name);
                _moduleBuilder = mb;
            }

            return _moduleBuilder;
        }
    }
}