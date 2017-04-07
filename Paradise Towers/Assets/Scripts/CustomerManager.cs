using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour {

    public Button moveCustomerButton;
    public Button removeCustomerButton;
    public Button backButton;
    public GameObject moveCustomerCanvas;
    public GameObject people;

    public bool canClickCustomers = true;

	// Use this for initialization
	void Start () {
        GameObject customer;
        foreach (Transform child in people.transform)
        {
            CustomerView customerView = people.transform.Find(child.name).GetComponent<CustomerView>();
            if (customerView.isSelected)
            {
                customer = child.gameObject;
                moveCustomerButton.onClick.AddListener(() => moveCustomerClick(customer));
                removeCustomerButton.onClick.AddListener(() => removeCustomerClick(customer));
                backButton.onClick.AddListener(() => backClick(customer));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        canClickCustomers = false;
    }

    void moveCustomerClick(GameObject customer)
    {
        CustomerView customerView = people.transform.Find(customer.name).GetComponent<CustomerView>();
        customerView.Resume();
        moveCustomerCanvas.SetActive(true);
        gameObject.SetActive(false);
        
    }

    void removeCustomerClick(GameObject customer)
    {
        customer.SetActive(false);
        CustomerView customerView = people.transform.Find(customer.name).GetComponent<CustomerView>();
        customerView.isSelected = false;
        gameObject.SetActive(false);
        canClickCustomers = true;
    }

    void backClick(GameObject customer)
    {
        gameObject.SetActive(false);
        CustomerView customerView = people.transform.Find(customer.name).GetComponent<CustomerView>();
        customerView.isSelected = false;
        canClickCustomers = true;
    }
}
