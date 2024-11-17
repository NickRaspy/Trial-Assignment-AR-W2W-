using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

        public UnityAction<Vector3> OnRaycastHit {  get; set; }

        public bool CanRaycast { get; set; } = false;
        void Awake()
        {
            arRaycastManager = GetComponent<ARRaycastManager>();
        }

        void Update()
        {
            if (!CanRaycast) return;

            Ray ray = new();

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
#endif
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag(ignoreTag)) return;
            }

            if (arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                OnRaycastHit(hits[0].pose.position);
            }
        }
    }
}
