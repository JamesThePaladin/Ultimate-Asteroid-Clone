using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //variable that holds this instance of the GameManager

    private PlayerControls controls; //to hold toggle


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
    }
    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<PlayerControls>();
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
    }
}
