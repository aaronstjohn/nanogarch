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

        static readonly ExecutingOrdersComponent executingOrdersComponent = new ExecutingOrdersComponent();

        public bool isExecutingOrders {
            get { return HasComponent(CoreComponentIds.ExecutingOrders); }
            set {
                if(value != isExecutingOrders) {
                    if(value) {
                        AddComponent(CoreComponentIds.ExecutingOrders, executingOrdersComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.ExecutingOrders);
                    }
                }
            }
        }

        public Entity IsExecutingOrders(bool value) {
            isExecutingOrders = value;
            return this;
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherExecutingOrders;

        public static IMatcher ExecutingOrders {
            get {
                if(_matcherExecutingOrders == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.ExecutingOrders);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherExecutingOrders = matcher;
                }

                return _matcherExecutingOrders;
            }
        }
    }
