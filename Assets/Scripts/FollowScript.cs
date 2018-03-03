﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

    public GameObject target;

	private float speed;

	void Start ()
	{
		speed = 4.0f;
	}

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		//transform.Translate (5.0f * Time.deltaTime, 0.0f, 0.0f, Space.World);
		GetComponent<Rigidbody>().velocity = new Vector3 (speed, 0.0f, 0.0f);;
    }
}
