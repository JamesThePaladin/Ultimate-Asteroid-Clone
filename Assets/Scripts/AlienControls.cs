using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienControls : MonoBehaviour
{
    public GameManager instance;
    public GameObject alienShip;
    public Rigidbody2D rb; //alien rigidbody
    public Transform targetLock; //player position
    public Transform alienPos; //alien position
    public float chaseSpeed; //var for chase speed
    public int points; //how many points asteroid is worth
    public GameObject player; //variable for player reference
    public GameObject explosion; //holds our explosion effect
    public float screenTop; //hold screen boundary +y
    public float screenBottom; //hold screen boundary -y
    public float screenRight; //hold screen boundary x
    public float screenLeft; //hold screen boundary -x


    // Start is called before the first frame update
    void Start()
    {
    
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

    private void FixedUpdate()
    {
        float xDelta = targetLock.localPosition.x - alienPos.localPosition.x;
        float yDelta = targetLock.localPosition.y - alienPos.localPosition.y;
        rb.AddForce(new Vector3(xDelta * chaseSpeed, yDelta * chaseSpeed), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //check to see if collision is the laser
        if (other.CompareTag("laser"))
        {
            player.SendMessage("ScorePoints", points);

            //make an explosion
            Instantiate(explosion, transform.position, transform.rotation);

            //destroy current asteroid
            Destroy(gameObject);
        }
        if (other.CompareTag("Player")) 
        {
            
        }

    }
}
