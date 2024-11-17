using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TA_W2W
{
    [RequireComponent (typeof(Button))]
    public class PlaceableButton : MonoBehaviour
    {
        private Button button;
        private Placeable placeable;

        public void Init(Placeable placeable, UnityAction action)
        {
            this.placeable = placeable;

            button = GetComponent<Button>();

            print(button.targetGraphic);

            if(button.targetGraphic is Image image)
                image.sprite = placeable.icon;

            button.onClick.AddListener(action);
        }
    }
}
