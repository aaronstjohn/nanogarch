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

        static readonly InputModeComponent inputModeComponent = new InputModeComponent();

        public bool isInputMode {
            get { return HasComponent(InputComponentIds.InputMode); }
            set {
                if(value != isInputMode) {
                    if(value) {
                        AddComponent(InputComponentIds.InputMode, inputModeComponent);
                    } else {
                        RemoveComponent(InputComponentIds.InputMode);
                    }
                }
            }
        }

        public Entity IsInputMode(bool value) {
            isInputMode = value;
            return this;
        }
    }

    public partial class Context {

        public Entity inputModeEntity { get { return GetGroup(InputMatcher.InputMode).GetSingleEntity(); } }

        public bool isInputMode {
            get { return inputModeEntity != null; }
            set {
                var entity = inputModeEntity;
                if(value != (entity != null)) {
                    if(value) {
                        CreateEntity().isInputMode = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class InputMatcher {

        static IMatcher _matcherInputMode;

        public static IMatcher InputMode {
            get {
                if(_matcherInputMode == null) {
                    var matcher = (Matcher)Matcher.AllOf(InputComponentIds.InputMode);
                    matcher.componentNames = InputComponentIds.componentNames;
                    _matcherInputMode = matcher;
                }

                return _matcherInputMode;
            }
        }
    }
