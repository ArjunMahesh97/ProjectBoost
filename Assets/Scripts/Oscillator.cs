using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementvector;

    [Range(0, 1)] [SerializeField] float movementFactor = 0f;

    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = movementvector * movementFactor;
        transform.position = startingPos + offset;
	}
}
