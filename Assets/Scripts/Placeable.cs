using UnityEngine;
using UnityEngine.Events;

namespace TA_W2W
{
    public class Placeable : MonoBehaviour, IPlaceable
    {
        public UnityAction OnPlace { get; set; }

        public Sprite icon;

        //for animated arrow
        [SerializeField] private Transform point;

        public void Place(Vector3 position)
        {
            transform.position = position;

            OnPlace?.Invoke();
        }

        public Vector3 GetPointPosition() { return point.position; }
    }
}
