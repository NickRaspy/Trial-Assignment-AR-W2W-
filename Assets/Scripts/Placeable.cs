using UnityEngine;
using UnityEngine.Events;

namespace TA_W2W
{
    public class Placeable : MonoBehaviour, IPlaceable
    {
        public UnityAction OnPlace { get; set; }

        public Sprite icon;

        [SerializeField] private Outline outline;

        public void Place(Vector3 position)
        {
            transform.position = position;

            OnPlace?.Invoke();
        }

        public void ToggleOutline(bool enable)
        {
            if(outline != null) outline.enabled = enable;
        }
    }
}
