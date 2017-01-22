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
		// var pickedUnit = context.GetPickedUnit();
    	var group = context.GetGroup(Matcher.AllOf(CoreMatcher.ReceivingOrders,CoreMatcher.Unit)); //.OnEntityAdded += (group, entity, index, component) =>
       	var pickedUnit = group.GetSingleEntity();
       	_commandDropdown.ClearOptions();
       	_commandDropdown.options.Add (new Dropdown.OptionData() {text="No Orders"});
        
        foreach(var c in pickedUnit.commands)
        {
            ICommand command = c as ICommand;
            _commandDropdown.options.Add (new Dropdown.OptionData() {text=command.GetCommandName()});
        }
        //Forces a refresh of the dropdown 
	     _commandDropdown.value = 1;
	     _commandDropdown.value = 0;
	     _commandDropdown.onValueChanged.AddListener(commandSelected);
     
	}
	private void commandSelected(int selected) {
     Debug.Log("selected: "+_commandDropdown.options[selected].text);
 	}
	
}
