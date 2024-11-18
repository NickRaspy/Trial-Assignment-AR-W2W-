using UnityEngine;

namespace TA_W2W
{
    public class Customizable : Placeable, ICustomizable
    {
        public Color Color
        {
            get
            {
                print(GetComponent<Renderer>().material.GetColor("_Color"));
                return GetComponent<Renderer>().material.GetColor("_Color");
            }
            set
            {
                GetComponent<Renderer>().material.SetColor("_Color", value);
            }
        }
    }
}
