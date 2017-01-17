using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CreateFocusSpotlightSystem :  IInitializeSystem {
	readonly Context _context;
   public CreateFocusSpotlightSystem(Contexts contexts) 
	{
		_context= contexts.core;
	}

	public void Initialize() {
	
		_context.CreateEntity()
            .AddName("CursorSpotlight")
			.AddResource("Spotlight");
		_context.CreateEntity()
            .AddName("PolygonFocusSpotlight")
			.AddResource("Spotlight");

	}
}