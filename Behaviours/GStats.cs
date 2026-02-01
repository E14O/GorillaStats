using GorillaServerStats.Extensions;
using UnityEngine;

namespace GorillaServerStats.Behaviours
{
    internal class GStats : MonoBehaviour
    {
        public void Start() => SetupStatBoards();

        void SetupStatBoards()
        {
            foreach (string _textPrefab in Constants.StatPrefab)
            {
                if (GameObject.Find(_textPrefab).GetComponent<StatsBoard>() == null)
                    GameObject.Find(_textPrefab).AddComponent<StatsBoard>();
            }
        }
    }
}
