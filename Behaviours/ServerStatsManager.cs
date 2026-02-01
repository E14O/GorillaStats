using System;
using System.Collections;
using System.Linq;
using Photon.Pun;
using UnityEngine;


namespace GorillaServerStats.Behaviours
{
    public class ServerStatsManager : MonoBehaviourPunCallbacks
    {
        public static ServerStatsManager Instance;

        public int _totalJoinedRooms;

        TimeSpan _time;
        string _playTime = "00:00:00";
        Coroutine _timer;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public override void OnJoinedRoom()
        {
            _totalJoinedRooms++;
            _time = TimeSpan.Zero;

            if (_timer != null)
                StopCoroutine(_timer);

            _timer = StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            while (PhotonNetwork.InRoom)
            {
                yield return new WaitForSeconds(1);

                _time = _time.Add(TimeSpan.FromSeconds(1));
                _playTime = _time.ToString(@"hh\:mm\:ss");
            }
        }

        public string GetBoardText()
        {
            if (!PhotonNetwork.InRoom)
            {
                return "Hello! Thank you for using ServerStats!\n\nPlease join a room for stats to appear!\n\nOriginal: @its3rr0rgtag\nFixed by: @e14o";
            }

            string _currentRegion = PhotonNetwork.CloudRegion;
            string _removeSillyChar = new string(_currentRegion.Where(char.IsLetter).ToArray()).ToUpper();

            string _statText = $"LOBBY CODE: {PhotonNetwork.CurrentRoom.Name} | TOTAL LOBBYS: {_totalJoinedRooms}\n" +
                $"PLAYERS: {PhotonNetwork.CurrentRoom.PlayerCount}\n" +
                $"MASTER: {PhotonNetwork.MasterClient.NickName}\n" +
                $"ACTIVE PLAYERS: {NetworkSystem.Instance.GlobalPlayerCount()}\n" +
                $"PLAY TIME: {_playTime}\n" +
                $"PING: {PhotonNetwork.GetPing()}\n" +
                $"REGION: {_removeSillyChar.ToUpper()}";

            return _statText;
        }
    }
}
