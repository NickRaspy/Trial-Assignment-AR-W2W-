using System.Collections.Generic;
using UnityEngine;

namespace TA_W2W
{
    public interface IAnimatable
    {
        HashSet<string> AnimationNames { get; set; }
        void SetAnimation(string name);
    }
}
