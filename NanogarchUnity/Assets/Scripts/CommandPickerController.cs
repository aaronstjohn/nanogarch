using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Entitas;

public class CommandPickerController : MonoBehaviour {

	Dropdown _commandDropdown;
	Context context;
	Entity _pickedUnit;
	Dictionary<int,CommandType> _optionMap;
	// EntityLink _link;
	void Awake()
    {
    	_commandDropdown = GetComponent<Dropdown>();
    	_optionMap = new Dictionary<int,CommandType>();
    	// _link = GetComponent<EntityLink>();
    	
    }
	// Use this for initialization
	void Start () {
		context = Contexts.sharedInstance.core;
		// var pickedUnit = context.GetPickedUnit();
    	var group = context.GetGroup(Matcher.AllOf(CoreMatcher.ReceivingOrders,CoreMatcher.Unit)); //.OnEntityAdded += (group, entity, index, component) =>
       	_pickedUnit = group.GetSingleEntity();
       	_commandDropdown.ClearOptions();
       	_commandDropdown.options.Add (new Dropdown.OptionData() {text="No Orders"});
        int option = 0;
        _optionMap.Add(option,CommandType.None);
        foreach(var c in _pickedUnit.commands)
        {
        	option ++;
            ICommand command = c as ICommand;
            _commandDropdown.options.Add (new Dropdown.OptionData() {text=command.GetCommandName()});
            _optionMap.Add(option,command.GetCommandType());
        }
        //Forces a refresh of the dropdown 
	     _commandDropdown.value = 1;
	     _commandDropdown.value = 0;
	     _commandDropdown.onValueChanged.AddListener(commandSelected);
     
	}
	private void commandSelected(int selected) {

     	Debug.Log("selected: "+_commandDropdown.options[selected].text);
     	_pickedUnit.AddCommandIssued(_optionMap[selected]);
     	gameObject.GetEntity().isDestroy = true;
 	}
	
}
