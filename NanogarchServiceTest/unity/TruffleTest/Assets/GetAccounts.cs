
using UnityEngine;
using Promises;
using Nanogarch.Web3;
public class GetAccounts : MonoBehaviour {

	 void Awake() {
        MainThreadDispatcher.Init();
    }
	// Use this for initialization
	void Start () {
		
	}
	public void GetEthAccounts()
	{
		Debug.Log("NEED TO HANDLE CUSTOM CONVERSION OF ADDRESSES");
		Eth.Accounts()
			.Then<Nothing>((Address[] response)=>{
				foreach( Address addy in response )
				{
					Debug.Log("FOUND ADDRESS: "+ addy);
				}
				return Nothing.AtAll;
			});
		Eth.Coinbase().Then<Nothing>((Address response)=>{
			Debug.Log("COINBASE ADDRESS IS: "+response );
			return Nothing.AtAll;
		});
	}
	// Update is called once per frame
	void Update () {
		
	}
}
