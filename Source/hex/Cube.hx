// Cube hex coordinates (x, y, z)
// From http://www.redblobgames.com/articles/grids/hexagons/
// Copyright 2013 Red Blob Games <redblobgames@gmail.com>
// License: Apache v2.0 <http://www.apache.org/licenses/LICENSE-2.0.html>

// Note that I use a 3-vector for the coordinates. Cube hex
// coordinates share a lot with vectors in 3-space so if you already
// have a 3-vector class you can reuse it for hexes. Trapezoidal hex
// coordinates (q, r) share a lot with vectors in 2-space and screen
// coordinates are of course vectors in 2-space, so all of these could
// use the same underlying class to share code. However I'm keeping
// some of these separate (and duplicating code) to make the
// explanations in the article easier to follow.
package hex;

@:keep @:expose class Cube {
    public function new(x:Int, y:Int, z:Int)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public var x:Int;
    public var y:Int;
    public var z:Int;

    static public function add(a:Cube, b:Cube):Cube
    {
        return new Cube(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    static public function scale(a:Cube, k:Int):Cube {
        return new Cube(a.x * k, a.y * k, a.z * k);
    }
                                                                                                         
    static public var directions:Array<Cube> = [new Cube(1, -1, 0), new Cube(1, 0, -1), new Cube(0, 1, -1), new Cube(-1, 1, 0), new Cube(-1, 0, 1), new Cube(0, -1, 1)];

    static public function direction(direction:Int):Cube
    {
        return Cube.directions[direction];
    }


    static public function neighbor(hex:Cube, direction:Int):Cube
    {
        return Cube.add(hex, Cube.direction(direction));
    }

    static public var diagonals:Array<Cube> = [new Cube(2, -1, -1), new Cube(1, 1, -2), new Cube(-1, 2, -1), new Cube(-2, 1, 1), new Cube(-1, -1, 2), new Cube(1, -2, 1)];

    static public function diagonalNeighbor(hex:Cube, direction:Int):Cube
    {
        return Cube.add(hex, Cube.diagonals[direction]);
    }

    static public function distance(a:Cube, b:Cube):Int
    {
        return Std.int((Math.abs(a.x - b.x) + Math.abs(a.y - b.y) + Math.abs(a.z - b.z)) / 2);
    }

    public function toString() {
        return v().join(",");
    }

    public function v() {
        return [x, y, z];
    }

    public function rotateLeft():Cube {
        return new Cube(-y, -z, -x);
    }
 
    public function rotateRight():Cube {
        return new Cube(-z, -x, -y);
    }

    public static function length(h:Cube):Int {
        return Std.int((Math.abs(h.x) + Math.abs(h.y) + Math.abs(h.z)) / 2);
    }

    public function equals(other:Cube):Bool {
        return x == other.x && y == other.y && z == other.z;
    }
}

