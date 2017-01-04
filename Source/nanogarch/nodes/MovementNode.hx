package nanogarch.nodes;
import ash.core.Node;
import nanogarch.components.Movement;
import nanogarch.components.HexFrame;
import nanogarch.components.Unit;

class MovementNode extends Node<MovementNode>
{
	public var movement:Movement;
	public var unit:Unit;
	public var hexFrame:HexFrame;
}