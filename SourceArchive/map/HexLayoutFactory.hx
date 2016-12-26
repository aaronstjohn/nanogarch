class HexLayoutFactory
{
	static public function createHexagonalLayout(size:Int):Array<Hex> {
        var hexes = [];
        for (x in -size...size+1) {
            for (y in -size...size+1) {
                var z = -x-y;
                if (Math.abs(x) <= size && Math.abs(y) <= size && Math.abs(z) <= size) {
                    hexes.push(new Hex(x, y, z));
                }
            }
        }
        return hexes;
    }
}