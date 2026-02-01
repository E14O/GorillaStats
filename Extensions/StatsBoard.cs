using GorillaServerStats.Behaviours;
using TMPro;
using UnityEngine;

namespace GorillaServerStats.Extensions
{
    public class StatsBoard : MonoBehaviour
    {
        TextMeshPro _text;

        void Start() => _text = gameObject.GetComponent<TextMeshPro>();
        void Update() => _text.text = ServerStatsManager.Instance.GetBoardText();
    }
}
