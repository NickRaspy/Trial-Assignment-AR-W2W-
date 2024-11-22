using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TA_W2W
{
    public class UIManager : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private GameObject mainMenu;

        [Header("Placeables")]
        [SerializeField] private PlaceableButton placeableButtonPrefab;
        [SerializeField] private Transform placeableButtonContainer;
        [SerializeField] private GameObject placeableSelectMenu;
        [SerializeField] private Image selectedPlaceableIcon;

        [Header("Buttons")]
        [SerializeField] private Button colorButton;
        [SerializeField] private Button animationsButton;
        [SerializeField] private Button editingButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private VisibleButton visibleButton;

        [Header("Popups")]
        [SerializeField] private ColorPicker colorPicker;
        [SerializeField] private AnimationPicker animationPicker;

        [Header("Other")]
        [SerializeField] private Sprite emptyIcon;

        private void OnGUI()
        {
            if ((Event.current.type == EventType.MouseDown || Event.current.type == EventType.TouchDown) && !EventSystem.current.IsPointerOverGameObject())
                HideAppearableMenus();
        }

        public void Init()
        {
            colorButton.onClick.AddListener(ToggleColorPickerVisibility);
            visibleButton.SetVisibility = ToggleMainMenuVisibility;
        }

        public void CreatePlaceableButton(Placeable placeable, UnityAction onButtonCreate)
        {
            var button = Instantiate(placeableButtonPrefab, placeableButtonContainer);
            button.Init(placeable, () =>
            {
                placeableSelectMenu.SetActive(false);
                onButtonCreate();
            });
        }

        public void ToggleMainMenu(bool enable) => mainMenu.SetActive(enable);

        public void ToggleVisibleButton(bool enable) => visibleButton.gameObject.SetActive(enable);

        public void ToggleRemoveButton(bool enable) => removeButton.interactable = enable;

        public void ToggleColorButton(bool enable) => colorButton.interactable = enable;

        public void ToggleAnimationButton(bool enable) => animationsButton.interactable = enable;

        public void ToggleEditingButton(bool enable) => editingButton.interactable = enable;

        public void ToggleColorPicker(bool enable) => colorPicker.gameObject.SetActive(enable);

        public void ToggleAnimationPicker(bool enable) => animationPicker.gameObject.SetActive(enable);

        public void HideAppearableMenus()
        {
            ToggleColorPicker(false);
            ToggleAnimationPicker(false);
        }

        public void ColorClickerSetup(Func<Color> getColorFromObject, Action<Color> onColorChange)
        {
            colorButton.onClick.RemoveAllListeners();
            colorButton.onClick.AddListener(() =>
            {
                colorPicker.color = getColorFromObject();
                ToggleColorPickerVisibility();
            });
            colorPicker.OnColorChanged += onColorChange;
        }

        public void AnimationPickerSetup(UnityAction<string> onAnimationSelect)
        {
            animationPicker.OnAnimationButtonClick += onAnimationSelect;
            animationsButton.onClick.RemoveAllListeners();
            animationsButton.onClick.AddListener(() =>
            {
                ToggleAnimationPickerVisibility();
                animationPicker.LoadButtons();
            });
        }

        public void AnimationListInstall(HashSet<string> list) => animationPicker.SetAnimationNames(list);

        public void SetIcon(Sprite icon) => selectedPlaceableIcon.sprite = icon ?? emptyIcon;

        private void ToggleColorPickerVisibility() => colorPicker.gameObject.SetActive(!colorPicker.gameObject.activeInHierarchy);

        private void ToggleAnimationPickerVisibility() => animationPicker.gameObject.SetActive(!animationPicker.gameObject.activeInHierarchy);

        private bool ToggleMainMenuVisibility()
        {
            ToggleMainMenu(!mainMenu.activeInHierarchy);
            return mainMenu.activeInHierarchy;
        }
    }
}