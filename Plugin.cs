using System;
using System.Collections;
using System.Collections.Specialized;
using BepInEx;
using Photon.Pun;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GorillaNetworking;

namespace GorillaServerStats
{
    [BepInPlugin(Constants.GUID, Constants.PluginName, Constants.Version)]

    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;

        public GameObject _forestSign;
        public TextMeshPro _signText;

        Coroutine _timerCoroutine;
        TimeSpan _time = TimeSpan.FromSeconds(0);
        string _playTime = "00:00:00";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                _timerCoroutine = StartCoroutine(Timer());
            }
            else
            {
                Logger.LogError("[ServerStats] Multiple instances detected. Destroying...");
                Destroy(gameObject);
                return;
            }
        }

        void Start()
        {
            GorillaTagger.OnPlayerSpawned(OnGameInitialized);
        }

        public string boardStatsUpdate()
        {
            if (PhotonNetwork.CurrentRoom == null)
            {
                return "Hello! Thank you for using ServerStats!\r\n\r\nPlease join a room for stats to appear!";
            }
            else
            {
                var _lobbyCode = PhotonNetwork.CurrentRoom.Name;
                int _playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
                var _master = PhotonNetwork.MasterClient;
                int _totalPlayerCount = NetworkSystem.Instance.GlobalPlayerCount();
                var _totalLobbies = "";

                if (!System.IO.File.Exists("./config.json"))
                {
                    System.IO.File.WriteAllText("./config.json", "{\"totalLobbies\":\"0\"}");
                    return "Hello! Thank you for using ServerStats!\r\n\r\nPlease join a room for stats to appear!\r\n\r\nPLAY TIME: " + _playTime;
                }

                string config = System.IO.File.ReadAllText("./config.json");
                NameValueCollection configCollection = System.Web.HttpUtility.ParseQueryString(config);
                _totalLobbies = configCollection["totalLobbies"];

                return "LOBBY CODE: " + _lobbyCode + " | TOTAL LOBBYS: " + "ERROR" +
                    "\r\nPLAYERS: " + _playerCount +
                    "\r\nMASTER: " + _master.NickName +
                    "\r\nACTIVE PLAYERS: " + _totalPlayerCount +
                    "\r\nPLAY TIME: " + _playTime +
                    "\r\nPING: " + PhotonNetwork.GetPing();
            }

        }
        void OnGameInitialized()
        {
            _forestSign = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/ScreenText (1)");

            if (_forestSign == null)
                return;

            _signText = _forestSign.GetComponent<TextMeshPro>();
            if (_signText == null)
                return;

            _signText.text = boardStatsUpdate();
        }

        void Update()
        {
            if (_forestSign != null)
                _signText.text = boardStatsUpdate();
        }

        public void OnJoin(string gamemode)
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            if (_forestSign != null)
            {
                _signText = _forestSign.GetComponent<TextMeshPro>();
                _signText.text = boardStatsUpdate();
            }

            if (!System.IO.File.Exists("./config.json"))
                System.IO.File.WriteAllText("./config.json", "{\"totalLobbies\":\"0\"}");

            string config = System.IO.File.ReadAllText("./config.json");
            NameValueCollection configCollection = System.Web.HttpUtility.ParseQueryString(config);
            int totalLobbies = Int32.Parse(configCollection["totalLobbies"]);
            totalLobbies++;
            configCollection["totalLobbies"] = totalLobbies.ToString();

            if (!System.IO.File.Exists("./config.json"))
                System.IO.File.WriteAllText("./config.json", configCollection.ToString());
        }

        public void OnLeave(string gamemode)
        {
            if(_signText != null)
                _signText.text = "WELCOME TO GORILLA TAG!\r\n\r\nPLEASE JOIN A ROOM FOR STATS TO APPEAR!";
        }

        IEnumerator Timer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                _time = _time.Add(TimeSpan.FromSeconds(1));
                _playTime = _time.ToString(@"hh\:mm\:ss");

                if (_forestSign != null)
                    _signText.text = boardStatsUpdate();
            }
        }
    }
}