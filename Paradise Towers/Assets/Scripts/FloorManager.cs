using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace context {

/// <summary>
/// Controller class for management of floors.
/// </summary>
public class FloorManager : MonoBehaviour {

    public GameObject arcadeFloor, restaurantFloor, suiteFloor;     // prefabs for floors
    public GameObject lobby;                                        // GameObject of lobby (there is only one for entire hotel, mandatory)
	public GameObject book;
	public Transform positionObject;                                // transform for centering new floors
	
	public GameObject window;
	public Text revText;
    private List<GameObject> floors = new List<GameObject>();       // list of all floors in hotel
	
	public static int fid = 1;

	void Awake(){
		floors.Add(lobby);      // add the mandatory lobby floor to list of floors
		//load();		
	}

	// Use this for initialization
	void Start () {			
		//ObjMgr.window.SetActive (false);
		window.SetActive (true);
    }
	
	// Update is called once per frame
	void Update () {
	}

    /**
     * Adds a floor to the hotel given a floor type.
     * 
     * "restaurant" adds a restaurant floor type.
     * "arcade" adds an arcade floor type.
     * "suite" adds a suite floor type.
     */
    public void addFloor(FloorType floorType)
    {
			if (floorType.Equals(FloorType.Arcade))
        {
            GameObject newFloor = Instantiate(arcadeFloor, positionObject.position, positionObject.rotation);   // instantiates new floor
            newFloor.transform.parent = gameObject.transform;           // set new floor as child of Floors
            newFloor.transform.localScale = positionObject.localScale;  // set position of new floor to position of PositioningObject
            floors.Add(newFloor);                                       // adds floor to list of floors
            newFloor.SetActive(false);                                  // set new floor to be initially inactive

			} else if (floorType.Equals(FloorType.Restaurant))
        {
            GameObject newFloor = Instantiate(restaurantFloor, positionObject.position, positionObject.rotation);
            newFloor.transform.parent = gameObject.transform;
            newFloor.transform.localScale = positionObject.localScale;
            floors.Add(newFloor);
            newFloor.SetActive(false);

			} else if (floorType.Equals(FloorType.Suite))
        {
            GameObject newFloor = Instantiate(suiteFloor, positionObject.position, positionObject.rotation);
            newFloor.transform.parent = gameObject.transform;
            newFloor.transform.localScale = positionObject.localScale;
            floors.Add(newFloor);
            newFloor.SetActive(false);
        }
			FinanceMgr.addRevenueSource(FinanceMgr.Floor, floorType.revenues());
			Queries.addFloor (fid, floorType.getType(), FinanceMgr.tid, floorType.getCost());
			fid++;

			//INSERT INTO FLOORS (pos, type, level) VALUES (floors.size() -1, floorType.type(), 0)
			//INSERT INTO Occupants (pos, num) VALUES(pos, 0)
			//tid += 1; //source = {floor : 0, upgrade : 1}
			//INSERT INTO Cost(tid, source, amt) VALUES (tid, 0, floorType.cost())
    }

    /**
     * Activates the given floor (makes it visible on screen).
     */
    public void selectFloor(GameObject selectedFloor)
    {
        selectedFloor.SetActive(true);  // activate the selected floor

        // deactivate all other floors
        foreach (GameObject floor in floors) {
            if (floor != selectedFloor)
            {
                floor.SetActive(false);
            }
        }
    }

    /**
     * Removes the floor that is given to it.
     */
    public void removeFloor(GameObject floor)
    {
		int position = 0;
        DestroyObject(floor);
		Queries.removeFloor (position);
    }
			

	}
}