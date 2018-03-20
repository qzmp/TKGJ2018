using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour {

    private Vector2 uvOffset = Vector2.zero;
    public Vector2 uvAnimationRate = new Vector2(0.5f, 0);

    private Renderer renderer;


	void Start () {
        renderer = GetComponent<Renderer>();
	}

	void Update () {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if(renderer.enabled)
        {
            renderer.material.SetTextureOffset("_MainTex", uvOffset);
        }
	}
}
