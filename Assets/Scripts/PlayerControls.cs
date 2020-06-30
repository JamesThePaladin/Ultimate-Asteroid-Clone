using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public GameManager instance; //var for gameManager
    public GameObject thisPlayer; //variable to store GameObject
    public Transform tf; // A variable to hold our Transform component
    public Rigidbody2D rb; //Rigidbody2D var
    public float thrust; //to hold movement speed
    public float turnThrust; //to hold rotation speed
    private float thrustInput; //to set thrust inputs
    private float turnInput; //to set turn input
    public float terminalForce; //for death force
    public GameObject explosion; //holds our explosion effect



    public float screenTop; //hold screen boundary +y
    public float screenBottom; //hold screen boundary -y
    public float screenRight; //hold screen boundary x
    public float screenLeft; //hold screen boundary -x

    public Color invColor;
    public Color normalColor;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //get object rigidbody compoenent
    }

    // Update is called once per frame
    void Update()
    {
        // shift "dodge" function, it really just makes you teleport but I'll leave it for now
        if (Input.GetKey("left shift") | Input.GetKey("right shift"))
        {
            thrustInput = Input.GetAxis("Vertical") * 5; //get forward and reverse input
            turnInput = Input.GetAxis("Horizontal") * 6; //get turn input
            transform.Rotate(Vector3.forward * turnInput * turnThrust * Time.deltaTime); 
        }

        //check for keyboard input
        else
        {
            thrustInput = Input.GetAxis("Vertical"); //get forward and reverse input
            turnInput = Input.GetAxis("Horizontal"); //get turn input
            transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * turnThrust);
        }

        //Return player to (0, 0, 0)
        if (Input.GetKeyDown("u"))
        {
            rb.velocity = Vector2.zero; //remove velocity
            tf.position = new Vector3(0, 0, 0);
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
        rb.AddRelativeForce(Vector2.up * thrustInput * thrust);
    }

    void Respawn() 
    {
        rb.velocity = Vector2.zero; //remove velocity
        transform.position = Vector2.zero; //reset vector
        SpriteRenderer sr = GetComponent<SpriteRenderer>(); //get and store renderer
        sr.enabled = true; //enable it
        sr.color = invColor; //set color to invulnerable color
        Invoke("Invulnerable", 3f); //waits to put the collider back
    }

    void Invulnerable() 
    {
        GetComponent<Collider2D>().enabled = true; //enable collider
        GetComponent<SpriteRenderer>().color = normalColor; //change color back to normal
    }

    //Player collision function
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.magnitude > terminalForce)
        {
            GameManager.instance.lives--; //decrement lives on collision
            GameManager.instance.livesText.text = "Lives: " + GameManager.instance.lives;//update lives in UI
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);//instantiate and explosion at player position
            Destroy(newExplosion, 3f); //destroy the new explosion after 3 seconds

            //respawn
            GetComponent<SpriteRenderer>().enabled = false; //disable renderer
            GetComponent<Collider2D>().enabled = false; //disable collider
            Invoke("Respawn", 3f); //call respawn 3 seconds after collision

            if (GameManager.instance.lives <= 0) //if lives are less than or equal to 0 game over
            {
                Application.Quit();
            }
        }
    }
}
