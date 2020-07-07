using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //variable that holds this instance of the GameManager
    public PlayerControls playerControls; //to hold player controls

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

    private float timer; //timer for spawning
    public float spawnCoolDownTime; //cool down time for spawning 
    
    public int score; //public player score for testing
    public int lives; //lives for player
    public Text scoreText; //reference to score text
    public Text livesText; //reference to lives text

    public GameObject playerGun; //variable for laser sprite empty


    private void Awake()
    {
        // if instance is empty
        if (instance == null) 
        {
            //store THIS instance of the class in the instance variable
            instance = this;
            //keep this instance of game manager when loading new scenes
            DontDestroyOnLoad(this.gameObject); 
        }
        else 
        {
            //delete the new game manager attempting to store itself, there can only be one.
            Destroy(this.gameObject);
            //display message in the console to inform of its demise
            Debug.Log("Warning: A second game manager was detected and destrtoyed"); 
        }

        //initialize score
        score = 0; 
    }
    //Start is called before the first frame update
    void Start()
    {
        //get alien spawn object
        alienSpawn = GameObject.FindWithTag("alienSpawner");
        //get player controls script
        playerControls = GameObject.FindObjectOfType<PlayerControls>();
        //update score text in UI
        scoreText.text = "" + score;
        //update lives in UI
        livesText.text = "Lives: " + lives;
        //get alien spawn transform
        alienTf = alienSpawn.GetComponent<Transform>();
        //get player gun
        playerGun = GameObject.FindWithTag("LaserEmpty");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGun == null) 
        {
            playerGun = GameObject.FindWithTag("LaserEmpty");
        } 

        //subtract the time since the last frame from timer
        timer -= Time.deltaTime; 

        //if timer gets below 1
        if (timer < 1) 
        {
            //set asteroid spawn to true
            canSpawnAsteroid = true;
            //set alien spawn to true
            canSpawnAlien = true;
            //set timer to cooldown
            timer = spawnCoolDownTime; 
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
                //spawn at that points location
                GameObject alien = Instantiate(alienPrefab, alienTf.position, alienTf.rotation); 
                //increment the number of aliens
                numberOfAliens++; 
                //set spawning to false
                canSpawnAlien = false;
            }
        }

        //toggle cotrols on/off
        if (Input.GetKeyDown(KeyCode.P))
        {
            //inverse component state
            playerControls.enabled = !playerControls.enabled;
            //for debugging
            Debug.Log("controls have been toggled"); 
        }
    }

    //takes in points from other objects and adds it to the player's score
    void ScorePoints(int addPoints) 
    {
        //add points to player score
        score += addPoints;
        //update score text in UI
        scoreText.text = "" + score;
    }

    public void LoseLife() 
    {
        //minus a life
        lives--;
        //update lives in UI
        livesText.text = "Lives: " + lives;

        //if lives are less than or equal to 0 game over
        if (lives <= 0) 
        {
            Application.Quit();
        }
    }
    public void Respawn()
    {
        //remove velocity
        playerControls.rb.velocity = Vector3.zero;
        //reset vector
        playerControls.gameObject.transform.position = Vector3.zero;
        //get and store renderer
        SpriteRenderer sr = playerControls.gameObject.GetComponent<SpriteRenderer>();
        //enable it
        sr.enabled = true;
        //set color to invulnerable color
        sr.color = playerControls.invColor;
        //waits to put the collider back
        Invoke("Invulnerable", 4f); 
    }

    void Invulnerable()
    {
        //enable collider
        playerControls.gameObject.GetComponent<Collider2D>().enabled = true;
        //change color back to normal
        playerControls.gameObject.GetComponent<SpriteRenderer>().color = playerControls.normalColor;
        //enable player gun
        playerGun.gameObject.GetComponent<LaserScript>().enabled = true;
    }
}
