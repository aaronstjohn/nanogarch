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

        public AnimationCurveComponent animationCurve { get { return (AnimationCurveComponent)GetComponent(VisualDebuggingComponentIds.AnimationCurve); } }
        public bool hasAnimationCurve { get { return HasComponent(VisualDebuggingComponentIds.AnimationCurve); } }

        public void AddAnimationCurve(UnityEngine.AnimationCurve newAnimationCurve) {
            var component = CreateComponent<AnimationCurveComponent>(VisualDebuggingComponentIds.AnimationCurve);
            component.animationCurve = newAnimationCurve;
            AddComponent(VisualDebuggingComponentIds.AnimationCurve, component);
        }

        public void ReplaceAnimationCurve(UnityEngine.AnimationCurve newAnimationCurve) {
            var component = CreateComponent<AnimationCurveComponent>(VisualDebuggingComponentIds.AnimationCurve);
            component.animationCurve = newAnimationCurve;
            ReplaceComponent(VisualDebuggingComponentIds.AnimationCurve, component);
        }

        public void RemoveAnimationCurve() {
            RemoveComponent(VisualDebuggingComponentIds.AnimationCurve);
        }
    }
}

    public partial class VisualDebuggingMatcher {

        static IMatcher _matcherAnimationCurve;

        public static IMatcher AnimationCurve {
            get {
                if(_matcherAnimationCurve == null) {
                    var matcher = (Matcher)Matcher.AllOf(VisualDebuggingComponentIds.AnimationCurve);
                    matcher.componentNames = VisualDebuggingComponentIds.componentNames;
                    _matcherAnimationCurve = matcher;
                }

                return _matcherAnimationCurve;
            }
        }
    }