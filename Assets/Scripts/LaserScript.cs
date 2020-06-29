using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float laserForce; //laser speed
    public GameObject laser; //var for laser

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check for fire input and make bullets
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newLaser = Instantiate(laser, transform.position, transform.rotation);
            newLaser.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * laserForce);
            newLaser.GetComponent<Transform>();
            Destroy(newLaser, 2.0f);
        }
    }
}
