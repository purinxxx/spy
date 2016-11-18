using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class spy2 : MonoBehaviour
{

    int k;
    int l;
    int m;
    int me;
    string ternplayer;
    bool mati = false;
    GameObject mituketabom;
    public static int mieru = 0;

    public void play()
    {
        foreach (Transform child in manager.boms.transform) child.GetComponent<Renderer>().sortingOrder = -5;
        if (spy1.mieru > 0) GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
        else if (mieru > 0)
        {
            GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
            mieru -= 1;
        }
        else GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = -5;
        ternplayer = this.gameObject.name;
        Debug.Log(ternplayer + "　スパイ２（青）のターン");
        if (manager.itemspy2.Count > 0) manager.itembutton.SetActive(true);
        manager.saikorobutton.SetActive(true);
        manager.playflag = true;
    }

    void hantei()
    {
        for (int i = 0; i < manager.bompos.Length; ++i)
        {
            if (manager.playerpos[2] == manager.bompos[i]) //スパイが爆弾を踏んだら
            {
                Destroy(GameObject.Find("bom" + manager.bompos[i].ToString()));
                //Destroy(GameObject.Find("spy2"));
                //GameObject.Find("spy2").SetActive(false);
                GameObject.Find("spy2").GetComponent<Renderer>().sortingOrder = -5;
                manager.bompos[i] = 0;
                manager.playerpos[2] = 0;
                mieru = 0;
                Debug.Log("爆弾を踏んでスパイ２死亡");
            }
        }

        //for (int i = 1; i < manager.playerpos.Length; ++i)
        //{
        if (manager.playerpos[0] == manager.playerpos[2]) //スパイがテロリストを踏んだら
        {
            manager.playerpos[0] = 0;
            //Destroy(GameObject.Find("terrorist"));
            Debug.Log("テロリスト死亡");
        }
        //}

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (manager.item && manager.spy2tern)
        {
            manager.itemcanvas.SetActive(true);
            int defaulty = 130;
            foreach (int i in manager.itemspy2)
            {
                Debug.Log(i);
                string str = "item" + i.ToString();
                GameObject plefab_a = (GameObject)Resources.Load(str);
                GameObject a = (GameObject)Instantiate(plefab_a);
                a.name = plefab_a.name;
                //a.transform.parent = manager.itemcanvas.transform;
                a.transform.SetParent(manager.itemcanvas.transform);
                RectTransform a_rect = a.GetComponent<RectTransform>();
                a_rect.anchoredPosition = new Vector2(-130, defaulty);
                defaulty += 70;
            }
            manager.saikorobutton.SetActive(false);
            manager.itembutton.SetActive(false);
            manager.item = false;
        }
        if (manager.modoru && manager.spy2tern)
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag("item");
            foreach (GameObject g in items) Destroy(g);
            manager.saikorobutton.SetActive(true);
            //manager.itembutton.SetActive(true);
            manager.itemcanvas.SetActive(false);
            manager.modoru = false;
        }
        if (manager.saikoro && manager.spy2tern)
        {
            me = Random.Range(1, 7);
            Debug.Log(me.ToString() + "の目が出た");
            if (me <= 3)
            {
                int item = Random.Range(1, 5);
                manager.itemspy2.Add(item);
                Debug.Log(item);
                if (manager.item2)
                {
                    me = me * 2;
                }
                else if (manager.item3)
                {
                    me = me * 3;
                }
            }
            manager.saikoro = false;
            manager.saikorobutton.SetActive(false);
            manager.itembutton.SetActive(false);
            manager.susumubutton.SetActive(true);
            manager.tansakubutton.SetActive(true);
        }

        if (manager.susumu && manager.spy2tern)
        {
            manager.susumu = false;
            manager.susumubutton.SetActive(false);
            manager.tansakubutton.SetActive(false);
            l = manager.playerpos[2];
            k = l + me;
            if (k > manager.total) k -= manager.total; //一周した場合
            manager.playerpos[2] = k;
            //Debug.Log("スパイ2の現在位置" + manager.playerpos[2]);
            Vector3 pos = GameObject.Find(k.ToString()).transform.position;
            pos.y += 0.4f; // コマの位置調整
            GameObject.Find(ternplayer).transform.position = pos;
            hantei();
            mati = true;
        }
        else if (manager.tansaku && manager.spy2tern)
        {
            Debug.Log("探索中");
            manager.tansaku = false;
            manager.susumu = false;
            manager.susumubutton.SetActive(false);
            manager.tansakubutton.SetActive(false);
            l = manager.playerpos[2];
            k = l + me;
            // l-meからl+meまで探索する
            for (int i = -1 * me; i <= me; ++i)
            {
                k = l + i;
                if (k > manager.total) k -= manager.total; //一周した場合
                else if (k < 1) k += manager.total; //一周した場合
                Debug.Log(k);
                //l+i
                for (int j = 1; j <= manager.bompos.Length; ++j)
                {
                    if (l + i == manager.bompos[j - 1])
                    {
                        //爆弾見つけた
                        Debug.Log("爆弾見つけた");
                        GameObject b = GameObject.Find("bom" + (l + i).ToString());
                        b.GetComponent<Renderer>().sortingOrder = 5;
                        manager.bompos[j - 1] = 0;
                        StartCoroutine(bomkesu(b));
                    }
                }
                if (l + i == manager.playerpos[0])
                {
                    //テロリスト見つけた
                    Debug.Log("テロリスト見つけた");
                    GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
                    mieru = 3;
                    if (manager.playerpos[0] == manager.playerpos[2]) manager.playerpos[0] = 0;
                }
            }
            mati = true;
        }


        if (Input.GetMouseButtonDown(0) && manager.spy2tern)
        {
            if (mati)
            {
                mati = false;
                Debug.Log("ターンエンド");
                manager.playflag = false;
                manager.spy2tern = false;
                Debug.Log("スパイ１　" + manager.playerpos[1].ToString() + "　　スパイ２　" + manager.playerpos[2].ToString());
                if (manager.playerpos[0] != 0)
                {
                    manager.terroristtern = true; // 次のターンへ
                }
                else
                {
                    Debug.Log("スパイの勝利");
                }
            }
        }
    }

    private IEnumerator bomkesu(GameObject mituketabom)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(mituketabom);
        Debug.Log("爆弾を除去した " + mituketabom.name);
    }
}