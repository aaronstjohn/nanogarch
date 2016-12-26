// A grid is a set of cube hex coordinates, scale, and orientation
// From http://www.redblobgames.com/articles/grids/
// Copyright 2012 Red Blob Games <redblobgames@gmail.com>
// License: Apache v2.0 <http://www.apache.org/licenses/LICENSE-2.0.html>

package hex;
// import heCube;
using Lambda;

@:keep @:expose class Grid {
    static public var SQRT_3_2 = Math.sqrt(3)/2;

    public var hexes:Array<Cube>;
    public var orientation:Bool;
    public var scale:Float;
    
    public function new(scale, orientation, shape) {
        this.scale = scale;
        this.orientation = orientation;
        this.hexes = shape;
    }


    public function hexToCenter(cube:Cube) {
        // NOTE: this is really a matrix multiply turning a 3-vector
        // into a 2-vector, and there's one matrix for each
        // orientation. In SymPy or NumPy you can write:
        //
        // M = Matrix( [ [ sqrt(3), sqrt(3)/2 ], [ 0, 3./2 ] ] )
        // or
        // M = Matrix( [ [ 3./2, 0 ], [ sqrt(3)/2, sqrt(3) ] ] )
        //
        // then [x, y] = size * M * [q, r]
        var s:ScreenCoordinate;
        var size = this.scale/2;
        // NOTE: I am ignoring cube.y. Why? Because I'm implicitly
        // converting cube coordinates to axial coordinates here. I
        // have hex to pixel and pixel to hex defined for axial
        // coordinates, *not* cube coordinates.
        if (orientation) {
            s = new ScreenCoordinate(Math.sqrt(3) * cube.x
                                     + Math.sqrt(3)/2 * cube.z,
                                     1.5 * cube.z);
        } else {
            s = new ScreenCoordinate(1.5 * cube.x,
                                     Math.sqrt(3)/2 * cube.x
                                     + Math.sqrt(3) * cube.z);
        }
        return s.scale(size);
    }
    

    public function cartesianToHex(p:ScreenCoordinate) {
        // NOTE: this is really the inverse matrix multiply. For the
        // matrix M defined in hexToCenter, get the inverse. It's
        // M**-1 in SymPy or M.I in NumPy.
        //
        // then [q, r] = (M**-1) * ([x, y] / size)
        //
        // Viewing these as matrix operations makes the transformation
        // from pixel to hex and hex to pixel easier to understand. I
        // used http://live.sympy.org/ to calculate them.
        var size = this.scale/2;
        p = p.scale(1/size);
        // NOTE: I calculate x,z cube coordinates and then calculate y
        // from those. That's because hex to pixel and pixel to hex
        // are defined in terms of axial coordinates, not cube
        // coordinates, and I am converting from axial to cube here.
        if (orientation) {
            var q = Math.sqrt(3)/3 * p.x + -1/3 * p.y;
            var r = 2/3 * p.y;
            return new FractionalCube(q, -q-r, r);
        } else {
            var q = 2/3 * p.x;
            var r = -1/3 * p.x + Math.sqrt(3)/3 * p.y;
            return new FractionalCube(q, -q-r, r);
        }
    }
    
                                                              
    public function bounds() {
        var centers = Lambda.array(hexes.map(function(hex) { return hexToCenter(hex); }));
        var b1 = boundsOfPoints(polygonVertices());
        var b2 = boundsOfPoints(centers);

        return {
            minX: b1.minX + b2.minX,
            maxX: b1.maxX + b2.maxX,
            minY: b1.minY + b2.minY,
            maxY: b1.maxY + b2.maxY
        };
    }
    

    public function polygonVertices() {
        var points = [];
        for (i in 0...6) {
            var angle = 2 * Math.PI * (2*i - (orientation? 1 : 0)) / 12;
            points.push(new ScreenCoordinate(0.5 * scale * Math.cos(angle),
                                             0.5 * scale * Math.sin(angle)));
        }
        return points;
    }
    
        
    static public function boundsOfPoints(points:Array<ScreenCoordinate>) {
        var minX = 0.0, minY = 0.0, maxX = 0.0, maxY = 0.0;
        for (p in points) {
            if (p.x < minX) { minX = p.x; }
            if (p.x > maxX) { maxX = p.x; }
            if (p.y < minY) { minY = p.y; }
            if (p.y > maxY) { maxY = p.y; }
        }
        return {minX: minX, maxX: maxX, minY: minY, maxY: maxY};
    }


    // These functions translate cubes into various hex coordinates

    static public function twoAxisToCube(hex:Hex) {
        return new Cube(hex.q, -hex.r-hex.q, hex.r);
    }

    static public function cubeToTwoAxis(cube:Cube) {
        return new Hex2d(Std.int(cube.x), Std.int(cube.z));
    }
    
    static public function oddQToCube(hex:Hex) {
        var x = hex.q, z = hex.r - ((hex.q - (hex.q & 1)) >> 1);
        return new Cube(x, -x-z, z);
    }

    static public function cubeToOddQ(cube:Cube) {
        var x = Std.int(cube.x), z = Std.int(cube.z);
        return new Hex2d(x, z + ((x - (x & 1)) >> 1));
    }

    static public function evenQToCube(hex:Hex2d) {
        var x = hex.q, z = hex.r - ((hex.q + (hex.q & 1)) >> 1);
        return new Cube(x, -x-z, z);
    }

    static public function cubeToEvenQ(cube:Cube) {
        var x = Std.int(cube.x), z = Std.int(cube.z);
        return new Hex2d(x, z + ((x + (x & 1)) >> 1));
    }

    static public function oddRToCube(hex:Hex2d) {
        var z = hex.r, x = hex.q - ((hex.r - (hex.r & 1)) >> 1);
        return new Cube(x, -x-z, z);
    }

    static public function cubeToOddR(cube:Cube) {
        var x = Std.int(cube.x), z = Std.int(cube.z);
        return new Hex2d(x + ((z - (z & 1)) >> 1), z);
    }

    static public function evenRToCube(hex:Hex2d) {
        var z = hex.r, x = hex.q - ((hex.r + (hex.r & 1)) >> 1);
        return new Cube(x, -x-z, z);
    }

    static public function cubeToEvenR(cube:Cube) {
        var x = Std.int(cube.x), z = Std.int(cube.z);
        return new Hex2d(x + ((z + (z & 1)) >> 1), z);
    }


    // These functions generate various shapes of hex maps
    
    static public function trapezoidalShape(minQ:Int, maxQ:Int,
                                            minR:Int, maxR:Int,
                                            toCube):Array<Cube> {
        var hexes = [];
        for (q in minQ...maxQ+1) {
            for (r in minR...maxR+1) {
                hexes.push(toCube(new Hex2d(q, r)));
            }
        }
        return hexes;
    }


    static public function triangularShape(size:Int):Array<Cube> {
        var hexes = [];
        for (k in 0...size+1) {
            for (i in 0...k+1) {
                hexes.push(new Cube(i, -k, k-i));
            }
        }
        return hexes;
    }

    
    static public function hexagonalShape(size:Int):Array<Cube> {
        var hexes = [];
        for (x in -size...size+1) {
            for (y in -size...size+1) {
                var z = -x-y;
                if (Math.abs(x) <= size && Math.abs(y) <= size && Math.abs(z) <= size) {
                    hexes.push(new Cube(x, y, z));
                }
            }
        }
        return hexes;
    }
}