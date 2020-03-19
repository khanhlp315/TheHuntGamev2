using System.Collections;
using System.Net.NetworkInformation;
using System.Text;
using TheHuntGame.Network.Data;
using TheHuntGame.Utilities;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine;

namespace TheHuntGame.Network
{
    public class Network : MonoSingleton<Network, NetworkConfig>
    {
        private string _macAddress;
        public override void Initialize()
        {
            _macAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
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

        public void CreatePlayer(string playerName, UnityAction<PlayerData> onDone = null, UnityAction<long> onError = null)
        {
            var requestBody = $"{{\"playerName\": \"{playerName}\"}}";
            StartCoroutine(SendPostRequest(_config.CreatePlayerPath, (json, responseCode) =>
            {
                if (responseCode == 200)
                {
                    var playerData = PlayerData.FromJson(json);
                    onDone?.Invoke(playerData);
                }
                else
                {
                    onError?.Invoke(responseCode);
                }
            }, requestBody));
        }

        public void CreateGame(long playerId, UnityAction<GameData> onDone = null, UnityAction<long> onError = null)
        {
            var requestBody = $"{{\"playerId\": {playerId},\r\n  \"machineIdOrMacAddress\": \"{_macAddress}\"}}";
            StartCoroutine(SendPostRequest(_config.CreateGamePath, (json, responseCode) =>
            {
                if (responseCode == 200)
                {
                    var gameData = GameData.FromJson(json);
                    onDone?.Invoke(gameData);
                }
                else
                {
                    Debug.Log(json);
                    onError?.Invoke(responseCode);
                }
            }, requestBody));
        }

    }
}