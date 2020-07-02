using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public GameObject laser; //var for laser
    public float laserForce; //laser speed
    
    // Update is called once per frame
    void Update()
    {
        //check for fire input
        if (Input.GetButtonDown("Fire1"))
        {
            //spawn laser at the same positiona and rotation as the object this is attached to
            GameObject newLaser = Instantiate(laser, transform.position, transform.rotation);
            //push the rigid body of the new laser away from our up position
            newLaser.GetComponent<Rigidbody2D>().AddForce(transform.up * laserForce);
            //destroy new laser after 2 seconds
            Destroy(newLaser, 2.0f);
        }
    }
}
