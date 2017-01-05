package nanogarch;
import ash.core.Engine;
import ash.core.Entity;
import ash.tick.ITickProvider;
import ash.tick.FrameTickProvider;
import nanogarch.creators.MapCellCreator;
import nanogarch.util.InjectiveEntity;
import minject.Injector;
import openfl.display.DisplayObjectContainer;

import nanogarch.systems.*;
import nanogarch.graphics.*;
import nanogarch.components.*;
import nanogarch.model.*;
import nanogarch.creators.*;
import nanogarch.commands.*;

class ConfigurationFactory
{
	public var injector:CommandBinder;

	public function new(container:DisplayObjectContainer,viewWidth:Int,viewHeight:Int)
	{
		injector 				= new CommandBinder();
		var config:GameConfig 	= new GameConfig();
		config.viewWidth 	= viewWidth;
		config.viewHeight 	= viewHeight;

		injector.map(GameConfig).toValue(config);
		injector.map(StyleConfig).toValue(new StyleConfig());

		injector.map(Engine).asSingleton();
		injector.map(DisplayObjectContainer,"GameDisplayObject").toValue(container);
		injector.map(FrameTickProvider).toValue(new FrameTickProvider(container));
		injector.map(Injector).toValue(injector);

	}
	public function configureSystems()
	{
		injector.map(MapGridSystem).toClass(MapGridSystem);
		injector.map(RenderSystem).toClass(RenderSystem);
		return this;

	}
	public function configureEntities()
	{
		injector.map(Entity).toClass(InjectiveEntity);
		injector.map(Terrain).toClass(Terrain);
		injector.map(Frame).toClass(Frame);
		injector.map(MapGridFrame).toClass(MapGridFrame);
		injector.map(Display).toClass(Display);
		injector.map(Collider).toClass(Collider);


		injector.map(ShapeView).toClass(ShapeView);

		return this;
	}
	public function configureMap()
	{
		injector.map(MapCellCreator).toClass(MapCellCreator);
		injector.map(MapGridCreator).toClass(MapGridCreator);
		injector.map(RandomTileCreator).toClass(RandomTileCreator);
 		injector.map(MapCell).toClass(MapCell);
 		injector.map(MapGrid).toClass(MapGrid);
 		return this;
 	// 	var grid = new MapGrid();
		
		// injector.injectInto(grid);
		// grid.initialize();
		// injector.map(MapGrid).toValue(grid);

	}
	public function generateRandomMap()
	{
		var mapGridCreator = injector.getInstance(MapGridCreator);
		var mapGrid:MapGrid = mapGridCreator.create();
		injector.map(MapGrid,"MainMap").toValue(mapGrid);
		return this;
	}

	public function configureSignals()
	{
		injector.bind(Signals.StartSignal,StartCommand).toClass(StartCommand);
		injector.bind(Signals.OverMapCellSignal,MapCellInfoCommand).toClass(MapCellInfoCommand);

		return this;

	}
	///Major test cases 
	// 0.) Generate a world map 
	// 1.) Hovering
	//		// Hoving on the grid world to show a UI display of terrain info 

	// 2.) Clicking on something in the game world, triggering a UI interaction.
			// Click on the grid world to bring up unit options 
	// 3.) Movement control
	//		One of the unit options will be movment, include movment range options 
	//		What does a movement range interaction look like:
	//			// Tap the unit
	//			// Get an overlay on the gridworld of its movment options computed based on the terrain
	//			// click a valid move square 
	//			// Unit moves 
}
// public static function configure_old(container:DisplayObjectContainer,viewWidth:Int,viewHeight:Int)
// 	{
// 		var injector:Injector = new Injector();
		
// 		var config:GameConfig = new GameConfig();
// 		config.viewWidth = viewWidth;
// 		config.viewHeight = viewHeight;
		
// 		var styleConfig:StyleConfig = new StyleConfig();

// 		injector.map(GameConfig).toValue(config);
// 		injector.map(StyleConfig).toValue(styleConfig);
// 		injector.map(Engine).asSingleton();
// 		injector.map(EntityCreator).asSingleton();
// 		injector.map(Entity).toClass(InjectiveEntity);
// 		injector.map(WorldCellView).toClass(WorldCellView);
// 		injector.map(UnitView).toClass(UnitView);

// 		injector.map(Frame).toClass(Frame);
// 		injector.map(HexFrame).toClass(HexFrame);

// 		injector.map(Collider).toClass(Collider);
// 		injector.map(Display).toClass(Display);
// 		injector.map(Terrain).toClass(Terrain);
// 		injector.map(Unit).toClass(Unit);

// 		injector.map(MapCellCreator).toClass(MapCellCreator);
// 		injector.map(MapCell).toClass(MapCell);
		
		
// 		injector.map(DisplayObjectContainer,"GameDisplayObject").toValue(container);
// 		injector.map(RenderSystem).asSingleton();
// 		injector.map(CollisionSystem).asSingleton();
// 		injector.map(MapGridPositionSystem).asSingleton();
		
// 		// injector.map(TerrainInfoCommand).toClass(TerrainInfoCommand);


		
// 		// var terrainInfoSignalPair = new Signals.TerrainInfoSignalPair(Signal.trigger());
		

// 		// injector.map(Signals.TerrainInfoSignalPair).toValue(terrainInfoSignalPair);
		
// 		// var sig=injector.getInstance(Signals.TerrainInfoSignalPair);
		

// 		// sig.signal.handle(function(i:Injector){
// 		// 	var cmd = i.getInstance(TerrainInfoCommand);
// 		// 	i.injectInto(cmd);
// 		// 	cmd.Execute();
// 		// });
		


// 		injector.map(FrameTickProvider).toValue(new FrameTickProvider(container));
// 		injector.map(Injector).toValue(injector);
// 		injector.map(Nanogarch).asSingleton();


// 		//INJECT INTO 
// 		var grid = new MapGrid();
		
// 		injector.injectInto(grid);
// 		grid.initialize();
// 		injector.map(MapGrid).toValue(grid);
		

		
// 		return injector;
// 	}