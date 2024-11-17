using UnityEngine;

namespace TA_W2W
{
    public class Customizable : Placeable, ICustomizable
    {
        public void SetColor(Color color) => GetComponent<Renderer>().material.color = color;
    }
}
