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

        static readonly SelectionEnabledComponent selectionEnabledComponent = new SelectionEnabledComponent();

        public bool isSelectionEnabled {
            get { return HasComponent(InputComponentIds.SelectionEnabled); }
            set {
                if(value != isSelectionEnabled) {
                    if(value) {
                        AddComponent(InputComponentIds.SelectionEnabled, selectionEnabledComponent);
                    } else {
                        RemoveComponent(InputComponentIds.SelectionEnabled);
                    }
                }
            }
        }

        public Entity IsSelectionEnabled(bool value) {
            isSelectionEnabled = value;
            return this;
        }
    }

    public partial class Context {

        public Entity selectionEnabledEntity { get { return GetGroup(InputMatcher.SelectionEnabled).GetSingleEntity(); } }

        public bool isSelectionEnabled {
            get { return selectionEnabledEntity != null; }
            set {
                var entity = selectionEnabledEntity;
                if(value != (entity != null)) {
                    if(value) {
                        CreateEntity().isSelectionEnabled = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class InputMatcher {

        static IMatcher _matcherSelectionEnabled;

        public static IMatcher SelectionEnabled {
            get {
                if(_matcherSelectionEnabled == null) {
                    var matcher = (Matcher)Matcher.AllOf(InputComponentIds.SelectionEnabled);
                    matcher.componentNames = InputComponentIds.componentNames;
                    _matcherSelectionEnabled = matcher;
                }

                return _matcherSelectionEnabled;
            }
        }
    }
