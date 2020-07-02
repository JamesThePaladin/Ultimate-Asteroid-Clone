using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomizer : MonoBehaviour
{
    public GameObject alienSpawners; // to hold our alien spawner objects
    public Transform tf; //for the spawners transform component
    public float spawnRadius; //spawn radius for aliens

    // Start is called before the first frame update
    void Start()
    {
        //get the transform component
        tf = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        //change spawner position
        tf.position = Random.insideUnitCircle * spawnRadius;
    }
}
