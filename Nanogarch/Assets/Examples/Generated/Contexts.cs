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

        public static Context CreateBlueprintsContext() {
            return CreateContext("Blueprints", BlueprintsComponentIds.TotalComponents, BlueprintsComponentIds.componentNames, BlueprintsComponentIds.componentTypes);
        }

        public static Context CreateVisualDebuggingContext() {
            return CreateContext("VisualDebugging", VisualDebuggingComponentIds.TotalComponents, VisualDebuggingComponentIds.componentNames, VisualDebuggingComponentIds.componentTypes);
        }

        public Context[] allContexts { get { return new [] { blueprints, visualDebugging }; } }

        public Context blueprints;
        public Context visualDebugging;

        public void SetAllContexts() {
            blueprints = CreateBlueprintsContext();
            visualDebugging = CreateVisualDebuggingContext();
        }
    }
}
