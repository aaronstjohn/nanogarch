// Non-cube hex coordinates (q, r)
// From http://www.redblobgames.com/articles/grids/hexagons/
// Copyright 2013 Red Blob Games <redblobgames@gmail.com>
// License: Apache v2.0 <http://www.apache.org/licenses/LICENSE-2.0.html>
package hex;
@:keep @:expose class Hex2d {
    public var q:Int;
    public var r:Int;
    
    public function new(q, r) {
        this.q = q;
        this.r = r;
    }

    public function toString() {
        return q + ":" + r;
    }
}
