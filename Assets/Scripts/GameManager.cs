using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //variable that holds this instance of the GameManager
    private PlayerControls controls; //to hold toggle
    public List<Transform> spawnPoints; //spawn point index
    public GameObject asteroidPrefab; //for asteroids
    public int numberOfAsteroids; //for number of asteroids
    public int maxAsteroids; //for number of asteroids
    float timer; //timer for spawning
    public float waitTime; //wait time for spawning 
    bool canSpawn; //boolean for spawning

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
        controls = GetComponent<PlayerControls>();
        scoreText.text = "" + score;//update score text in UI
        livesText.text = "Lives: " + lives;//update lives in UI
    }

    // Update is called once per frame
    void Update()
    {
        //toggle cotrols on/off
        if (Input.GetKeyDown(KeyCode.P))
        {
            controls.enabled = !controls.enabled; //inverse component state
            Debug.Log("controls have been toggled"); //
        }

        timer -= Time.deltaTime;
        if (timer < 1) 
        {
            canSpawn = true;
            timer = waitTime;
        }
        if (maxAsteroids > numberOfAsteroids) 
        {
            if (canSpawn == true) 
            {
                int random = Random.Range(0, spawnPoints.Count);
                GameObject asteroid = Instantiate(asteroidPrefab, spawnPoints[random].position, spawnPoints[random].rotation);
                numberOfAsteroids++;
                canSpawn =  false;
            }
        }
    }

    void ScorePoints(int addPoints)
    {
        score += addPoints;
        scoreText.text = "" + score;//update score text in UI
    }
}
