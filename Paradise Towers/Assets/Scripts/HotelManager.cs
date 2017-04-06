using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HotelManager : MonoBehaviour {


	void Awake(){
		Queries.createLog ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /** MAIN MENU FUNCTIONS **/
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
