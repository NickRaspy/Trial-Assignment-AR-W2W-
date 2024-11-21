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
        [SerializeField] private string ignoreTag;

        private ARRaycastManager arRaycastManager;
        private List<ARRaycastHit> hits = new();

        public UnityAction OnRaycastHitNothing { get; set; }

        public UnityAction<GameObject> OnRaycastHitObject {  get; set; }

        public UnityAction<Vector3> OnRaycastHitARPlane {  get; set; }

        private Ray ray;

        void Awake()
        {
            arRaycastManager = GetComponent<ARRaycastManager>();
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastCheck(ray);
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(touch.position);
                }
            }
            else
            {
                RaycastCheck(ray);
            }
#endif
            ray = new();
        }

        private void RaycastCheck(Ray ray)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                OnRaycastHitObject(hit.collider.gameObject);
            }

            if (arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                OnRaycastHitARPlane(hits[0].pose.position);
            }

            if (hits.Count > 0 || hit.collider != null) return;
            else OnRaycastHitNothing();
        }
    }
}
