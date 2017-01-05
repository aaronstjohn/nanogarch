package nanogarch.commands;
import ash.core.Entity;
class MapCellInfoCommand extends Command<Entity>
{
	public function new(){super();}

	override public function Execute(arg:Entity)
	{
		trace("OVER MAP CELL!!!");
		
	}
}