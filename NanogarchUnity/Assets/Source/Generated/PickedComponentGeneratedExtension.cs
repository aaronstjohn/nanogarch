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

        static readonly PickedComponent pickedComponent = new PickedComponent();

        public bool isPicked {
            get { return HasComponent(CoreComponentIds.Picked); }
            set {
                if(value != isPicked) {
                    if(value) {
                        AddComponent(CoreComponentIds.Picked, pickedComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.Picked);
                    }
                }
            }
        }

        public Entity IsPicked(bool value) {
            isPicked = value;
            return this;
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherPicked;

        public static IMatcher Picked {
            get {
                if(_matcherPicked == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Picked);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherPicked = matcher;
                }

                return _matcherPicked;
            }
        }
    }
