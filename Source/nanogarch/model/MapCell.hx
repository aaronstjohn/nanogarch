package nanogarch.model;
import ash.core.Entity;

class MapCell
{
	private var entities:Array<Entity>;
	public function new()
	{
		entities = new Array<Entity>();
	}
	public function iterator():Iterator<Entity>
	{
		return entities.iterator();
	}
	public function addEntity(entity:Entity)
	{
		entities.push(entity);
	}
	public function removeEntity(entity:Entity)
	{
		entities.remove(entity);
	}
	
}