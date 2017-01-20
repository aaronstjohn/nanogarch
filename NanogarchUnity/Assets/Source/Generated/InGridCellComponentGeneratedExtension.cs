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

        public InGridCellComponent inGridCell { get { return (InGridCellComponent)GetComponent(CoreComponentIds.InGridCell); } }
        public bool hasInGridCell { get { return HasComponent(CoreComponentIds.InGridCell); } }

        public Entity AddInGridCell(int newId) {
            var component = CreateComponent<InGridCellComponent>(CoreComponentIds.InGridCell);
            component.id = newId;
            return AddComponent(CoreComponentIds.InGridCell, component);
        }

        public Entity ReplaceInGridCell(int newId) {
            var component = CreateComponent<InGridCellComponent>(CoreComponentIds.InGridCell);
            component.id = newId;
            ReplaceComponent(CoreComponentIds.InGridCell, component);
            return this;
        }

        public Entity RemoveInGridCell() {
            return RemoveComponent(CoreComponentIds.InGridCell);
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherInGridCell;

        public static IMatcher InGridCell {
            get {
                if(_matcherInGridCell == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.InGridCell);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherInGridCell = matcher;
                }

                return _matcherInGridCell;
            }
        }
    }
