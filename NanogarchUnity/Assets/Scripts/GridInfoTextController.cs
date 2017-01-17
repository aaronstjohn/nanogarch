using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Entitas;

public class GridInfoTextController : MonoBehaviour {

	public Text gridInfoText;
	Context context;
	void Awake()
    {
    	gridInfoText = GetComponent<Text>();
    }
	// Use this for initialization
	void Start () {
		context = Contexts.sharedInstance.core;
    	context.GetGroup(CoreMatcher.InFocus).OnEntityAdded += (group, entity, index, component) =>
        {
        	gridInfoText.text=entity.name.id;
            
        };
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
