using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ESCQuit : MonoBehaviour {
    public GameObject hint; //Hint to show when ESC is tapped
    public GameObject optionsMenu; //Menu to Toggle
    public float countDown = 3f; //Time to hold ESC before quit
    public bool displayHint = false;
    public bool displayOptions;

    private float downTime, upTime, pressTime = 0f;
    private bool quit = false;
    private bool keyDown = false;

	void Start () {
        hint.SetActive(false);
    }
	
	void Update () {
        //When ESC is down
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            keyDown = true;
            downTime = Time.time;
            pressTime = downTime + countDown;
            quit = true;
            hint.SetActive(true);
        }

        //When ESC is up after it is down
        if (Input.GetKeyUp(KeyCode.Escape) && (keyDown = true))
        {
            keyDown = false;
            if (!SceneManager.GetActiveScene().name.Equals("Main Menu"))
            {
                //Show/hide options
                displayOptions = !displayOptions;
                if (displayOptions)
                {
                    GameObject.FindObjectOfType<MenuHandler>().ShowOptions();
                }
                else
                {
                    GameObject.FindObjectOfType<MenuHandler>().CloseOptions();
                }
            }
        }

        if (Time.time >= pressTime)
        {
            hint.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            quit = false;
        }

        if ((Time.time >= pressTime) && (quit == true))
        {
            Application.Quit();
        }
    }
}
