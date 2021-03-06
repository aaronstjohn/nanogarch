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

        public CommandIssuedComponent commandIssued { get { return (CommandIssuedComponent)GetComponent(CoreComponentIds.CommandIssued); } }
        public bool hasCommandIssued { get { return HasComponent(CoreComponentIds.CommandIssued); } }

        public Entity AddCommandIssued(CommandType newCommand) {
            var component = CreateComponent<CommandIssuedComponent>(CoreComponentIds.CommandIssued);
            component.command = newCommand;
            return AddComponent(CoreComponentIds.CommandIssued, component);
        }

        public Entity ReplaceCommandIssued(CommandType newCommand) {
            var component = CreateComponent<CommandIssuedComponent>(CoreComponentIds.CommandIssued);
            component.command = newCommand;
            ReplaceComponent(CoreComponentIds.CommandIssued, component);
            return this;
        }

        public Entity RemoveCommandIssued() {
            return RemoveComponent(CoreComponentIds.CommandIssued);
        }
    }

    public partial class Context {

        public Entity commandIssuedEntity { get { return GetGroup(CoreMatcher.CommandIssued).GetSingleEntity(); } }
        public CommandIssuedComponent commandIssued { get { return commandIssuedEntity.commandIssued; } }
        public bool hasCommandIssued { get { return commandIssuedEntity != null; } }

        public Entity SetCommandIssued(CommandType newCommand) {
            if(hasCommandIssued) {
                throw new EntitasException("Could not set commandIssued!\n" + this + " already has an entity with CommandIssuedComponent!",
                    "You should check if the context already has a commandIssuedEntity before setting it or use context.ReplaceCommandIssued().");
            }
            var entity = CreateEntity();
            entity.AddCommandIssued(newCommand);
            return entity;
        }

        public Entity ReplaceCommandIssued(CommandType newCommand) {
            var entity = commandIssuedEntity;
            if(entity == null) {
                entity = SetCommandIssued(newCommand);
            } else {
                entity.ReplaceCommandIssued(newCommand);
            }

            return entity;
        }

        public void RemoveCommandIssued() {
            DestroyEntity(commandIssuedEntity);
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherCommandIssued;

        public static IMatcher CommandIssued {
            get {
                if(_matcherCommandIssued == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.CommandIssued);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherCommandIssued = matcher;
                }

                return _matcherCommandIssued;
            }
        }
    }
