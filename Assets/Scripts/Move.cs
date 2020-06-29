using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject thisPlayer; //variable to store GameObject
    public Transform tf; // A variable to hold our Transform component
    public float speed; // variable for vector magnitude
    public float turnSpeed; // separate variable for turn speed cause its driving me nuts
    public float xDelta; // variable for x-axis
    public float yDelta; // variable for y-axis
    private Boolean MoveEnabled = true; //boolean for disabling/enabling movement

    void Start()
    {
        // Get the Transform Component
        tf = GetComponent<Transform>();
    }
    void Update()
    {
        //toggle input handlers
        if (Input.GetKeyDown("p"))
        {
            if (MoveEnabled)
            {
                MoveEnabled = false;
            }

            else 
            {
                MoveEnabled = true;
            }
        }

        if (MoveEnabled)
        {   //if the shift key is pressed down, move 1 unit
            if (Input.GetKey("left shift") | Input.GetKey("right shift")) 
            {
                
                if (Input.GetKeyDown("w") | Input.GetKeyDown("up"))
                {
                    Vector3 myVector = new Vector3(0, 1, 0); // create vector to add
                    myVector = myVector.normalized; // You could also call the function myVector.Normalize();
                    tf.position += (myVector * speed); // change position and add magnitude
                }

                if (Input.GetKeyDown("a") | Input.GetKeyDown("left"))
                {
                    Vector3 myVector = new Vector3(-1, 0, 0); // create vector to add
                    myVector = myVector.normalized; // You could also call the function myVector.Normalize();
                    tf.position += (myVector * speed); // change position and add magnitude
                }

                if (Input.GetKeyDown("s") | Input.GetKeyDown("down"))
                {
                    Vector3 myVector = new Vector3(0, -1, 0); // create vector to add
                    myVector = myVector.normalized; // You could also call the function myVector.Normalize();
                    tf.position += (myVector * speed); // change position and add magnitude
                }

                if (Input.GetKeyDown("d") | Input.GetKeyDown("right"))
                {
                    Vector3 myVector = new Vector3(1, 0, 0); // create vector to add
                    myVector = myVector.normalized; // You could also call the function myVector.Normalize();
                    tf.position += (myVector * speed); // change position and add magnitude
                }
            }

            //otherwise move as normal
            else
            {
                if (Input.GetKey("w") | Input.GetKey("up"))
                {
                    tf.position = tf.position + (tf.TransformDirection(new Vector3(0, 1, 0)) * speed); // move forward when w is pressed
                }

                if (Input.GetKey("a") | Input.GetKey("left"))
                {
                    tf.Rotate(0, 0, turnSpeed); //rotate left when a is pressed
                }

                if (Input.GetKey("s") | Input.GetKey("down"))
                {
                    tf.position = tf.position + (tf.TransformDirection(new Vector3(0, -1, 0)) * speed); // move backward when s is pressed
                }

                if (Input.GetKey("d") | Input.GetKey("right"))
                {
                    tf.Rotate(0, 0, -turnSpeed); //rotate right when d is pressed
                }
            }

            //Return alpha to (0, 0, 0)
            if (Input.GetKeyDown("u"))
            {
                tf.position = new Vector3(0, 0, 0);
            }
        }

        //exit application with escape key
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }

        //make thisShip inactive
        if (Input.GetKeyDown("q")) 
        {
            thisPlayer.SetActive(false);
        }
    }
}
