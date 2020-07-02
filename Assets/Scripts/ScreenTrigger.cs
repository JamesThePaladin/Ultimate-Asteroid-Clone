using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTrigger : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        //check to see if collision is the laser
        if (other.CompareTag("laser"))
        {
            //destroy the laser
            Destroy(other.gameObject);
        }
    }
}
