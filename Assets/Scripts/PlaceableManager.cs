using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.XR.ARFoundation;

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

        public UnityAction OnPlacePrepare {  get; set; }
        public UnityAction OnPlace { get; set; }
        public UnityAction<Placeable> OnPlaceableSet { get; set; }
        public void Init(UnityAction<Placeable> onPlaceablesInit)
        {
            foreach (var placeable in list.placeables) onPlaceablesInit(placeable);
        }

        public void SetPlaceable(Placeable placeable)
        {
            if (selectedPlaceable == placeable) return;

            arrowPoint.SetActive(placeable != null);

            if(placeable != null) SetArrowPosition(placeable.GetPointPosition());

            selectedPlaceable = placeable;

            OnPlaceableSet?.Invoke(placeable);
        }

        public void PlaceablePrepare(Placeable placeable)
        {
            temporaryPlaceable = placeable;

            PlacePrepare();
        }

        public void PlacePrepare() => OnPlacePrepare?.Invoke();

        public void CreatePlaceable(Vector3 position)
        {
            factory.Create(isTesting ? testPlaceable : temporaryPlaceable, position);

            SetPlaceable(factory.GetLastPlaceable());

            OnPlace?.Invoke();

            temporaryPlaceable = null;
        }

        public void DestroySelectedPlayable()
        {
            factory.RemovePlaceable(selectedPlaceable);

            Destroy(selectedPlaceable.gameObject);

            OnPlaceableSet?.Invoke(null);

            arrowPoint.SetActive(false);
        }

        public void MovePlaceable(Vector3 position)
        {
            selectedPlaceable.Place(position);

            SetArrowPosition(selectedPlaceable.GetPointPosition());

            OnPlace?.Invoke();
        }

        private void SetArrowPosition(Vector3 position) => arrowPoint.transform.position = position;

        public void ChangeColor(Color color)
        {
            if(selectedPlaceable is Customizable customizable) customizable.Color = color;
        }

        public Color GetColor()
        {
            if (selectedPlaceable is Customizable customizable) return customizable.Color;
            return Color.white;
        }

        public void PlayAnimation(string name)
        {
            if(selectedPlaceable is Animatable animatable) animatable.SetAnimation(name);
        }

        public bool IsCustomizable() { return selectedPlaceable is Customizable; }

        public bool IsAnimatable() { return selectedPlaceable is Animatable; }

        public IEnumerable<Placeable> GetPlaceables()
        {
            return list.placeables;
        }
    }
}
