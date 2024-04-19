using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight4Fridge : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> renderers;

    [SerializeField]
    private Color color = Color.white;

    private List<Material> materials;

    private void Awake()
    { //talking about render thing which im not using right now , but maybe in the future
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool look)
    {
        if (look)
        {
            foreach (var material in materials)
            {
                material.EnableKeyword("_EMISSION"); //show player that this object is selected
                material.SetColor("_EmissionColor", Color.white);
            }
        }
        else
        {
            foreach (var material in materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
