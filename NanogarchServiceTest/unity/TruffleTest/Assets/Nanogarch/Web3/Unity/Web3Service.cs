using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Promises;

namespace Nanogarch.Web3.Unity {
    public static class Web3Extensions {
    }

    public class Web3Service : MonoBehaviour {
       
        public int requests { get { return _requests; } }
        public string provider {get {return _provider;}}
        static Web3Service _service;
        
         int _requests;
         string _provider = "http://localhost:8545";

        public static Web3Service getService() {
            if (_service == null) {
                _service = new GameObject("Web3Service").AddComponent<Web3Service>();
                DontDestroyOnLoad(_service);
                _service.updateName();
            }

            return _service;
        }
        public Promise<string> Request(string payload)
        {
            Promise<string> promise = Web3Post(_provider,payload);
            _requests++;
            updateName();
            promise.ThenCoroutine<string>(requestComplete);
            return promise;
        }
        static Promise<string> Web3Post(string providerUrl,string payload) {
            return Promise.WithCoroutine<string>(() => Web3PostCoroutine(providerUrl,payload));
           
        }
        IEnumerator requestComplete(string result)
        {
                _requests--;
                updateName();
                yield return result;
        }
        static IEnumerator Web3PostCoroutine(string providerUrl,string payload)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(payload);
            using (UnityWebRequest www = new UnityWebRequest(providerUrl))
            {
                www.uploadHandler   = new UploadHandlerRaw(bytes);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.method          = UnityWebRequest.kHttpVerbPOST;
                www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            
                yield return www.Send();
    
                if (www.isError)
                {
                    Debug.Log(www.error);
                    yield return www.error;
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    yield return www.downloadHandler.text;
                }
            }
        }

        void updateName() {
            name = "Web3Service Requests (" + _requests + " pending)";
        }

        void OnDestroy() {
            _service = null;
        }
    }

}
