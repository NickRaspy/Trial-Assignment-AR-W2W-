using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace TA_W2W
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARRaycaster : MonoBehaviour
    {
        private ARRaycastManager arRaycastManager;

        private readonly List<ARRaycastHit> hits = new();

        public UnityAction OnRaycastHitNothing { get; set; }
        public UnityAction<GameObject> OnRaycastHitObject { get; set; }
        public UnityAction<Vector3> OnRaycastHitARPlane { get; set; }

        private void Awake()
        {
            arRaycastManager = GetComponent<ARRaycastManager>();
        }

        private void Update()
        {
            if (IsInputDetected())
            {
                var ray = GetRayFromInput();
                if (ray.HasValue)
                {
                    RaycastCheck(ray.Value);
                }
            }
        }

        private bool IsInputDetected()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Input.GetMouseButtonUp(0);
#elif UNITY_ANDROID
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
#else
            return false;
#endif
        }

        private Ray? GetRayFromInput()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Camera.main.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID
            return Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#else
            return null;
#endif
        }

        private void RaycastCheck(Ray ray)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                OnRaycastHitObject?.Invoke(hit.collider.gameObject);
            }

            if (arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon) && hits[0].trackable.gameObject.activeInHierarchy)
            {
                OnRaycastHitARPlane?.Invoke(hits[0].pose.position);
            }
            else
            {
                hits.Clear();
                if (hit.collider == null)
                {
                    OnRaycastHitNothing?.Invoke();
                }
            }
        }
    }
}