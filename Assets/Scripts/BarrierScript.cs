using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour {

    public ColorsManager colorsManager;

    private Color color;
    public Color Color { get { return color; } }

    private void OnEnable()
    {
        RandomizeTrailMaterial();
    }

    private void RandomizeTrailMaterial()
    {
        GetComponent<ParticleSystemRenderer>().trailMaterial = colorsManager.GetRandomMaterial();
    }
}
