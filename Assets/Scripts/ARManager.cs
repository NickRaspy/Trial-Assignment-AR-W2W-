using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace TA_W2W
{
    public class ARManager : MonoBehaviour
    {
        [Header("AR")]
        [SerializeField] private ARSession ARSession;
        [SerializeField] private ARPlaneManager planeManager;

        [Header("Raycast")]
        [SerializeField] private ARRaycaster raycaster;

        public void SetActionOnRaycaster(UnityAction<Vector3> ARaction, UnityAction<GameObject> objectAction, UnityAction nothingAction)
        {
            raycaster.OnRaycastHitARPlane += ARaction;
            raycaster.OnRaycastHitObject += objectAction;
            raycaster.OnRaycastHitNothing += nothingAction;
        }

        public void TrackablesReset()
        {
            planeManager.enabled = false;

            foreach (var plane in planeManager.trackables)
            {
                Destroy(plane.gameObject);
            }

            ARSession.Reset();
        }

        public void EnablePlaneDetection() => planeManager.enabled = true;
    }
}
