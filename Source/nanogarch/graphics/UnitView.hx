package nanogarch.graphics;

import openfl.display.Sprite;

class UnitView extends Sprite
{
    public function new()
    {
        super();
        
    }
    public function initialize()
    {
        graphics.beginFill(0xCC456D);
        graphics.moveTo(10, 0);
        graphics.lineTo(-7, 7);
        graphics.lineTo(-4, 0);
        graphics.lineTo(-7, -7);
        graphics.lineTo(10, 0);
        graphics.endFill();
        return this;
    }

}