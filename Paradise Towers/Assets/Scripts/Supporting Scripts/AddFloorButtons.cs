using context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddFloorButtons : MonoBehaviour {

    public Button addSuite;
    public Button addRestaurant;
    public Button addArcade;

    private FloorManager floorManager;

    // Use this for initialization
    void Start () {
        floorManager = GameObject.Find("FloorManager").GetComponent<FloorManager>();    // find FloorManager script on FloorManager GameObject

        addSuite.GetComponent<Button>().onClick.AddListener(delegate { SuiteOnClick(); });
        addRestaurant.GetComponent<Button>().onClick.AddListener(delegate { RestaurantOnClick(); });
        addArcade.GetComponent<Button>().onClick.AddListener(delegate { ArcadeOnClick(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SuiteOnClick()
    {
        floorManager.addFloor(FloorType.Suite);
    }

    private void RestaurantOnClick()
    {
        floorManager.addFloor(FloorType.Restaurant);
    }

    private void ArcadeOnClick()
    {
        floorManager.addFloor(FloorType.Arcade);
    }
}
