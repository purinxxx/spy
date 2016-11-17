using UnityEngine;
using System.Collections;

public class koudoubutton : MonoBehaviour {

    public void susumu() {
        Debug.Log("進む");
        manager.susumu = true;
    }

    public void tansaku()
    {
        Debug.Log("探索する");
        manager.tansaku = true;
    }

    public void modoru()
    {
        Debug.Log("アイテムを使わないで前の画面に戻る");
        manager.modoru = true;
        manager.itembutton.SetActive(true);
    }

    public void trap()
    {
        Debug.Log("トラップ爆弾を仕掛ける（未実装）");
        //manager.trap = true;
    }

    public void item1()
    {
        Debug.Log("アイテム1");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        //manager.item1 = true;
    }

    public void item2()
    {
        Debug.Log("アイテム2");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        //manager.item2 = true;
    }

    public void item3()
    {
        Debug.Log("アイテム3");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        //manager.item3 = true;
    }

    public void item4()
    {
        Debug.Log("アイテム4");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        //manager.item4 = true;
    }

    public void item5()
    {
        Debug.Log("アイテム5");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        //manager.item5 = true;
    }

    public void item6()
    {
        Debug.Log("アイテム6");
        //manager.item6 = true;
    }

    public void item7()
    {
        Debug.Log("アイテム7");
        //manager.item7 = true;
    }

    public void item8()
    {
        Debug.Log("アイテム8");
        //manager.item8 = true;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
