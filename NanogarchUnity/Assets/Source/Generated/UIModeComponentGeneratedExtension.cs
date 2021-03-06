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

        static readonly UIModeComponent uIModeComponent = new UIModeComponent();

        public bool isUIMode {
            get { return HasComponent(UIComponentIds.UIMode); }
            set {
                if(value != isUIMode) {
                    if(value) {
                        AddComponent(UIComponentIds.UIMode, uIModeComponent);
                    } else {
                        RemoveComponent(UIComponentIds.UIMode);
                    }
                }
            }
        }

        public Entity IsUIMode(bool value) {
            isUIMode = value;
            return this;
        }
    }

    public partial class Context {

        public Entity uIModeEntity { get { return GetGroup(UIMatcher.UIMode).GetSingleEntity(); } }

        public bool isUIMode {
            get { return uIModeEntity != null; }
            set {
                var entity = uIModeEntity;
                if(value != (entity != null)) {
                    if(value) {
                        CreateEntity().isUIMode = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class UIMatcher {

        static IMatcher _matcherUIMode;

        public static IMatcher UIMode {
            get {
                if(_matcherUIMode == null) {
                    var matcher = (Matcher)Matcher.AllOf(UIComponentIds.UIMode);
                    matcher.componentNames = UIComponentIds.componentNames;
                    _matcherUIMode = matcher;
                }

                return _matcherUIMode;
            }
        }
    }
