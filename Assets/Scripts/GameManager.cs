using UnityEngine;

namespace TA_W2W
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlaceableManager placeableManager;
        [SerializeField] private ARManager ARManager;
        [SerializeField] private UIManager UIManager;

        public void Start()
        {
            ARInit();
            PlaceablesInit();
            UIInit();
        }

        private void ARInit()
        {
            ARManager.SetActionOnRaycaster(placeableManager.CreatePlaceable);
        }

        private void PlaceablesInit()
        {
            placeableManager.OnPlace += () =>
            {
                ARManager.TrackablesReset();
                UIManager.EnableMainMenu();
            };

            placeableManager.OnPlacePrepare += () =>
            {
                ARManager.EnablePlaneDetection();
                ARManager.EnableRaycaster();
            };

            placeableManager.Init(placeable =>
            {
                UIManager.CreatePlaceableButton(placeable, () => 
                {
                    placeableManager.PlacePrepare(placeable);
                });
            });
        }

        private void UIInit()
        {
            UIManager.Init();

            UIManager.ColorClickerSetup(placeableManager.GetColor, placeableManager.ChangeColor);
        }
    } 
}
