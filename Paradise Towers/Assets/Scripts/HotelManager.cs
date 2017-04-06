using context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HotelManager : MonoBehaviour {

    private FloorManager floorManager;

	void Awake(){
		Queries.createLog();
	}

    // Use this for initialization
    void Start () {
        floorManager = GameObject.Find("FloorManager").GetComponent<FloorManager>();
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

    public void setLobbyVisible()
    {
        foreach(FloorType floor in floorManager.getFloors())
        {
            if (floor.getType() == 3)
            {
                floor.setVisibility(true);
            }
            else
            {
                floor.setVisibility(false);
            }
        }
    }
}
