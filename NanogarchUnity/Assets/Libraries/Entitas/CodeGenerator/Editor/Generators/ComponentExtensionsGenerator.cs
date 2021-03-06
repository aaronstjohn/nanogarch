using System.Collections.Generic;
using System.Linq;
using Entitas.Serialization;

namespace Entitas.CodeGenerator {

    public class ComponentExtensionsGenerator : IComponentCodeGenerator {

        public CodeGenFile[] Generate(ComponentInfo[] componentInfos) {
            var generatorName = GetType().FullName;
            return componentInfos
                .Where(info => info.generateMethods)
                .Select(info => new CodeGenFile(
                    info.fullTypeName + "GeneratedExtension",
                    generateComponentExtension(info),
                    generatorName
                )).ToArray();
        }

        static string generateComponentExtension(ComponentInfo componentInfo) {
            var code = addNamespace();
            code += addEntityMethods(componentInfo);
            if(componentInfo.isSingleEntity) {
                code += addContextMethods(componentInfo);
            }

            if(componentInfo.generateComponent) {
                // Add default matcher
                code += addMatcher(componentInfo, true);
                code += closeNamespace();
                // Add custom matchers
                code += addMatcher(componentInfo);
                return addUsings("Entitas")
                    + generateComponent(componentInfo)
                    + code;
            }

            code += addMatcher(componentInfo, true);
            code += closeNamespace();

            var hasCustomcContexts = componentInfo.contexts.Length > 1 || !componentInfo.contexts[0].IsDefaultContextName();
            if(hasCustomcContexts) {
                code += addMatcher(componentInfo);
                code = addUsings("Entitas") + code;
            }

            return code;
        }

