using UnityEngine;
using System.Collections;

public class saikorobutton : MonoBehaviour {

    public void huru() {
        Debug.Log("さいころをふる");
        manager.saikoro = true;
    }

    public void use()
    {
        Debug.Log("アイテムを使う");
        manager.item = true;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
