using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomizer : MonoBehaviour
{
    public GameObject alienSpawners; // to hold our alien spawner objects
    public Transform tf; //for the spawners transform component

    // Start is called before the first frame update
    void Start()
    {
        //get the transform component
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        tf.position = Random.insideUnitSphere * 10;
    }
}
