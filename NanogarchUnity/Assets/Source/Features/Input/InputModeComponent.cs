
using Entitas;
using Entitas.CodeGenerator;

// enum InputMode {FreePick,}
[Input,SingleEntity]
public sealed class InputModeComponent : IComponent
{

}

[Input,SingleEntity]
public sealed class PickingEnabledComponent : IComponent {}


[Input,SingleEntity]
public sealed class DragEnabledComponent : IComponent {}

[Input,SingleEntity]
public sealed class HoverEnabledComponent : IComponent {}