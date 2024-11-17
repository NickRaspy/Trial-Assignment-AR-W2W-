using UnityEngine;
using UnityEngine.Events;

namespace TA_W2W
{
    public class GenericPlaceableFactory : PlaceableFactory
    {
        public override void Create(Placeable placeablePrototype, Vector3 position, UnityAction onPlaceAction = null)
        {
            Placeable newPlaceable = Instantiate(placeablePrototype, transform);

            newPlaceable.Place(position);

            if (onPlaceAction != null)
            {
                newPlaceable.OnPlace = onPlaceAction;
            }

            AddPlaceable(newPlaceable);
        }
    }
}
