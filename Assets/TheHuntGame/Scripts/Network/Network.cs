using System.Collections;
using System.Text;
using TheHuntGame.Network.Data;
using TheHuntGame.Utilities;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace TheHuntGame.Network
{
    public class Network: MonoSingleton<Network, NetworkConfig>
    {
        public override void Initialize()
        {
            OnInitializeDone?.Invoke();
        }

        private IEnumerator SendPostRequest(string path, UnityAction<string, long> onResponseReceived = null, string requestBody = null)
        {
            var downloadHandler = new DownloadHandlerBuffer();
            var request = new UnityWebRequest($"{_config.BaseUrl}/{path}", "POST")
            {
                downloadHandler = downloadHandler
            };

            if (requestBody != null)
            {
                var bodyRaw = Encoding.UTF8.GetBytes(requestBody);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.SetRequestHeader("Content-Type", "application/json");
            }
            yield return request.SendWebRequest();

            onResponseReceived?.Invoke(downloadHandler.text, request.responseCode);
        }
        
        private IEnumerator SendGetRequest(string path, UnityAction<string, long> onResponseReceived = null)
        {
            var downloadHandler = new DownloadHandlerBuffer();
            var request = new UnityWebRequest($"{_config.BaseUrl}/{path}", "GET")
            {
                downloadHandler = downloadHandler
            };
            yield return request.SendWebRequest();

            onResponseReceived?.Invoke(downloadHandler.text, request.responseCode);
        }
        
        public void CreatePlayer(UnityAction<PlayerData> onDone = null, UnityAction<long> onError = null)
        {
            StartCoroutine(SendPostRequest(_config.CreatePlayerPath, (json, responseCode) =>
            {
                if (responseCode == 200)
                {
                    var remoteConfig = PlayerDataResponse.FromJson(json).Get();
                    onDone?.Invoke(remoteConfig);
                }
                else
                {
                    onError?.Invoke(responseCode);
                }
            }));
        }
    }
}