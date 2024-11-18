using System;
using UnityEngine;
using UnityEngine.Events;
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

        [Header("Buttons")]
        [SerializeField] private Button colorButton;
        [SerializeField] private Button animationsButton;

        [Header("Popups")]
        [SerializeField] private ColorPicker colorPicker;

        public void Init()
        {
            colorButton.onClick.AddListener(() => colorPicker.gameObject.SetActive(!colorPicker.gameObject.activeInHierarchy));
        }

        public void CreatePlaceableButton(Placeable placeable, UnityAction onButtonCreate)
        {
            Instantiate(placeableButtonPrefab, placeableButtonContainer).Init(placeable, () =>
            {
                placeableSelectMenu.SetActive(false);

                onButtonCreate();
            });
        }

        public void EnableMainMenu() => mainMenu.SetActive(true);

        public void ToggleColorButton(bool enable) => colorButton.interactable = enable;

        public void ToggleAnimationButton(bool enable) => animationsButton.interactable = enable;

        public void ColorClickerSetup(Func<Color> getColorFromObject, Action<Color> onColorChange)
        {
            colorButton.onClick.RemoveAllListeners();
            colorButton.onClick.AddListener(() =>
            {
                colorPicker.gameObject.SetActive(!colorPicker.gameObject.activeInHierarchy);

                if (!colorPicker.gameObject.activeInHierarchy) return;

                colorPicker.color = getColorFromObject();

                print(colorPicker.color);
            });
            colorPicker.OnColorChanged += onColorChange;
        }
    }
}
