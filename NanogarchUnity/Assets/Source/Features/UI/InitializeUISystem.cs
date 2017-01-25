using Entitas;
public sealed class InitializeUISystem :IInitializeSystem
{
	readonly Context _context;
    public InitializeUISystem(Contexts contexts) 
	{
		_context= contexts.uI;
	}
	public void Initialize() {
		_context.CreateEntity()
			.IsUIMode(true)
			.IsExecuteOrdersButtonEnabled(false)
			.IsCommandPickerEnabled(false);
	}
}