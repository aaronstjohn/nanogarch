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

        public MoveCommandComponent moveCommand { get { return (MoveCommandComponent)GetComponent(CoreComponentIds.MoveCommand); } }
        public bool hasMoveCommand { get { return HasComponent(CoreComponentIds.MoveCommand); } }

        public Entity AddMoveCommand(int newSourceCell, int newDestCell) {
            var component = CreateComponent<MoveCommandComponent>(CoreComponentIds.MoveCommand);
            component.sourceCell = newSourceCell;
            component.destCell = newDestCell;
            return AddComponent(CoreComponentIds.MoveCommand, component);
        }

        public Entity ReplaceMoveCommand(int newSourceCell, int newDestCell) {
            var component = CreateComponent<MoveCommandComponent>(CoreComponentIds.MoveCommand);
            component.sourceCell = newSourceCell;
            component.destCell = newDestCell;
            ReplaceComponent(CoreComponentIds.MoveCommand, component);
            return this;
        }

        public Entity RemoveMoveCommand() {
            return RemoveComponent(CoreComponentIds.MoveCommand);
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherMoveCommand;

        public static IMatcher MoveCommand {
            get {
                if(_matcherMoveCommand == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.MoveCommand);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherMoveCommand = matcher;
                }

                return _matcherMoveCommand;
            }
        }
    }
