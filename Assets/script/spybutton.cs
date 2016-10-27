using UnityEngine;
using System.Collections;

public class spybutton : MonoBehaviour {

    public void susumu() {
        Debug.Log("進む");
        manager.susumu = true;
    }

    public void tansaku() {
        Debug.Log("探索する");
        manager.tansaku = true;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
