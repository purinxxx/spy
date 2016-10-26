using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {

    public void play() {
        Debug.Log(this.gameObject.name);
        manager.canvas.SetActive(true);
        
    }
    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
