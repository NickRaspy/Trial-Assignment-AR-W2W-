using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

namespace TA_W2W
{
    public abstract class PlaceableFactory : MonoBehaviour
    {
        protected HashSet<Placeable> placeables = new();

        public abstract void Create(Placeable placeablePrototype, Vector3 position, UnityAction onPlaceAction = null);

        public Placeable GetLastPlaceable()
        {
            return placeables.Last();
        }

        protected void AddPlaceable(Placeable placeable)
        {
            if (placeable != null)
            {
                placeables.Add(placeable);
            }
        }
        
        public void RemovePlaceable(Placeable placeable)
        {
            if (placeable != null)
            {
                placeables.Remove(placeable);
            }
        }
    }
}