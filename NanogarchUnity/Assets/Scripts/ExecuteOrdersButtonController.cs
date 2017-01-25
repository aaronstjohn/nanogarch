using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Entitas;

public class ExecuteOrdersButtonController : MonoBehaviour {
	Contexts contexts;
	Button _button;
	Group _entitiesUnderOrders;
	void Awake()
    {
    	_button = GetComponent<Button>();

    }
	// Use this for initialization
	void Start () {
		contexts = Contexts.sharedInstance;
        contexts.uI.uIModeEntity.isExecuteOrdersButtonEnabled = true;
		_button.onClick.AddListener(ExecuteCommands);
		_entitiesUnderOrders = contexts.core.GetGroup(CoreMatcher.Orders);
     
	}
	void ExecuteCommands()
	{
		Debug.Log("Executing Commands!");
		contexts.uI.uIModeEntity.isExecuteOrdersButtonEnabled = false;
		gameObject.GetEntity().isDestroy = true;
		foreach(var e in _entitiesUnderOrders.GetEntities())
			e.isExecutingOrders = true;
		

	}
	
	
}
