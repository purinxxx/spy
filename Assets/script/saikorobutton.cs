using UnityEngine;
using System.Collections;

public class saikorobutton : MonoBehaviour {

    public void ClickTest() {
        Debug.Log("Clicked.");
        manager.saikoro = true;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
