package nanogarch.map;
// import graphx.Graph;
import graphx.Edge;
import graphx.NodeFunctions;
import util.GraphExt;

class HexCellGraph extends GraphExt<HexCell> {

	public function new() {
	

		var nodeFunctions : NodeFunctions<HexCell> = {
		  isEqual: function(a : HexCell, b : HexCell) : Bool {
		    return a.position.equals(b.position);
		  },
		  getKey: function(a : HexCell) : String {
		    return Std.string(a.position.x)+":"+ Std.string(a.position.y)+":"+ Std.string(a.position.z);
		  }
		};
		super(nodeFunctions);

	}
	public function addAllDirectedEdges(hexCell:HexCell):HexCellGraph
	{
		if(!contains(hexCell))
			return this;

		for( dir in 0...6)
		{
			edgeInDirection(dir,hexCell);
		}
		return this;
	}
	public function edgeInDirection (direction:Int,from:HexCell): HexCellGraph
	{
		if(!contains(from))
			return this;

		var toHex:Hex = Hex.neighbor(from.position,direction);

		var toNodes = nodes.filter( function(n){return n.value.position.equals(toHex);});

		if( toNodes.length == 1 && !containsEdge(from,toNodes[0]))
		{
			addEdgeFrom(from,toNodes[0]);
		}

		
		return this;
	}
	
}