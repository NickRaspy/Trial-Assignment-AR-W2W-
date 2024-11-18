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

        [Header("Testing")]
        [SerializeField] private bool isTesting;
        [SerializeField] private Placeable testPlaceable;

        private Placeable selectedPlaceable;

        public UnityAction OnPlacePrepare {  get; set; }
        public UnityAction OnPlace { get; set; }
        public void Init(UnityAction<Placeable> onPlaceablesInit)
        {
            foreach (var placeable in list.placeables) onPlaceablesInit(placeable);
        }

        public void PlacePrepare(Placeable placeable)
        {
            selectedPlaceable = placeable;

            OnPlacePrepare?.Invoke();
        }

        public void CreatePlaceable(Vector3 position)
        {
            factory.Create(isTesting ? testPlaceable : selectedPlaceable, position);

            selectedPlaceable = factory.GetLastPlaceable();

            OnPlace?.Invoke();
        }

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

        public IEnumerable<Placeable> GetPlaceables()
        {
            return list.placeables;
        }
    }
}
