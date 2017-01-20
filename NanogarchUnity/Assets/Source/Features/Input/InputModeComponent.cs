
using Entitas;
using Entitas.CodeGenerator;
[Input,SingleEntity]
public sealed class InputModeComponent : IComponent{}

[Input,SingleEntity]
public sealed class SelectionEnabledComponent : IComponent {}


[Input,SingleEntity]
public sealed class DragEnabledComponent : IComponent {}

[Input,SingleEntity]
public sealed class HoverEnabledComponent : IComponent {}