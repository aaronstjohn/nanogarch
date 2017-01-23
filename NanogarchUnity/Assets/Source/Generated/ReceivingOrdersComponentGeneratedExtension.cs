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

        static readonly ReceivingOrdersComponent receivingOrdersComponent = new ReceivingOrdersComponent();

        public bool isReceivingOrders {
            get { return HasComponent(CoreComponentIds.ReceivingOrders); }
            set {
                if(value != isReceivingOrders) {
                    if(value) {
                        AddComponent(CoreComponentIds.ReceivingOrders, receivingOrdersComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.ReceivingOrders);
                    }
                }
            }
        }

        public Entity IsReceivingOrders(bool value) {
            isReceivingOrders = value;
            return this;
        }
    }

    public partial class Context {

        public Entity receivingOrdersEntity { get { return GetGroup(CoreMatcher.ReceivingOrders).GetSingleEntity(); } }

        public bool isReceivingOrders {
            get { return receivingOrdersEntity != null; }
            set {
                var entity = receivingOrdersEntity;
                if(value != (entity != null)) {
                    if(value) {
                        CreateEntity().isReceivingOrders = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class CoreMatcher {

        static IMatcher _matcherReceivingOrders;

        public static IMatcher ReceivingOrders {
            get {
                if(_matcherReceivingOrders == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.ReceivingOrders);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherReceivingOrders = matcher;
                }

                return _matcherReceivingOrders;
            }
        }
    }
