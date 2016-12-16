package nanogarch.map;

class HexMap  {
	
	public var hexCellMap:Map<Hex,HexCell>;

	public var graph:HexCellGraph;
	public var grid:HexGrid;

	public function new(hexgrid:HexGrid)
	{
		grid=hexgrid;
		graph = new HexCellGraph();
		hexCellMap = new Map<Hex,HexCell>();
		for (hex in grid.hexes)
		{
			var cell:HexCell = new HexCell(hex);
			graph.addNode(cell);
			hexCellMap.set(hex,cell);
		}
		//Fully connect the graph 
		for (hex in grid.hexes)
		{
			var cell:HexCell = hexCellMap.get(hex);
			graph.addAllDirectedEdges(cell);
		}
	}
  

	 
}