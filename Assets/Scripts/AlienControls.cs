using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienControls : MonoBehaviour
{
    public GameObject thisAlien; //holds the alien ship
    public Transform alienPos; //alien position
    public Rigidbody2D rb; //alien rigidbody
    public GameObject explosion; //holds our explosion effect
    public float homingSpeed; //var for chase speed
    public int points; //how many points asteroid is worth

    private GameObject player; //variable for player reference
    private Transform playerPos; //for player position

    public float screenTop; //hold screen boundary +y
    public float screenBottom; //hold screen boundary -y
    public float screenRight; //hold screen boundary x
    public float screenLeft; //hold screen boundary -x

    private void Awake()
    {
        //check if player is empty
        if (player == null)
        {
            //if not fill it with the game object tagged player
            player = GameObject.FindWithTag("Player");
        }
        //get transform of player
        playerPos = player.GetComponent<Transform>();
    }
   
    // Update is called once per frame
    void Update()
    {
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

        //set alien transform to new Pos
        transform.position = newPos;
    }

    private void FixedUpdate()
    {
        //float xDelta = playerPos.localPosition.x - transform.localPosition.x; //get x vector distance
        //float yDelta = playerPos.localPosition.y - transform.localPosition.y; //get y vector distance
        //rb.AddForce(new Vector3(xDelta * homingSpeed, yDelta * homingSpeed)); //add our distance as force to rb
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, homingSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //check to see if collision is the laser
        if (other.CompareTag("laser"))
        {
            //destroy the laser
            Destroy(other.gameObject);
            //lower number of aliens
            GameManager.instance.numberOfAliens--;
            //make an explosion
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            //destroy it after 3 seconds
            Destroy(newExplosion, 3f);
            //send points to add
            GameManager.instance.SendMessage("ScorePoints", points);
            //destroy current instance
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject)
        {
            //lower number of aliens
            GameManager.instance.numberOfAliens--;
            //explosion particle effect
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            //destroy it after 3 seconds
            Destroy(newExplosion, 3f);
            ///destroy current instance
            Destroy(this.gameObject);
        }
    }
}
