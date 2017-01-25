using Entitas;
using Entitas.CodeGenerator;

// enum InputMode {FreePick,}
[UI,SingleEntity]
public sealed class UIModeComponent : IComponent
{

}

[UI,SingleEntity]
public sealed class ExecuteOrdersButtonEnabledComponent : IComponent {}


[UI,SingleEntity]
public sealed class CommandPickerEnabledComponent : IComponent {}
