﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
