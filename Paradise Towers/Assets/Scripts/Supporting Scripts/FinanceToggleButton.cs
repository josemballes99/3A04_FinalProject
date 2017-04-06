using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinanceToggleButton : MonoBehaviour {

    public Button toggleFinanceButton;
    public GameObject financeUI;

	// Use this for initialization
	void Start () {
        toggleFinanceButton.onClick.AddListener(() => toggleFinances());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void toggleFinances()
    {
        bool active = financeUI.activeSelf;
        financeUI.SetActive(!active);
    }
}
