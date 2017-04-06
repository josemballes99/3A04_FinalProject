using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour {

    public Button toggleButton;
    public GameObject UI;

	// Use this for initialization
	void Start () {
        toggleButton.onClick.AddListener(() => togglePanel());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void togglePanel()
    {
        bool active = UI.activeSelf;
        UI.SetActive(!active);
    }
}
