using UnityEngine;
using System.Collections.Generic;

namespace TA_W2W
{
    [RequireComponent (typeof(Animator))]
    public class Animatable : Customizable, IAnimatable
    {
        private Animator animator;

        public HashSet<string> AnimationNames { get; set; } = new();

        private void Start()
        {
            animator = GetComponent<Animator>();

            foreach (var anim in animator.runtimeAnimatorController.animationClips)
            {
                AnimationNames.Add(anim.name);
            }
        }
        public void SetAnimation(string name)
        {
            if (animator != null) animator.Play(name);
        }
    }
}
