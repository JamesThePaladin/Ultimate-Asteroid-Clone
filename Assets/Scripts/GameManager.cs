using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //variable that holds this instance of the GameManager
    private PlayerControls playerControls; //to hold toggle

    public List<Transform> spawnPoints; //asteroid spawn point index
    public GameObject asteroidPrefab; //for asteroids
    public int numberOfAsteroids; //for number of asteroids
    public int maxAsteroids; //for number of asteroids
    bool canSpawnAsteroid; //boolean for spawning asteroids

    public GameObject alienSpawn;  //alien spawn point circle
    public Transform alienTf; //alien spawner transform
    public GameObject alienPrefab; //for aliens
    public int numberOfAliens; //for number of asteroids
    public int maxAliens; //for number of asteroids
    bool canSpawnAlien; //boolean for spawning aliens

    float timer; //timer for spawning
    public float waitTime; //wait time for spawning 
    
    public int score; //public player score for testing
    public int lives; //lives for player
    public Text scoreText; //reference to score text
    public Text livesText; //reference to lives text


    private void Awake()
    {
        if (instance == null) // if instance is empty
        {
            instance = this; // store THIS instance of the class in the instance variable
            DontDestroyOnLoad(this.gameObject); //keep this instance of game manager when loading new scenes
        }
        else 
        {
            Destroy(this.gameObject); // delete the new game manager attempting to store itself, there can only be one.
            Debug.Log("Warning: A second game manager was detected and destrtoyed"); // display message in the console to inform of its demise
        }
        score = 0; //initialize score
    }
    // Start is called before the first frame update
    void Start()
    {
        alienSpawn = GameObject.FindWithTag("alienSpawner"); //get alien spawn object
        playerControls = GetComponent<PlayerControls>(); //get player controls script
        scoreText.text = "" + score;//update score text in UI
        livesText.text = "Lives: " + lives;//update lives in UI
    }

    // Update is called once per frame
    void Update()
    {
        alienTf = alienSpawn.GetComponent<Transform>(); //get alien spawn transform

        timer -= Time.deltaTime; //set time that is subtracted by time that has passed
        if (timer < 1) 
        {
            canSpawnAsteroid = true; //initialize asteroid spawn spawn to true
            canSpawnAlien = true; //initialize alien spawner to true
            timer = waitTime; //set timer to cooldown
        }
        if (maxAsteroids > numberOfAsteroids)  //if the number of asteroids is less than the max amount of asteroids
        {
            if (canSpawnAsteroid == true) //and if cooldown is up
            {
                int random = Random.Range(0, spawnPoints.Count); //choose randomly from list of spawn points
                GameObject asteroid = Instantiate(asteroidPrefab, spawnPoints[random].position, spawnPoints[random].rotation); //spawn at that points location
                numberOfAsteroids++; //increment the asteroid count
                canSpawnAsteroid =  false; //change spawning to false
            }
        }
        if (maxAliens > numberOfAliens)  //if the number of aliens is less than the max amount aliens
        {
            if (canSpawnAlien == true) //and if cooldown is up
            {
                GameObject alien = Instantiate(alienPrefab, alienTf.position, alienTf.rotation); //spawn at that points location 
                numberOfAliens++; //increment the number of aliens
                canSpawnAlien = false; //set spawning to false
            }
        }

        //toggle cotrols on/off
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerControls.enabled = !playerControls.enabled; //inverse component state
            Debug.Log("controls have been toggled"); //for debugging
        }
    }

    //takes in points from other objects and adds it to the player's score
    void ScorePoints(int addPoints) 
    {
        score += addPoints; //add points to player score
        scoreText.text = "" + score;//update score text in UI
    }
}
