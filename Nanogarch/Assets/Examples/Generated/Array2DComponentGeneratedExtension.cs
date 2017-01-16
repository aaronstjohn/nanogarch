//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Entitas;

namespace Entitas {

    public partial class Entity {

        public Array2DComponent array2D { get { return (Array2DComponent)GetComponent(VisualDebuggingComponentIds.Array2D); } }
        public bool hasArray2D { get { return HasComponent(VisualDebuggingComponentIds.Array2D); } }

        public void AddArray2D(string[,] newArray2d) {
            var component = CreateComponent<Array2DComponent>(VisualDebuggingComponentIds.Array2D);
            component.array2d = newArray2d;
            AddComponent(VisualDebuggingComponentIds.Array2D, component);
        }

        public void ReplaceArray2D(string[,] newArray2d) {
            var component = CreateComponent<Array2DComponent>(VisualDebuggingComponentIds.Array2D);
            component.array2d = newArray2d;
            ReplaceComponent(VisualDebuggingComponentIds.Array2D, component);
        }

        public void RemoveArray2D() {
            RemoveComponent(VisualDebuggingComponentIds.Array2D);
        }
    }
}

    public partial class VisualDebuggingMatcher {

        static IMatcher _matcherArray2D;

        public static IMatcher Array2D {
            get {
                if(_matcherArray2D == null) {
                    var matcher = (Matcher)Matcher.AllOf(VisualDebuggingComponentIds.Array2D);
                    matcher.componentNames = VisualDebuggingComponentIds.componentNames;
                    _matcherArray2D = matcher;
                }

                return _matcherArray2D;
            }
        }
    }
