using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace TA_W2W
{
    public class PlaceableManager : MonoBehaviour
    {
        [Header("AR")]
        [SerializeField] private ARPlaneManager planeManager;

        [Header("Raycast")]
        [SerializeField] private ARRaycaster raycaster;

        [Header("Placeables")]
        [SerializeField] private PlaceableFactory factory;
        [SerializeField] private PlaceablesList list;

        [Header("UI")]
        [SerializeField] private PlaceableButton placeableButtonPrefab;
        [SerializeField] private Transform placeableButtonContainer;
        [SerializeField] private GameObject placeableSelectMenu;
        [SerializeField] private GameObject mainMenu;

        [Header("Testing")]
        [SerializeField] private bool isTesting;
        [SerializeField] private Placeable testPlaceable;

        private Placeable selectedPlaceable;
        private void Start()
        {
            raycaster.OnRaycastHit += CreatePlaceable;

            foreach (var placeable in list.placeables)
            {
                Instantiate(placeableButtonPrefab, placeableButtonContainer).Init(placeable, () => 
                {
                    selectedPlaceable = placeable;

                    placeableSelectMenu.SetActive(false);

                    planeManager.enabled = true;
                    
                    raycaster.CanRaycast = true;
                });
            }
        }

        public void CreatePlaceable(Vector3 position)
        {
            factory.Create(isTesting ? testPlaceable : selectedPlaceable, position);

            mainMenu.SetActive(true);

            raycaster.CanRaycast = false;

            planeManager.enabled = false;

            foreach (var plane in planeManager.trackables)
            {
                Destroy(plane.gameObject);
            }
        }
    }
}
