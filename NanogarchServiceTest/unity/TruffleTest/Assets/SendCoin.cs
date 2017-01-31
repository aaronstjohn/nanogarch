using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SendCoin : MonoBehaviour {

	[DllImport("__Internal")]
    private static extern void SendCoins(string address,int amount);

	InputField address;
	InputField amount; 

	Button sendCoins;
	void Awake()
	{

	}
	// Use this for initialization
	void Start () {
		address = GameObject.Find("AddressInputField").GetComponent<InputField>();
		amount = GameObject.Find("AmountInputField").GetComponent<InputField>();
		// Text t = addressText.GetComponent<Text>();
		address.text = "0xdfa0ec4490b826d0f7ed615ed06ba7f94a008a06";
		amount.text = "10";

		sendCoins = GetComponent<Button>();
		sendCoins.onClick.AddListener(HandleSendCoins);

	}
	void HandleSendCoins()
	{
		Debug.Log(string.Format("Sending {0} Metacoins to {1}",amount.text,address.text));
		SendCoins(address.text,Int32.Parse(amount.text));
	}
	// Update is called once per frame
	void Update () {
		
	}
}
