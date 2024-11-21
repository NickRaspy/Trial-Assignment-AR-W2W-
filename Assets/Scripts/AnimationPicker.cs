using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TA_W2W
{
    public class AnimationPicker : MonoBehaviour
    {
        [SerializeField] private Transform buttonContainer;
        [SerializeField] private AnimationButton animationButtonPrefab;

        public UnityAction<string> OnAnimationButtonClick {  get; set; }

        private HashSet<string> animationNames = new();

        public void SetAnimationNames(HashSet<string> animationNames) => this.animationNames = animationNames;

        public void LoadButtons()
        {
            if(buttonContainer.childCount > 0) foreach(Transform t in buttonContainer) Destroy(t.gameObject);

            foreach (string name in animationNames)
                Instantiate(animationButtonPrefab, buttonContainer).Init(() => OnAnimationButtonClick(name), name);
        }
    }
}
