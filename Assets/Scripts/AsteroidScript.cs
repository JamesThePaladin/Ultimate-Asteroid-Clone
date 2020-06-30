using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameManager instance; //game manager instance
    public GameObject thisAsteroid; //holds this asteroid
    public float maxTorque; // hold asteroid max torque
    public Rigidbody2D rb; //rigidbody var
    public float screenTop; //hold screen boundary +y
    public float screenBottom; //hold screen boundary -y
    public float screenRight; //hold screen boundary x
    public float screenLeft; //hold screen boundary -x
    public int asteroidSize; //for asteroid breaking 3 = large, 2 = medium, 1 = small
    public GameObject asteroidMedium; //reference to medium size prefab
    public GameObject asteroidSmall; //reference to small size prefab
    public int points; //how many points asteroid is worth
    public GameObject explosion;
    public GameObject player; //variable for player reference
    public Transform playerPos; //for player position
    public Transform asteroidPos; //asteroid position
    public float startSpeed; //var for chase speed

    private void Awake()
    {
        if (player == null) //if player slot is empty
        {
            player = GameObject.FindWithTag("Player"); //fill it with player
        }

        playerPos = player.GetComponent<Transform>(); //get player transform
    }

    // Start is called before the first frame update
    void Start()
    {
        float xDelta = playerPos.localPosition.x - asteroidPos.localPosition.x; //get x vector distance
        float yDelta = playerPos.localPosition.y - asteroidPos.localPosition.y; //get y vector distance
        rb.AddForce(new Vector3(xDelta * startSpeed, yDelta * startSpeed)); //add our distance as force to rb
        float torque = Random.Range(-maxTorque, maxTorque); //add random rotation
        rb.AddTorque(torque); //apply torque to rigidbody to rotate
       
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

        //set asteroid transform to new Pos
        transform.position = newPos;
    }

    //asteroid collisions function
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check to see if collision is the laser
        if (other.CompareTag("laser")) 
        {
            Destroy(other.gameObject); //destroy the laser
            //check the size of the asteroid and spawn the next smaller size
            //if (asteroidSize == 3) 
            //{
            //    //spawn 2 medium asteroids at the same spot of the large that was destroyed
            //    Instantiate(asteroidMedium, transform.position, transform.rotation);
            //    Instantiate(asteroidMedium, transform.position, transform.rotation);
            //}
            //else if (asteroidSize == 2) //else if its medium
            //{
            //    //spawn 2 medium asteroids at the same spot of the large that was destroyed
            //    Instantiate(asteroidSmall, transform.position, transform.rotation);
            //    Instantiate(asteroidSmall, transform.position, transform.rotation);
            //}
            //else if (asteroidSize == 1) //else if its small
            //{
            //    //give player points
            //}
            //tell the player to score points
            GameManager.instance.SendMessage("ScorePoints", points);
            //lower number of asteroids
            GameManager.instance.numberOfAsteroids--;

            //make an explosion
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            //destroy the asteroid
            Destroy(this.gameObject);
            

        }
       
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject)
        {
            //lower the number of asteroids
            GameManager.instance.numberOfAsteroids--;
            //make an explosion
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            //destroy the asteroid
            thisAsteroid.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
