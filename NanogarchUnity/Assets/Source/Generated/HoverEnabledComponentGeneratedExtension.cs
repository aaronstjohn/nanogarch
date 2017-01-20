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

        static readonly HoverEnabledComponent hoverEnabledComponent = new HoverEnabledComponent();

        public bool isHoverEnabled {
            get { return HasComponent(InputComponentIds.HoverEnabled); }
            set {
                if(value != isHoverEnabled) {
                    if(value) {
                        AddComponent(InputComponentIds.HoverEnabled, hoverEnabledComponent);
                    } else {
                        RemoveComponent(InputComponentIds.HoverEnabled);
                    }
                }
            }
        }

        public Entity IsHoverEnabled(bool value) {
            isHoverEnabled = value;
            return this;
        }
    }

    public partial class Context {

        public Entity hoverEnabledEntity { get { return GetGroup(InputMatcher.HoverEnabled).GetSingleEntity(); } }

        public bool isHoverEnabled {
            get { return hoverEnabledEntity != null; }
            set {
                var entity = hoverEnabledEntity;
                if(value != (entity != null)) {
                    if(value) {
                        CreateEntity().isHoverEnabled = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class InputMatcher {

        static IMatcher _matcherHoverEnabled;

        public static IMatcher HoverEnabled {
            get {
                if(_matcherHoverEnabled == null) {
                    var matcher = (Matcher)Matcher.AllOf(InputComponentIds.HoverEnabled);
                    matcher.componentNames = InputComponentIds.componentNames;
                    _matcherHoverEnabled = matcher;
                }

                return _matcherHoverEnabled;
            }
        }
    }
