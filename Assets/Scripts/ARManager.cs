using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace TA_W2W
{
    public class ARManager : MonoBehaviour
    {
        [Header("AR Components")]
        [SerializeField] private ARPlaneManager planeManager;

        [Header("Raycasting")]
        [SerializeField] private ARRaycaster raycaster;

        public void ConfigureRaycaster(UnityAction<Vector3> onPlaneHit, UnityAction<GameObject> onObjectHit, UnityAction onNothingHit)
        {
            raycaster.OnRaycastHitARPlane += onPlaneHit;
            raycaster.OnRaycastHitObject += onObjectHit;
            raycaster.OnRaycastHitNothing += onNothingHit;
        }

        public void DisableTrackables()
        {
            planeManager.enabled = false;
            TogglePlanes(false);
        }

        public void EnablePlaneDetection()
        {
            TogglePlanes(true);
            planeManager.enabled = true;
        }

        private void TogglePlanes(bool isEnabled)
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(isEnabled);
            }
        }

        private void DestroyAllPlanes()
        {
            foreach (var plane in planeManager.trackables)
            {
                Destroy(plane.gameObject);
            }
        }

        private void OnDisable()
        {
            raycaster.OnRaycastHitARPlane -= null;
            raycaster.OnRaycastHitObject -= null;
            raycaster.OnRaycastHitNothing -= null;
        }
    }
}