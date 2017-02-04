using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

using Newtonsoft.Json;
public class RPCAdd : MonoBehaviour {
	void Start() {
        System.Net.ServicePointManager.Expect100Continue=false;
        // StartCoroutine(GetText());
        //'{"jsonrpc":"2.0","method":"eth_accounts","params":[],"id":1}'
        StartCoroutine(Post("http://localhost:8545",
        "{\"jsonrpc\":\"2.0\",\"method\":\"eth_accounts\",\"params\":[],\"id\":1}"));
    }
//     request                 = new UnityWebRequest(url);
//    request.uploadHandler   = new UploadHandlerRaw(myStringToByteArrayConverter(body));
//    request.downloadHandler = new DownloadHandlerBuffer();
//    request.method          = UnityWebRequest.kHttpVerbPOST;
    IEnumerator Post(string url,string json)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);//GetBytes(postData);
       using (UnityWebRequest www = new UnityWebRequest(url))
        {
            www.uploadHandler   = new UploadHandlerRaw(bytes);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.method          = UnityWebRequest.kHttpVerbPOST;
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
           
            yield return www.Send();
 
            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
	void DoListUsers()
	{
		// StartCoroutine(GetText());
		//  GetRequest("http://127.0.0.1:8081/listUsers")   // Schedule an async operation.
        //         .Then(result =>                 // Use Done to register a callback to handle completion of the async operation.
        //         {
        //             Debug.Log("Async operation completed.");
        //             Debug.Log(result);
                   
        //         })
        //         .Done();

        //     Debug.Log("Waiting For Get request to complete");
	}
	// static IPromise<string> GetRequest(string url)
	// {
	// 	Debug.Log("Get Requesting:  " + url + " ...");

	// 	var promise = new Promise<string>();
	// 	using(UnityWebRequest www = UnityWebRequest.Get(url)) {
    //         // yield return www.Send();
     
    //         // if(www.isError) {
    //         //     Debug.Log(www.error);
    //         // }
    //         // else {
    //         //     // Show results as text
    //         //     Debug.Log(www.downloadHandler.text);
     
    //         //     // Or retrieve results as binary data
    //         //     byte[] results = www.downloadHandler.data;
    //         // }
    //     }
	// 	// using (var client = new WebClient())
	// 	// {
	// 	// 	client.DownloadStringCompleted +=
	// 	// 		(s, ev) =>
	// 	// 		{
	// 	// 			if (ev.Error != null)
	// 	// 			{
	// 	// 				Console.WriteLine("An error occurred... rejecting the promise.");

	// 	// 				// Error during download, reject the promise.
	// 	// 				promise.Reject(ev.Error);
	// 	// 			}
	// 	// 			else
	// 	// 			{
	// 	// 				Console.WriteLine("... Download completed.");

	// 	// 				// Downloaded completed successfully, resolve the promise.
	// 	// 				promise.Resolve(ev.Result);
	// 	// 			}
	// 	// 		};

	// 	// 	client.DownloadStringAsync(new Uri(url), null);
	// 	// }
	// 	return promise;
	// }
    IEnumerator GetText() {
        using(UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8081/listUsers")) {
            yield return www.Send();
     
            if(www.isError) {
                Debug.Log(www.error);
            }
            else {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
     
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }
	
}
// using UnityEngine;
// using Unity_RPC;
// using System;
// using System.Collections.Generic;
// using UnityEngine.UI;
// using UnityEngine.Networking;

// class TestClientNetworking : ClientNetworking
// {
// 	public TestClientNetworking(IRPCParser parser, string ip,int port):base(parser,ip,port)
// 		{}
// 	 protected override void OnConnected(NetworkMessage message) {        
//             Debug.Log("CONNECT");
//         }

//         protected override void OnDisconnected(NetworkMessage message) {
//             Debug.Log("DISCONNECT");
//         }

//         protected override void OnError(NetworkMessage message) {          
//             Debug.Log("ERROR");
//         }
// }
// public class RPCAdd : MonoBehaviour {
    
//     public Text label;

//     ClientNetworking _client;
// 	// Use this for initialization
// 	void Start () {
//         RPCHandler handler = new RPCHandler();
//         RPCParser parser = new RPCParser(handler);

//         _client = new TestClientNetworking(parser,"localhost",8000);
//         _client.connect();
// 		Debug.Log("SHOULD BE CONNECTED TO A SERVER!");
//         handler.addResponseListener("numberResult",handleResult,handleError);
               
// 	}
	

//     public void add()
//     {
//         _client.sendRequest("add","numberResult",new Dictionary<string,object>()
//             {
//                 {"x",2},
//                 {"y",1}
//             });
//     }

//     void handleResult(object result)
//     {
//         int total = Int32.Parse(result.ToString());
//         label.text = total.ToString();
//     }

//     void handleError(int code,string message,IDictionary<string,object> data)
//     {
//         Debug.LogError(message);
//     }
// }
