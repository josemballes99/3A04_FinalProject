using context;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoveCustomer : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject people;
    public Button backButton;
    public CustomerManager customerManager;
    public GameObject floorsObject;

    private FloorManager floorManager;

    // Use this for initialization
    void Start()
    {
        floorManager = GameObject.Find("FloorManager").GetComponent<FloorManager>();

        GameObject customer;
        foreach (Transform child in people.transform)
        {
            CustomerView customerView = people.transform.Find(child.name).GetComponent<CustomerView>();
            if (customerView.isSelected)
            {
                customerView.Stop();
                customer = child.gameObject;
                backButton.onClick.AddListener(() => backClick(customer));
            }
        }

        createFloorButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        floorManager = GameObject.Find("FloorManager").GetComponent<FloorManager>();
        createFloorButtons();
    }

    void OnDisable()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "ButtonPrefab(Clone)");
        foreach (GameObject button in objects)
        {
            Destroy(button);
        }
    }

    void createFloorButtons()
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
            int index = tempButton.transform.GetSiblingIndex();
            tempButton.transform.SetSiblingIndex(index - 1);
            tempButton.onClick.AddListener(() => moveCustomer(tempButton.GetComponentInChildren<Text>().text));
        }
    }

    void moveCustomer(string name)
    {
        foreach (Transform customer in people.transform)
        {
            CustomerView customerView = people.transform.Find(customer.name).GetComponent<CustomerView>();
            if (customerView.isSelected)
            {
                Transform floorObject = floorsObject.transform.Find(name);
                customer.parent = floorObject;
                floorManager.customers.Add(floorObject.name, customer.name);
            }
        }
    }

    void backClick(GameObject customer)
    {
        gameObject.SetActive(false);
        if (people.transform.Find(customer.name) != null)
        {
            CustomerView customerView = people.transform.Find(customer.name).GetComponent<CustomerView>();
            customerView.isSelected = false;
        }
        customerManager.canClickCustomers = true;
    }
}
