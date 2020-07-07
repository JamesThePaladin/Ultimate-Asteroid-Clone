using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public GameObject thisPlayer; //variable to store GameObject
    public Transform tf; // A variable to hold our Transform component
    public Rigidbody2D rb; //Rigidbody2D var

    public float thrust; //to hold movement speed
    public float thrustBoost; //variable for shift thrust boost
    private float thrustInput; //to set thrust inputs

    public float turnThrust; //to hold rotation speed
    public float turnBoost; //variable for shift turn boost
    private float turnInput; //to set turn input

    public float terminalForce; //for death force
    public Color invColor; //invincibility color
    public Color normalColor;//normal color tor return to
    public GameObject explosion; //holds our explosion effect

    public float screenTop; //hold screen boundary +y
    public float screenBottom; //hold screen boundary -y
    public float screenRight; //hold screen boundary x
    public float screenLeft; //hold screen boundary -x

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //get object rigidbody compoenent
    }

    // Update is called once per frame
    void Update()
    {
        // shift "boost" function
        if (Input.GetKey("left shift") | Input.GetKey("right shift"))
        {
            thrustInput = Input.GetAxis("Vertical") * thrustBoost; //get forward and reverse input
            turnInput = Input.GetAxis("Horizontal") * turnBoost; //get turn input
            transform.Rotate(transform.forward * turnInput * turnThrust * Time.deltaTime); //rotate sprite 
        }

        //check for keyboard input
        else
        {
            thrustInput = Input.GetAxis("Vertical"); //get forward and reverse input
            turnInput = Input.GetAxis("Horizontal"); //get turn input
            transform.Rotate(transform.forward * turnInput * turnThrust * Time.deltaTime); //rotate sprite
        }
            
        //exit application with escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //screen wrapping
        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop) 
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom) 
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }

        //set player transform to new Pos
        transform.position = newPos;
    }

    //update on a fixed time interval for consistency
    private void FixedUpdate()
    {
        //apply thrust
        rb.AddForce(transform.up * thrustInput * thrust);
    }

    //Player collision function
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.magnitude > terminalForce)
        {
            GameManager.instance.LoseLife();
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);//instantiate and explosion at player position
            Destroy(newExplosion, 3f); //destroy the new explosion after 3 seconds

            //respawn
            GetComponent<SpriteRenderer>().enabled = false; //disable renderer
            GetComponent<Collider2D>().enabled = false; //disable collider
            GameManager.instance.playerGun.gameObject.GetComponent<LaserScript>().enabled = false; //disable player gun
            StartCoroutine(respawnTimer());

            //wait timer for respawn
            IEnumerator respawnTimer() 
            {
                yield return new WaitForSeconds(3f);
                GameManager.instance.Respawn();
            }
        }
    }
}
