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

        static readonly CommandStateComponent commandStateComponent = new CommandStateComponent();

        public bool isCommandState {
            get { return HasComponent(CoreComponentIds.CommandState); }
            set {
                if(value != isCommandState) {
                    if(value) {
                        AddComponent(CoreComponentIds.CommandState, commandStateComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.CommandState);
                    }
                }
            }
        }

        public Entity IsCommandState(bool value) {
            isCommandState = value;
            return this;
        }
    }

    public partial class Context {

        public Entity commandStateEntity { get { return GetGroup(CoreMatcher.CommandState).GetSingleEntity(); } }

        public bool isCommandState {
            get { return commandStateEntity != null; }
            set {
                var entity = commandStateEntity;
                if(value != (entity != null)) {
                    if(value) {
                        CreateEntity().isCommandState = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherCommandState;

        public static IMatcher CommandState {
            get {
                if(_matcherCommandState == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.CommandState);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherCommandState = matcher;
                }

                return _matcherCommandState;
            }
        }
    }
