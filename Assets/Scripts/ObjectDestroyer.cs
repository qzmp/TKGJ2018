using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {



    bool enteredCamera = false;

    void OnBecameVisible()
    {
        enteredCamera = true;
    }
    void OnBecameInvisible()
    {
        if(enteredCamera)
            Destroy(gameObject);
    }
}
