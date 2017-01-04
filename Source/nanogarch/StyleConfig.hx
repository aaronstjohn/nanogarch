package nanogarch;

import openfl.text.TextFormat;
import nanogarch.model.TerrainType;
import nanogarch.Styles;

class StyleConfig
{
	public var defaultColliderFillColor:UInt = 0xA2C0F2;
	public var defaultColliderLineColor:UInt = 0x000000;
	public var defaultColliderLineThickness:Float = 1.0;

	public var defaultWorldCellFillColor:UInt = 0xA2C0F2;
	public var defaultWorldCellLineColor:UInt = 0x000000;
	public var defaultWorldCellLineThickness:Float = 1.0;
	public var defaultTextFormat:TextFormat =  new TextFormat(Fonts.M_PLUS_1M_BOLD, 10);
	public var terrainStyles:Map<TerrainType,Styles.Shape>;
	public function new()
    {
    	terrainStyles = new Map<TerrainType,Styles.Shape>();
	    // http://www.creativecolorschemes.com/resources/free-color-schemes/gray-tone-color-scheme.shtml
    	terrainStyles.set(TerrainType.DESERT,new Styles.Shape(0xB99C6B,0xB99C6B,2.0));
    	terrainStyles.set(TerrainType.GRASS,new Styles.Shape(0xB2C891,0xB2C891,2.0));
    	terrainStyles.set(TerrainType.SCRUB,new Styles.Shape(0xC9C27F,0xC9C27F,2.0));
    	terrainStyles.set(TerrainType.FOREST,new Styles.Shape(0x74A18E,0x74A18E,2.0));
    	terrainStyles.set(TerrainType.HILLS,new Styles.Shape(0x91867E,0x91867E,2.0));
    	terrainStyles.set(TerrainType.MOUNTAINS,new Styles.Shape(0x949494,0x949494,2.0));
    	terrainStyles.set(TerrainType.WATER,new Styles.Shape(0x818FB5,0x818FB5,2.0));
    	


    }
}
// DESERT;
// 	GRASS;
// 	SCRUB;
// 	FOREST;
// 	HILL;
// 	MOUNTAIN;
// 	WATER;