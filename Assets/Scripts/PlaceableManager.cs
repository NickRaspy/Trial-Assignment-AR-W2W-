using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TA_W2W
{
    public class PlaceableManager : MonoBehaviour
    {
        [Header("Placeables")]
        [SerializeField] private PlaceableFactory factory;
        [SerializeField] private PlaceablesList list;

        [Header("Point")]
        [SerializeField] private GameObject arrowPoint;

        [Header("Testing")]
        [SerializeField] private bool isTesting;
        [SerializeField] private Placeable testPlaceable;

        private Placeable selectedPlaceable;
        private Placeable temporaryPlaceable;

        public UnityAction OnPlacePrepare { get; set; }
        public UnityAction OnPlace { get; set; }
        public UnityAction<Placeable> OnPlaceableSet { get; set; }

        public void Init(UnityAction<Placeable> onPlaceablesInit)
        {
            if (onPlaceablesInit == null) return;
            foreach (var placeable in list.placeables)
            {
                onPlaceablesInit(placeable);
            }
        }

        public void SetPlaceable(Placeable placeable)
        {
            if (selectedPlaceable == placeable) return;

            arrowPoint.SetActive(placeable != null);
            if (placeable != null)
            {
                SetArrowPosition(placeable.GetPointPosition());
            }

            selectedPlaceable = placeable;
            OnPlaceableSet?.Invoke(placeable);
        }

        public void PreparePlaceable(Placeable placeable)
        {
            temporaryPlaceable = placeable;

            PreparePlace();
        }

        public void PreparePlace() => OnPlacePrepare?.Invoke();

        public void CreatePlaceable(Vector3 position)
        {
            var placeableToCreate = isTesting ? testPlaceable : temporaryPlaceable;
            if (placeableToCreate == null) return;

            factory.Create(placeableToCreate, position);
            SetPlaceable(factory.GetLastPlaceable());
            OnPlace?.Invoke();
            temporaryPlaceable = null;
        }

        public void DestroySelectedPlaceable()
        {
            if (selectedPlaceable == null) return;

            factory.RemovePlaceable(selectedPlaceable);
            Destroy(selectedPlaceable.gameObject);
            OnPlaceableSet?.Invoke(null);
            arrowPoint.SetActive(false);
        }

        public void MovePlaceable(Vector3 position)
        {
            if (selectedPlaceable == null) return;

            selectedPlaceable.Place(position);
            SetArrowPosition(selectedPlaceable.GetPointPosition());
            OnPlace?.Invoke();
        }

        private void SetArrowPosition(Vector3 position) => arrowPoint.transform.position = position;

        public void ChangeColor(Color color)
        {
            if (selectedPlaceable is Customizable customizable)
            {
                customizable.Color = color;
            }
        }

        public Color GetColor() => selectedPlaceable is Customizable customizable ? customizable.Color : Color.white;

        public void PlayAnimation(string name)
        {
            if (selectedPlaceable is Animatable animatable)
            {
                animatable.SetAnimation(name);
            }
        }

        public bool IsCustomizable() => selectedPlaceable is Customizable;

        public bool IsAnimatable() => selectedPlaceable is Animatable;

        public IEnumerable<Placeable> GetPlaceables() => list.placeables;
    }
}