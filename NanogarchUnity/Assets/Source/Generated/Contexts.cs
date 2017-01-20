//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ContextsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {

    public partial class Contexts {

        public static Context CreateCoreContext() {
            return CreateContext("Core", CoreComponentIds.TotalComponents, CoreComponentIds.componentNames, CoreComponentIds.componentTypes);
        }

        public static Context CreateInputContext() {
            return CreateContext("Input", InputComponentIds.TotalComponents, InputComponentIds.componentNames, InputComponentIds.componentTypes);
        }

        public Context[] allContexts { get { return new [] { core, input }; } }

        public Context core;
        public Context input;

        public void SetAllContexts() {
            core = CreateCoreContext();
            input = CreateInputContext();
        }
    }
}
