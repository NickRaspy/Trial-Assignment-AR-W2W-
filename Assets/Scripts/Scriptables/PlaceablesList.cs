using System.Collections.Generic;
using UnityEngine;

namespace TA_W2W
{
    [CreateAssetMenu(fileName = "Placeables List", menuName = "Scriptable Objects/Placeables List")]
    public class PlaceablesList : ScriptableObject
    {
        public List<Placeable> placeables = new();
    }
}