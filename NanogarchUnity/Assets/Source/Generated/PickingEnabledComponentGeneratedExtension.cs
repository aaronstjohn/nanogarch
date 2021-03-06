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

        static readonly PickingEnabledComponent pickingEnabledComponent = new PickingEnabledComponent();

        public bool isPickingEnabled {
            get { return HasComponent(InputComponentIds.PickingEnabled); }
            set {
                if(value != isPickingEnabled) {
                    if(value) {
                        AddComponent(InputComponentIds.PickingEnabled, pickingEnabledComponent);
                    } else {
                        RemoveComponent(InputComponentIds.PickingEnabled);
                    }
                }
            }
        }

        public Entity IsPickingEnabled(bool value) {
            isPickingEnabled = value;
            return this;
        }
    }

    public partial class Context {

        public Entity pickingEnabledEntity { get { return GetGroup(InputMatcher.PickingEnabled).GetSingleEntity(); } }

        public bool isPickingEnabled {
            get { return pickingEnabledEntity != null; }
            set {
                var entity = pickingEnabledEntity;
                if(value != (entity != null)) {
                    if(value) {
                        CreateEntity().isPickingEnabled = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class InputMatcher {

        static IMatcher _matcherPickingEnabled;

        public static IMatcher PickingEnabled {
            get {
                if(_matcherPickingEnabled == null) {
                    var matcher = (Matcher)Matcher.AllOf(InputComponentIds.PickingEnabled);
                    matcher.componentNames = InputComponentIds.componentNames;
                    _matcherPickingEnabled = matcher;
                }

                return _matcherPickingEnabled;
            }
        }
    }
