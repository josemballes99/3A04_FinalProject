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

    void OnEnable()
    {
        canClickCustomers = false;

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

    void moveCustomerClick(GameObject customer)
    {
        CustomerView customerView = customer.GetComponent<CustomerView>();
        customerView.Resume();
        moveCustomerCanvas.SetActive(true);
		gameObject.SetActive(false);
    }

    void removeCustomerClick(GameObject customer)
    {
        customer.SetActive(false);
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
        gameObject.SetActive(false);
        canClickCustomers = true;
    }

    void backClick(GameObject customer)
    {
        gameObject.SetActive(false);
        if (customer != null)
        {
            CustomerView customerView = customer.GetComponent<CustomerView>();
            customerView.isSelected = false;
            customerView.Resume();

        } else
        {
            CustomerView customerView = people.transform.GetChild(0).GetComponent<CustomerView>();
            customerView.Resume();
        }
        canClickCustomers = true;
    }
}
