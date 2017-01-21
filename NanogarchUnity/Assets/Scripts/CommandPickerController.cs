using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Entitas;

public class CommandPickerController : MonoBehaviour {

	Dropdown _commandDropdown;
	Context context;
	void Awake()
    {
    	_commandDropdown = GetComponent<Dropdown>();
    }
	// Use this for initialization
	void Start () {
		context = Contexts.sharedInstance.core;
		
    	context.GetGroup(CoreMatcher.Picked).OnEntityAdded += (group, entity, index, component) =>
        {
        	if(! (entity.isUnit && entity.hasCommands) )
        		return;
        	_commandDropdown.ClearOptions();

            foreach(var c in entity.commands)
            {
                ICommand command = c as ICommand;
                _commandDropdown.options.Add (new Dropdown.OptionData() {text=command.GetCommandName()});
            }
            //Forces a refresh of the dropdown 
		     _commandDropdown.value = 1;
		     _commandDropdown.value = 0;
            
        };
	}
	
}
