using UnityEngine;
using UnityEngine.Events;

namespace TA_W2W
{
    public interface IPlaceable
    {
        void Place(Vector3 position);
        UnityAction OnPlace { get; set; }
    }
}
