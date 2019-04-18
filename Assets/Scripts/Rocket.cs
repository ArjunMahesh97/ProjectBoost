﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    void Thrust()
    {
        //Debug.Log('a');
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log('a');
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void Rotate()
    {

        rigidBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log('a');
            rigidBody.transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.transform.Rotate(-Vector3.forward);
        }

        rigidBody.freezeRotation = false;
    }
}
