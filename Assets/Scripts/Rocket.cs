using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] private float rcsThrust = 100f;
    [SerializeField] private float mianThrust = 1000f;
    [SerializeField] AudioClip thruster;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive,Dead,Transition}
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
	}

    void Thrust()
    {
        //Debug.Log('a');
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log('a');
            rigidBody.AddRelativeForce(Vector3.up * mianThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thruster);
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
            rigidBody.transform.Rotate(Vector3.forward * rcsThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.transform.Rotate(-Vector3.forward * rcsThrust * Time.deltaTime);
        }

        rigidBody.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }

        switch (collision.transform.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                state = State.Transition;
                audioSource.Stop();
                audioSource.PlayOneShot(success);
                Invoke("LoadNextScene", 2f);
                break;
            default:
                state = State.Dead;
                audioSource.Stop();
                audioSource.PlayOneShot(death);
                Invoke("LoadFirstLevel", 2f);
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
