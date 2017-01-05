package nanogarch.components;

import openfl.display.Sprite;

class Display
{
    public var displayObject(default, null):Sprite;

    public function new()
    {
        
    }
    public function initialize(displayObject:Sprite):Display
    {
    	this.displayObject = displayObject;
    	this.displayObject.mouseEnabled =true;
    	return this;
    }
}
