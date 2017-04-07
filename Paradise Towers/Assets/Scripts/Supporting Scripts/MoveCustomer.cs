using context;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Mono.Data.Sqlite;
using System.Data;

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
			tempButton.onClick.AddListener(() => moveCustomer(index, tempButton.GetComponentInChildren<Text>().text));
        }
    }

    void moveCustomer(int index, string name)
    {
        foreach (Transform customer in people.transform)
        {
            CustomerView customerView = people.transform.Find(customer.name).GetComponent<CustomerView>();
			if (customerView.isSelected)
			{
				Transform floorObject = floorsObject.transform.Find(name);
				customer.parent = floorObject;
				floorManager.customers.Add(new Tuple<string, string>(floorObject.name, customer.name));


				//Move customers database
				int UID = LobbySpawn.clients.IndexOf(customer.gameObject);
				int fid = index;

				int oldFloor = LobbySpawn.cList[UID];

				IDbConnection connection = Queries.connect (Queries.dbURL);
				IDbCommand command2 = connection.CreateCommand();
				command2.CommandText = "UPDATE Occupants SET num=num-1 WHERE pos=$1";
				command2 = Queries.addParam(command2, DbType.Int32, "1", oldFloor);
				command2.ExecuteNonQuery();

				command2 = connection.CreateCommand();
				command2.CommandText = "UPDATE Occupants SET num=num+1 WHERE pos=$2";
				command2 = Queries.addParam(command2, DbType.Int32, "2", fid);
				command2.ExecuteNonQuery();


				command2.Dispose();
				command2 = null;
				connection.Close ();

				LobbySpawn.cList [UID] = fid;
			}
        }
    }

    void backClick(GameObject customer)
    {
        gameObject.SetActive(false);
        if (customer != null)
        {
            CustomerView customerView = customer.GetComponent<CustomerView>();
            customerView.isSelected = false;
            customerView.Resume();
        }
        else
        {
            CustomerView customerView = people.transform.GetChild(0).GetComponent<CustomerView>();
            customerView.Resume();
        }
        customerManager.canClickCustomers = true;
    }
}
