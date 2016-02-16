using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseUI;                   // The actual Pause object that's slapped on top of the screen.

    private bool paused = false;                 // will toggle the pause


	// Use this for initialization
	void Start () {
	   
        pauseUI.SetActive(false);                // don't want to start out paused
	}
	
	// Update is called once per frame
	void Update () {
	   
        // User toggles pause on/off
        if( Input.GetButtonDown("Pause") )
        {
            paused = !paused;
        }

        if( paused )
        {
            pauseUI.SetActive(true);         // Pause menu pops up
            Time.timeScale = 0;              // Freeze time!
        }
        else
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }

	}

    public void Resume(){
        paused = false;
    }

    public void Restart(){

        // Reload this scene
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.buildIndex);
    }

    public void Quit(){
        Application.Quit();
    }

    public void MainMenu(){

    }
}
