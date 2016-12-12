package nanogarch;
import hex.*;
import minject.Injector;
import Type;

class Main {
  static public function main():Void {

  	var injector:Injector = Nanogarch.configure();
  	var game:Nanogarch = injector.getInstance(Nanogarch);
  	game.initialize();

  }
}