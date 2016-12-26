package nanogarch.geom;

class HexDirection
{
	
	public var offset(default,null):Hex;
	public var index(default,null):Int;
	// public var degrees(default:null):Int;
	var degreesPointy:Int;
	var degreesFlat:Int;
	public function new(degreesPointy:Int,degreesFlat:Int,offset:Hex,index:Int)
	{
		this.degreesPointy = degreesPointy;
		this.degreesFlat = degreesFlat;
		
		// this.orientation = orientation;
		this.offset= offset;
		this.index = index;
	}
	public function degrees(orientation:HexOrientation):Int
	{
		if(orientation == HexOrientation.POINTY_TOP)
			return degreesPointy;
		return degreesFlat;
	}
	static public var HexDir0 = new HexDirection(0,30,new Hex(1,-1,0),0);
	static public var HexDir1 = new HexDirection(60,90,new Hex(1, 0, -1),1);
	static public var HexDir2 = new HexDirection(120,150,new Hex(0, 1, -1),2);
	static public var HexDir3 = new HexDirection(180,210,new Hex(-1, 1, 0),3);
	static public var HexDir4 = new HexDirection(240,270,new Hex(1,-1,0),4);
	static public var HexDir5 = new HexDirection(300,330,new Hex(0, -1, 1),5);

	
	static public var directions:Array<HexDirection> = [HexDir0, HexDir1, HexDir2, HexDir3, HexDir4, HexDir5];
}