        static string generateComponent(ComponentInfo componentInfo) {
            const string hideInBlueprintInspector = "[Entitas.Serialization.Blueprints.HideInBlueprintInspectorAttribute]\n";
            const string componentFormat = @"public class {0} : IComponent {{

    public {1} {2};
}}

";
            var memberInfo = componentInfo.memberInfos[0];
            var code = string.Format(componentFormat, componentInfo.fullTypeName, memberInfo.type, memberInfo.name);
            return componentInfo.hideInBlueprintInspector
                        ? hideInBlueprintInspector + code
                        : code;
        }

        static string addUsings(params string[] usings) {
            return string.Join("\n", usings
                .Select(name => "using " + name + ";")
                .ToArray()) + "\n\n";
        }

        static string addNamespace() {
            return "namespace Entitas {\n";
        }

        static string closeNamespace() {
            return "}\n";
        }

        /*
         *
         * ENTITY METHODS
         *
         */

        static string addEntityMethods(ComponentInfo componentInfo) {
            return addEntityClassHeader()
                    + addGetMethods(componentInfo)
                    + addHasMethods(componentInfo)
                    + addAddMethods(componentInfo)
                    + addReplaceMethods(componentInfo)
                    + addRemoveMethods(componentInfo)
                    + addCloseClass();
        }

        static string addEntityClassHeader() {
            return "\n    public partial class Entity {\n";
        }

        static string addGetMethods(ComponentInfo componentInfo) {
            var getMethod = componentInfo.isSingletonComponent
                    ? "\n        static readonly $Type $nameComponent = new $Type();\n"
                    : "\n        public $Type $name { get { return ($Type)GetComponent($Ids.$Name); } }\n";

            return buildString(componentInfo, getMethod);
        }

        static string addHasMethods(ComponentInfo componentInfo) {
            var hasMethod = componentInfo.isSingletonComponent ? @"
        public bool $prefix$Name {
            get { return HasComponent($Ids.$Name); }
            set {
                if(value != $prefix$Name) {
                    if(value) {
                        AddComponent($Ids.$Name, $nameComponent);
                    } else {
                        RemoveComponent($Ids.$Name);
                    }
                }
            }
        }

        public Entity $Prefix$Name(bool value) {
            $prefix$Name = value;
            return this;
        }
" : @"        public bool has$Name { get { return HasComponent($Ids.$Name); } }
";
            return buildString(componentInfo, hasMethod);
        }

        static string addAddMethods(ComponentInfo componentInfo) {
            return componentInfo.isSingletonComponent ? string.Empty : buildString(componentInfo, @"
        public Entity Add$Name($typedArgs) {
            var component = CreateComponent<$Type>($Ids.$Name);
$assign
            return AddComponent($Ids.$Name, component);
        }
");
        }

        static string addReplaceMethods(ComponentInfo componentInfo) {
            return componentInfo.isSingletonComponent ? string.Empty : buildString(componentInfo, @"
        public Entity Replace$Name($typedArgs) {
            var component = CreateComponent<$Type>($Ids.$Name);
$assign
            ReplaceComponent($Ids.$Name, component);
            return this;
        }
");
        }

        static string addRemoveMethods(ComponentInfo componentInfo) {
            return componentInfo.isSingletonComponent ? string.Empty : buildString(componentInfo, @"
        public Entity Remove$Name() {
            return RemoveComponent($Ids.$Name);
        }
");
        }

        /*
         *
         * CONTEXT METHODS
         *
         */

        static string addContextMethods(ComponentInfo componentInfo) {
            return addContextClassHeader()
                    + addContextGetMethods(componentInfo)
                    + addContextHasMethods(componentInfo)
                    + addContextAddMethods(componentInfo)
                    + addContextReplaceMethods(componentInfo)
                    + addContextRemoveMethods(componentInfo)
                    + addCloseClass();
        }

        static string addContextClassHeader() {
            return "\n    public partial class Context {\n";
        }

        static string addContextGetMethods(ComponentInfo componentInfo) {
            var getMehod = componentInfo.isSingletonComponent ? @"
        public Entity $nameEntity { get { return GetGroup($TagMatcher.$Name).GetSingleEntity(); } }
" : @"
        public Entity $nameEntity { get { return GetGroup($TagMatcher.$Name).GetSingleEntity(); } }
        public $Type $name { get { return $nameEntity.$name; } }
";
            return buildString(componentInfo, getMehod);
        }

        static string addContextHasMethods(ComponentInfo componentInfo) {
            var hasMethod = componentInfo.isSingletonComponent ? @"
        public bool $prefix$Name {
            get { return $nameEntity != null; }
            set {
                var entity = $nameEntity;
                if(value != (entity != null)) {
                    if(value) {
                        CreateEntity().$prefix$Name = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
" : @"        public bool has$Name { get { return $nameEntity != null; } }
";
            return buildString(componentInfo, hasMethod);
        }

        static object addContextAddMethods(ComponentInfo componentInfo) {
            return componentInfo.isSingletonComponent ? string.Empty : buildString(componentInfo, @"
        public Entity Set$Name($typedArgs) {
            if(has$Name) {
                throw new EntitasException(""Could not set $name!\n"" + this + "" already has an entity with $Type!"",
                    ""You should check if the context already has a $nameEntity before setting it or use context.Replace$Name()."");
            }
            var entity = CreateEntity();
            entity.Add$Name($args);
            return entity;
        }
");
        }

        static string addContextReplaceMethods(ComponentInfo componentInfo) {
            return componentInfo.isSingletonComponent ? string.Empty : buildString(componentInfo, @"
        public Entity Replace$Name($typedArgs) {
            var entity = $nameEntity;
            if(entity == null) {
                entity = Set$Name($args);
            } else {
                entity.Replace$Name($args);
            }

            return entity;
        }
");
        }

        static string addContextRemoveMethods(ComponentInfo componentInfo) {
            return componentInfo.isSingletonComponent ? string.Empty : buildString(componentInfo, @"
        public void Remove$Name() {
            DestroyEntity($nameEntity);
        }
");
        }

        /*
        *
        * MATCHER
        *
        */

       static string addMatcher(ComponentInfo componentInfo, bool onlyDefault = false) {
            const string matcherFormat = @"
    public partial class $TagMatcher {

        static IMatcher _matcher$Name;

        public static IMatcher $Name {
            get {
                if(_matcher$Name == null) {
                    var matcher = (Matcher)Matcher.AllOf($Ids.$Name);
                    matcher.componentNames = $Ids.componentNames;
                    _matcher$Name = matcher;
                }

                return _matcher$Name;
            }
        }
    }
";

            if(onlyDefault) {
                if(componentInfo.contexts.Contains(CodeGenerator.DEFAULT_CONTEXT_NAME)) {
                    return buildString(componentInfo, matcherFormat);
                } else {
                    return string.Empty;
                }
            } else {
                var contextIndex = 0;
                var matchers = componentInfo.contexts.Aggregate(string.Empty, (acc, contextName) => {
                    if(!contextName.IsDefaultContextName()) {
                        return acc + buildString(componentInfo, matcherFormat, contextIndex++);
                    } else {
                        contextIndex += 1;
                        return acc;
                    }
                });

                return buildString(componentInfo, matchers);
            }
        }

        /*
         *
         * HELPERS
         *
         */

        static string buildString(ComponentInfo componentInfo, string format, int contextIndex = 0) {
            format = createFormatString(format);
            var a0_type = componentInfo.fullTypeName;
            var a1_name = componentInfo.typeName.RemoveComponentSuffix();
            var a2_lowercaseName = a1_name.LowercaseFirst();
            var contextNames = componentInfo.contexts;
            var a3_tag = contextNames[contextIndex].ContextPrefix();
            var lookupTags = componentInfo.ComponentLookupTags();
            var a4_ids = lookupTags.Length == 0 ? string.Empty : lookupTags[contextIndex];
            var memberInfos = componentInfo.memberInfos;
            var a5_memberNamesWithType = memberNamesWithType(memberInfos);
            var a6_memberAssigns = memberAssignments(memberInfos);
            var a7_memberNames = memberNames(memberInfos);
            var prefix = componentInfo.singleComponentPrefix;
            var a8_prefix = prefix.UppercaseFirst();
            var a9_lowercasePrefix = prefix.LowercaseFirst();

            return string.Format(format, a0_type, a1_name, a2_lowercaseName,
                a3_tag, a4_ids, a5_memberNamesWithType, a6_memberAssigns, a7_memberNames,
                a8_prefix, a9_lowercasePrefix);
        }

        static string createFormatString(string format) {
            return format.Replace("{", "{{")
                        .Replace("}", "}}")
                        .Replace("$Type", "{0}")
                        .Replace("$Name", "{1}")
                        .Replace("$name", "{2}")
                        .Replace("$Tag", "{3}")
                        .Replace("$Ids", "{4}")
                        .Replace("$typedArgs", "{5}")
                        .Replace("$assign", "{6}")
                        .Replace("$args", "{7}")
                        .Replace("$Prefix", "{8}")
                        .Replace("$prefix", "{9}");
        }

        static string memberNamesWithType(List<PublicMemberInfo> memberInfos) {
            var typedArgs = memberInfos
                .Select(info => info.type.ToCompilableString() + " new" + info.name.UppercaseFirst())
                .ToArray();

            return string.Join(", ", typedArgs);
        }

        static string memberAssignments(List<PublicMemberInfo> memberInfos) {
            const string format = "            component.{0} = {1};";
            var assignments = memberInfos.Select(info => {
                var newArg = "new" + info.name.UppercaseFirst();
                return string.Format(format, info.name, newArg);
            }).ToArray();

            return string.Join("\n", assignments);
        }

        static string memberNames(List<PublicMemberInfo> memberInfos) {
            var args = memberInfos.Select(info => "new" + info.name.UppercaseFirst()).ToArray();
            return string.Join(", ", args);
        }

        static string addCloseClass() {
            return "    }\n";
        }
    }
}
