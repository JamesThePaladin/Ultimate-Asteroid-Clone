using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienControls : MonoBehaviour
{
    public GameManager instance;
    public Rigidbody2D rb; //alien rigidbody
    public Transform targetLock; //player position
    public Transform alienPos; //alien position
    public float homingSpeed; //var for chase speed
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
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        //get transform of player
        targetLock = player.GetComponent<Transform>();
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
        //enemy ship chases player
        float xDelta = targetLock.localPosition.x - alienPos.localPosition.x;
        float yDelta = targetLock.localPosition.y - alienPos.localPosition.y;
        rb.AddForce(new Vector3(xDelta * homingSpeed, yDelta * homingSpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //check to see if collision is the laser
        if (other.CompareTag("laser"))
        {
            Destroy(other.gameObject); //destroy the laser
            //make an explosion
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            GameManager.instance.SendMessage("ScorePoints", points);
            //destroy current instance
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (CompareTag("Player"))
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            //destroy yourself
            Destroy(this.gameObject);
        }
    }
}
