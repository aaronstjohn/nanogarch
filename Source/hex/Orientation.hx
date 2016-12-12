package hex;

class Orientation
{
    public function new(f0:Float, f1:Float, f2:Float, f3:Float, b0:Float, b1:Float, b2:Float, b3:Float, start_angle:Float)
    {
        this.f0 = f0;
        this.f1 = f1;
        this.f2 = f2;
        this.f3 = f3;
        this.b0 = b0;
        this.b1 = b1;
        this.b2 = b2;
        this.b3 = b3;
        this.start_angle = start_angle;
    }
    public var f0:Float;
    public var f1:Float;
    public var f2:Float;
    public var f3:Float;
    public var b0:Float;
    public var b1:Float;
    public var b2:Float;
    public var b3:Float;
    public var start_angle:Float;
}