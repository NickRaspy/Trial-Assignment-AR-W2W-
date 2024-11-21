using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TA_W2W
{
    [RequireComponent(typeof(Button))]
    public class AnimationButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private TMP_Text buttonText;

        public void Init(UnityAction buttonAction, string text)
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(buttonAction);
            buttonText.text = text;
        }
    }
}
