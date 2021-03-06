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

        static readonly FortifiableComponent fortifiableComponent = new FortifiableComponent();

        public bool isFortifiable {
            get { return HasComponent(CoreComponentIds.Fortifiable); }
            set {
                if(value != isFortifiable) {
                    if(value) {
                        AddComponent(CoreComponentIds.Fortifiable, fortifiableComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.Fortifiable);
                    }
                }
            }
        }

        public Entity IsFortifiable(bool value) {
            isFortifiable = value;
            return this;
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherFortifiable;

        public static IMatcher Fortifiable {
            get {
                if(_matcherFortifiable == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Fortifiable);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherFortifiable = matcher;
                }

                return _matcherFortifiable;
            }
        }
    }
