// Screen coordinates
// From http://www.redblobgames.com/articles/grids/
// Copyright 2012 Red Blob Games <redblobgames@gmail.com>
// License: Apache v2.0 <http://www.apache.org/licenses/LICENSE-2.0.html>
package hex;

@:keep @:expose class ScreenCoordinate {
    public var x:Float;
    public var y:Float;

    public function new(x, y) {
        this.x = x;
        this.y = y;
    }

    public function equals(p) { return x == p.x && y == p.y; }
    public function toString() { return x + "," + y; }
    public function length_squared() { return x * x + y * y; }
    public function length() { return Math.sqrt(length_squared()); }
    public function normalize() { var d = length(); return new ScreenCoordinate(x / d, y / d); }
    public function scale(d:Float) { return new ScreenCoordinate(x * d, y * d); }
    
    public function rotateLeft() { return new ScreenCoordinate(y, -x); }
    public function rotateRight() { return new ScreenCoordinate(-y, x); }
    
    public function add(p) { return new ScreenCoordinate(x + p.x, y + p.y); }
    public function subtract(p) { return new ScreenCoordinate(x - p.x, y - p.y); }
    public function dot(p) { return x * p.x + y * p.y; }
    public function cross(p) { return x * p.y - y * p.x; }
    public function distance(p) { return subtract(p).length(); }
}
