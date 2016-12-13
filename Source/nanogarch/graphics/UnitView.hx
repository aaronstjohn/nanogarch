package nanogarch.graphics;

import openfl.display.Shape;

class UnitView extends Shape
{
    public function new()
    {
        super();
        graphics.beginFill(0x4295f4);
        graphics.moveTo(10, 0);
        graphics.lineTo(-7, 7);
        graphics.lineTo(-4, 0);
        graphics.lineTo(-7, -7);
        graphics.lineTo(10, 0);
        graphics.endFill();
    }

}