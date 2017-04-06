using context;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectFloorButtons : MonoBehaviour {

    public GameObject buttonPrefab;
    public HotelManager hotelManager;

    private FloorManager floorManager;

    // Use this for initialization
    void Start()
    {
        floorManager = GameObject.Find("FloorManager").GetComponent<FloorManager>();
    }

    void OnEnable()
    {
        floorManager = GameObject.Find("FloorManager").GetComponent<FloorManager>();
        createSelectButtons();
    }

    void OnDisable()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "ButtonPrefab(Clone)");
        foreach (GameObject button in objects)
        {
            Destroy(button);
        }
    }

    void createSelectButtons()
    {
        List<FloorType> floors = floorManager.getFloors();

        int arcadeNum = 0;
        int restaurantNum = 0;
        int suiteNum = 0;

        foreach (FloorType floor in floors)
        {
            if (floor.getType() == 3)
            {
                continue;
            }

            GameObject goButton = Instantiate(buttonPrefab);
            goButton.transform.SetParent(gameObject.transform, false);
            Button tempButton = goButton.GetComponent<Button>();
            if (floor.getType() == 0)
            {
                arcadeNum++;
                tempButton.GetComponentInChildren<Text>().text = "Arcade " + arcadeNum;
            }
            else if (floor.getType() == 1)
            {
                restaurantNum++;
                tempButton.GetComponentInChildren<Text>().text = "Restaurant " + restaurantNum;
            }
            else if (floor.getType() == 2)
            {
                suiteNum++;
                tempButton.GetComponentInChildren<Text>().text = "Suite " + suiteNum;
            }
            tempButton.onClick.AddListener(() => SelectOnClick(floor));
        }
    }

    void SelectOnClick(FloorType floor)
    {
        floorManager.selectFloor(floor);
        hotelManager.LoadByIndex(0);
    }

}
