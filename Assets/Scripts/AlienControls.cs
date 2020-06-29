using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienControls : MonoBehaviour
{
    public GameManager instance;
    public GameObject alienShip;
    public Rigidbody2D rb; //alien rigidbody
    private Transform targetLock; //player position
    private Transform alienPos; //alien position
    public float chaseSpeed; //var for chase speed
    public int points; //how many points asteroid is worth
    public GameObject player; //variable for player reference
    public GameObject explosion; //holds our explosion effect


    // Start is called before the first frame update
    void Start()
    {
        chaseSpeed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float xDelta = targetLock.localPosition.x - alienPos.localPosition.x;
        float yDelta = targetLock.localPosition.y - alienPos.localPosition.y;
        rb.AddForce(new Vector3(xDelta * chaseSpeed, yDelta * chaseSpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //check to see if collision is the laser
        if (other.CompareTag("laser"))
        {
            //Destroy(other.gameObject); //destroy the laser
            ////check the size of the asteroid and spawn the next smaller size
            //if (asteroidSize == 3)
            //{
            //    //spawn 2 medium asteroids at the same spot of the large that was destroyed
            //    Instantiate(asteroidMedium, transform.position, transform.rotation);
            //    Instantiate(asteroidMedium, transform.position, transform.rotation);
            //}

            //tell the player to score points
            player.SendMessage("ScorePoints", points);

            //make an explosion
            Instantiate(explosion, transform.position, transform.rotation);

            //destroy current asteroid
            Destroy(gameObject);
        }

    }
}
