using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TA_W2W
{
    public class Customizable : Placeable, ICustomizable
    {
        [SerializeField] private Color defaultColor = Color.white;

        private Color color = Color.white;

        [Tooltip("If your placeable doesn't have material (renderer), but its childs have")]
        [SerializeField] private List<Renderer> separatedRenderers = new();

        [Tooltip("If your placeable have multiple materials and you don't want it to use first one")]
        [SerializeField] private string selectedMaterialName;

        public Color Color
        {
            get
            {
                color = color == Color.white ? defaultColor : color;
                return color;
            }
            set
            {
                color = value;
                if(GetComponent<Renderer>() != null) 
                    FindMaterialByName(GetComponent<Renderer>().materials, selectedMaterialName).SetColor("_Color", value);
                if (separatedRenderers.Count > 0) 
                    separatedRenderers.ForEach(sr => FindMaterialByName(sr.materials, selectedMaterialName).SetColor("_Color", value));
            }
        }

        private Material FindMaterialByName(Material[] materials, string materialName)
        {
            Material foundMaterial = materials.FirstOrDefault(mat => mat.name.Contains(materialName));

            return foundMaterial ?? materials.FirstOrDefault();
        }
    }
}
