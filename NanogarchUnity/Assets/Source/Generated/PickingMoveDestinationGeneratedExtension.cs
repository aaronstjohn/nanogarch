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

        static readonly PickingMoveDestination pickingMoveDestinationComponent = new PickingMoveDestination();

        public bool isPickingMoveDestination {
            get { return HasComponent(CoreComponentIds.PickingMoveDestination); }
            set {
                if(value != isPickingMoveDestination) {
                    if(value) {
                        AddComponent(CoreComponentIds.PickingMoveDestination, pickingMoveDestinationComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.PickingMoveDestination);
                    }
                }
            }
        }

        public Entity IsPickingMoveDestination(bool value) {
            isPickingMoveDestination = value;
            return this;
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherPickingMoveDestination;

        public static IMatcher PickingMoveDestination {
            get {
                if(_matcherPickingMoveDestination == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.PickingMoveDestination);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherPickingMoveDestination = matcher;
                }

                return _matcherPickingMoveDestination;
            }
        }
    }
