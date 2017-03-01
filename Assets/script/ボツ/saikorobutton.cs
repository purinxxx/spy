using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class saikorobutton : MonoBehaviour {

    void notuseitem()
    {
        manager.modoru = true;
        //manager.itembutton.SetActive(true);
        manager.itembutton.GetComponent<Button>().interactable = true;
        foreach (Transform child in manager.itemcanvas.transform) if (child.name != "modoru") Destroy(child.gameObject);
    }

    public void huru()
    {
        if (manager.itemcanvas.activeSelf) notuseitem();
        manager.mapbutton.GetComponent<Button>().interactable = true;
        manager.mapwindow.SetActive(false);
        manager.logbutton.GetComponent<Button>().interactable = true;
        manager.logcanvas.GetComponent<Canvas>().enabled = false;
        //manager.logwindow.SetActive(false);
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
        manager.saikorobutton.GetComponent<Button>().interactable = false;
        Debug.Log("さいころをふる");
        manager.saikoro = true;
    }

    public void use()
    {
        manager.mapbutton.GetComponent<Button>().interactable = true;
        manager.mapwindow.SetActive(false);
        manager.logbutton.GetComponent<Button>().interactable = true;
        manager.logcanvas.GetComponent<Canvas>().enabled = false;
        //manager.logwindow.SetActive(false);
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
        manager.itembutton.GetComponent<Button>().interactable = false;
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
