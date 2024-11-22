using UnityEngine;

namespace TA_W2W
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlaceableManager placeableManager;
        [SerializeField] private ARManager ARManager;
        [SerializeField] private UIManager UIManager;

        private Mode mode = Mode.None;

        public void Start()
        {
            InitializeAR();
            InitializePlaceables();
            InitializeUI();
        }

        private void InitializeAR()
        {
            ARManager.ConfigureRaycaster(
                HandleRaycastPosition,
                HandleRaycastObject,
                HandleRaycastClear
            );
        }

        private void HandleRaycastPosition(Vector3 position)
        {
            switch (mode)
            {
                case Mode.Creation:
                    placeableManager.CreatePlaceable(position);
                    break;
                case Mode.Editing:
                    placeableManager.MovePlaceable(position);
                    break;
            }
        }

        private void HandleRaycastObject(GameObject obj)
        {
            if (mode != Mode.None) return;

            var placeable = obj.GetComponent<Placeable>();
            placeableManager.SetPlaceable(placeable);

            UpdatePlaceableUI();
        }

        private void HandleRaycastClear()
        {
            if (mode != Mode.None) return;

            placeableManager.SetPlaceable(null);
            UpdatePlaceableUI();
        }

        private void InitializePlaceables()
        {
            placeableManager.OnPlace += OnPlace;
            placeableManager.OnPlacePrepare += OnPlacePrepare;
            placeableManager.OnPlaceableSet += OnPlaceableSet;

            placeableManager.Init(placeable =>
            {
                UIManager.CreatePlaceableButton(placeable, () => placeableManager.PreparePlaceable(placeable));
            });
        }

        private void OnPlace()
        {
            mode = Mode.None;
            ARManager.DisableTrackables();
            UIManager.ToggleMainMenu(true);
            UIManager.ToggleVisibleButton(true);

            UpdatePlaceableUI();
        }

        private void OnPlacePrepare()
        {
            mode = mode == Mode.None ? Mode.Creation : mode;
            ARManager.EnablePlaneDetection();

            UIManager.HideAppearableMenus();
            UIManager.ToggleVisibleButton(false);
        }

        private void OnPlaceableSet(Placeable placeable)
        {
            UIManager.HideAppearableMenus();

            UIManager.SetIcon(placeable?.icon);
            UIManager.ToggleEditingButton(placeable != null);
            UIManager.ToggleRemoveButton(placeable != null);

            if (placeable is Animatable animatable)
            {
                UIManager.AnimationListInstall(animatable.AnimationNames);
            }
            else
            {
                UIManager.AnimationListInstall(new());
            }
        }

        private void InitializeUI()
        {
            UIManager.Init();
            UIManager.ColorClickerSetup(placeableManager.GetColor, placeableManager.ChangeColor);
            UIManager.AnimationPickerSetup(placeableManager.PlayAnimation);
        }

        private void UpdatePlaceableUI()
        {
            UIManager.ToggleColorButton(placeableManager.IsCustomizable());
            UIManager.ToggleAnimationButton(placeableManager.IsAnimatable());
        }

        public void SetEditingMode()
        {
            mode = Mode.Editing;
            placeableManager.PreparePlace();
        }
    }

    enum Mode
    {
        None, Creation, Editing
    }
}