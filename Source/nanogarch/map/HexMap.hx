package nanogarch.map;
import haxe.ds.HashMap;
class HexMap  {
	
	public var cellMap:HashMap<Hex,HexCell>;

	public var graph:HexCellGraph;
	public var grid:HexGrid;

	public function new(hexgrid:HexGrid)
	{
		grid=hexgrid;
		graph = new HexCellGraph();
		cellMap = new HashMap<Hex,HexCell>();
		for (hex in grid.hexes)
		{
			var cell:HexCell = new HexCell(hex);
			graph.addNode(cell);
			cellMap.set(hex,cell);
		}
		//Fully connect the graph 
		for (hex in grid.hexes)
		{
			var cell:HexCell = cellMap.get(hex);
			graph.addAllDirectedEdges(cell);
		}

	}
  

	 
}