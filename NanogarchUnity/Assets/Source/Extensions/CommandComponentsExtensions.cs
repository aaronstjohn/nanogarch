using Entitas;
using System.Collections;
using System.Collections.Generic;

namespace Entitas {
	public partial class Entity{
		static ComponentTypeMap _commandMap  = new ComponentTypeMap(typeof(ICommand),
		        												 CoreComponentIds.componentNames,
		        												 CoreComponentIds.componentTypes);
		 public bool hasCommands { get { return HasAnyComponent(_commandMap.componentIds); } }

		public IEnumerable<IComponent> commands {
			get{
				if(HasAnyComponent(_commandMap.componentIds))
				{
					foreach(int id in _commandMap.componentIds)
					{
						if(HasComponent(id))
						{
							yield return GetComponent(id);
						}
					}
				}
			}
		}

		
	}
}
public partial class CoreMatcher {

	static IMatcher _matcherCommands;

	public static IMatcher Commands {
	    get {
	        if(_matcherCommands == null) {
	        	var componentTypeMap  = new ComponentTypeMap(typeof(ICommand),
	        												 CoreComponentIds.componentNames,
	        												 CoreComponentIds.componentTypes);
	        	_matcherCommands = componentTypeMap.CreateMatcher();
	        	
	        }

	        return _matcherCommands;
	    }
	}
}