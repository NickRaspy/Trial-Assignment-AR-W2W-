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
            ARInit();
            PlaceablesInit();
            UIInit();
        }

        private void ARInit()
        {
            ARManager.SetActionOnRaycaster(position =>
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
            },
            obj =>
            {
                if(mode == Mode.None)
                {
                    placeableManager.SetPlaceable(obj.GetComponent<Placeable>() != null ? obj.GetComponent<Placeable>() : null);

                    PlaceableCheck();
                }
            },
            () =>
            {
                if(mode == Mode.None)
                {
                    placeableManager.SetPlaceable(null);

                    PlaceableCheck();
                }
            });
        }

        private void PlaceablesInit()
        {
            placeableManager.OnPlace += () =>
            {
                mode = Mode.None;
                ARManager.TrackablesReset();
                UIManager.ToggleMainMenu(true);

                PlaceableCheck();
            };

            placeableManager.OnPlacePrepare += () =>
            {
                mode = mode == Mode.None ? Mode.Creation : mode;
                ARManager.EnablePlaneDetection();
            };

            placeableManager.OnPlaceableSet += placeable =>
            {
                UIManager.SetIcon(placeable != null ? placeable.icon : null);

                if (placeable is Animatable animatable) UIManager.AnimationListInstall(animatable.AnimationNames);
                else UIManager.AnimationListInstall(new());

                UIManager.ToggleEditingButton(placeable != null);
            };

            placeableManager.Init(placeable =>
            {
                UIManager.CreatePlaceableButton(placeable, () =>
                {
                    placeableManager.PlaceablePrepare(placeable);
                });
            });
        }

        private void UIInit()
        {
            UIManager.Init();

            UIManager.ColorClickerSetup(placeableManager.GetColor, placeableManager.ChangeColor);

            UIManager.AnimationPickerSetup(placeableManager.PlayAnimation);
        }

        private void PlaceableCheck()
        {
            UIManager.ToggleColorButton(placeableManager.IsCustomizable());
            UIManager.ToggleAnimationButton(placeableManager.IsAnimatable());
        }

        public void SetEditingMode()
        {
            mode = Mode.Editing;

            placeableManager.PlacePrepare();
        }
    }

    enum Mode
    {
        None, Creation, Editing
    }
}
