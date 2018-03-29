using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour {

    public ColorsManager colorsManager;
    
    private void OnEnable()
    {
        RandomizeTrailMaterial();
    }

    private void RandomizeTrailMaterial()
    {
        GetComponent<ParticleSystemRenderer>().trailMaterial = colorsManager.GetRandomMaterial();
    }

}
