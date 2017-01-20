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

        public SpawnComponent spawn { get { return (SpawnComponent)GetComponent(CoreComponentIds.Spawn); } }
        public bool hasSpawn { get { return HasComponent(CoreComponentIds.Spawn); } }

        public Entity AddSpawn(int newSpawnGridPolyId) {
            var component = CreateComponent<SpawnComponent>(CoreComponentIds.Spawn);
            component.spawnGridPolyId = newSpawnGridPolyId;
            return AddComponent(CoreComponentIds.Spawn, component);
        }

        public Entity ReplaceSpawn(int newSpawnGridPolyId) {
            var component = CreateComponent<SpawnComponent>(CoreComponentIds.Spawn);
            component.spawnGridPolyId = newSpawnGridPolyId;
            ReplaceComponent(CoreComponentIds.Spawn, component);
            return this;
        }

        public Entity RemoveSpawn() {
            return RemoveComponent(CoreComponentIds.Spawn);
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherSpawn;

        public static IMatcher Spawn {
            get {
                if(_matcherSpawn == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Spawn);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherSpawn = matcher;
                }

                return _matcherSpawn;
            }
        }
    }