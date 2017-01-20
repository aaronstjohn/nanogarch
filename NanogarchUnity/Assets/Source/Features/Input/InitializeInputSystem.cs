using Entitas;
public sealed class InitializeInputSystem :IInitializeSystem
{
	readonly Context _context;
    public InitializeInputSystem(Contexts contexts) 
	{
		_context= contexts.input;
	}
	public void Initialize() {
		_context.CreateEntity()
			.AddName("InputState")
			.IsInputMode(true)
			.IsPickingEnabled(true)
			.IsDragEnabled(true)
			.IsHoverEnabled(true);
	}
}