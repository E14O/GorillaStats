using UnityEngine;

namespace GorillaServerStats.Behaviours.UI
{
    public class ScreenButton : MonoBehaviour
    {
        public void Start()
        {
            gameObject.SetLayer(UnityLayer.GorillaInteractable);
            GetComponent<Collider>().isTrigger = true;
        }

        public void OnTriggerEnter(Collider _other)
        {
            if (!_other.GetComponent<GorillaTriggerColliderHandIndicator>())
                return;

            ServerStatsManager.Instance.ChangeScreen();
        }
    }
}
