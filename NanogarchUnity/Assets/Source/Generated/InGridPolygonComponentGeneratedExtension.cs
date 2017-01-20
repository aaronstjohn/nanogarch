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

        public InGridPolygonComponent inGridPolygon { get { return (InGridPolygonComponent)GetComponent(CoreComponentIds.InGridPolygon); } }
        public bool hasInGridPolygon { get { return HasComponent(CoreComponentIds.InGridPolygon); } }

        public Entity AddInGridPolygon(int newGridPolyId) {
            var component = CreateComponent<InGridPolygonComponent>(CoreComponentIds.InGridPolygon);
            component.gridPolyId = newGridPolyId;
            return AddComponent(CoreComponentIds.InGridPolygon, component);
        }

        public Entity ReplaceInGridPolygon(int newGridPolyId) {
            var component = CreateComponent<InGridPolygonComponent>(CoreComponentIds.InGridPolygon);
            component.gridPolyId = newGridPolyId;
            ReplaceComponent(CoreComponentIds.InGridPolygon, component);
            return this;
        }

        public Entity RemoveInGridPolygon() {
            return RemoveComponent(CoreComponentIds.InGridPolygon);
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherInGridPolygon;

        public static IMatcher InGridPolygon {
            get {
                if(_matcherInGridPolygon == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.InGridPolygon);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherInGridPolygon = matcher;
                }

                return _matcherInGridPolygon;
            }
        }
    }
