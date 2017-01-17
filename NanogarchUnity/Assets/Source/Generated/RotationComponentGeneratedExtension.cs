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

        public RotationComponent rotation { get { return (RotationComponent)GetComponent(CoreComponentIds.Rotation); } }
        public bool hasRotation { get { return HasComponent(CoreComponentIds.Rotation); } }

        public Entity AddRotation(float newXDegrees, float newYDegrees) {
            var component = CreateComponent<RotationComponent>(CoreComponentIds.Rotation);
            component.xDegrees = newXDegrees;
            component.yDegrees = newYDegrees;
            return AddComponent(CoreComponentIds.Rotation, component);
        }

        public Entity ReplaceRotation(float newXDegrees, float newYDegrees) {
            var component = CreateComponent<RotationComponent>(CoreComponentIds.Rotation);
            component.xDegrees = newXDegrees;
            component.yDegrees = newYDegrees;
            ReplaceComponent(CoreComponentIds.Rotation, component);
            return this;
        }

        public Entity RemoveRotation() {
            return RemoveComponent(CoreComponentIds.Rotation);
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherRotation;

        public static IMatcher Rotation {
            get {
                if(_matcherRotation == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Rotation);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherRotation = matcher;
                }

                return _matcherRotation;
            }
        }
    }