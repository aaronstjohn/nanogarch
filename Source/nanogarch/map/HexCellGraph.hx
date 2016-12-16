package nanogarch.map;
import graphx.Graph;
import graphx.Edge;
using util.GraphExtensions;

class HexCellGraph extends Graph<HexCell> {

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

		for( dir in 0..6)
		{
			edgeInDirection(dir,hex)
		}
		return this;
	}
	public function edgeInDirection (direction:Int,from:HexCell): HexCellGraph
	{
		if(!contains(from))
			return this;

		var toHex:Hex = Hex.neighbor(from.hex,direction);

		// var potentialEdge = new Edge(from,new HexCell(to) );

		if ( !anyEdge(function(e){
				return e.from.position.equals(from.position) && e.to.position.equals(toHex)
			}))
		{
			addEdgeFrom(from,to);
		}
		return this;
	}
	
}