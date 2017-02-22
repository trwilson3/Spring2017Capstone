using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    // Assign in inspector
    public GameObject menuCanvas; //mainMenu Canvas Object
    public GameObject optionsCanvas; //OptionsMenu Canvas Object
    public GameObject titleCanvas;

    //Button Sounds
    public AudioClip rolloverSound;
    public AudioClip clickSound;

    private int initialLevel = 1;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Main Menu"))
        {
            titleCanvas = GameObject.Find("TitleCanvas");
            menuCanvas = GameObject.Find("MenuCanvas");
        }
        //optionsCanvas = GameObject.Find("OptionsCanvas"); Don't know why this doesn't work

        this.CloseOptions();

    }

    public void Continue()
    {
        //TODO hook for loading latest save (if one exists)
    }

    public void LoadGame()
    {
        //TODO hook for loading save of choice
        //Read from PlayerPrefs/XML
        //SceneManager.LoadScene(initialLevel); //Add data that coordinates Which level to load!
    }

    public void NewGame()
    {
        //TODO load first level
        //SceneManager.LoadScene(initialLevel);


		//Added by Adam
		SceneManager.LoadScene(1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowOptions()
    {
        if (SceneManager.GetActiveScene().name.Equals("UI Shell Code") || SceneManager.GetActiveScene().name.Equals("Main Menu"))
        {
            titleCanvas.SetActive(false);
            menuCanvas.SetActive(false);
            optionsCanvas.SetActive(true);
        }
        else
        {
            titleCanvas.SetActive(true);
            optionsCanvas.SetActive(true);
            Debug.Log("Starting pause");
            //Timescale.GetComponent<Pause>().PauseGame(); //Something like this to pause game
        }
    }

    public void CloseOptions()
    {
        if (SceneManager.GetActiveScene().name.Equals("UI Shell Code") || SceneManager.GetActiveScene().name.Equals("Main Menu"))
        {
            titleCanvas.SetActive(true);
            menuCanvas.SetActive(true);
            optionsCanvas.SetActive(false);
        }
        else
        {
            optionsCanvas.SetActive(false);
            Debug.Log("Starting un-pause");
            //Timescale.GetComponent<Pause>().UnPauseGame(); //Something like this to un-pause game
        }
    }

    //Button Sounds
    public void playRolloverSound(Transform transform)
    {
        AudioSource.PlayClipAtPoint(rolloverSound, transform.position);
    }

    public void playClickSound(Transform transform)
    {
        AudioSource.PlayClipAtPoint(clickSound, transform.position);
    }

}
