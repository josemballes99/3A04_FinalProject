using context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to draw floors onto screen.
/// </summary>

public class FloorView : MonoBehaviour
{
    public GameObject arcadeFloor, restaurantFloor, suiteFloor;     // prefabs for floors
    public Transform positionObject;

    public GameObject window;

    private FloorManager floorManager;

    // Use this for initialization
    void Start()
    {
        floorManager = GameObject.Find("FloorManager").GetComponent<FloorManager>();    // find FloorManager script on FloorManager GameObject
        drawFloors();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * Creates floor GameObjects on the screen. Gets list of floors from FloorManager.
     */
    public void drawFloors()
    {
        List<FloorType> floors = floorManager.getFloors();
        foreach (FloorType floor in floors)
        {
            if (floor.Equals(FloorType.Arcade))
            {
                GameObject newFloor = Instantiate(arcadeFloor, positionObject.position, positionObject.rotation);   // instantiates new floor
                setupFloor(newFloor);
                if (floor.getVisibility())
                {
                    newFloor.SetActive(true);
                }
            }
            else if (floor.Equals(FloorType.Restaurant))
            {
                GameObject newFloor = Instantiate(restaurantFloor, positionObject.position, positionObject.rotation);
                setupFloor(newFloor);
                if (floor.getVisibility())
                {
                    newFloor.SetActive(true);
                }

            }
            else if (floor.Equals(FloorType.Suite))
            {
                GameObject newFloor = Instantiate(suiteFloor, positionObject.position, positionObject.rotation);
                setupFloor(newFloor);
                if (floor.getVisibility())
                {
                    newFloor.SetActive(true);
                }
            }
        }
    }

    private void setupFloor(GameObject floor)
    {
        floor.transform.parent = gameObject.transform;           // set new floor as child of Floors
        floor.transform.localScale = positionObject.localScale;  // set position of new floor to position of PositioningObject
        floor.SetActive(false);
    }
}
