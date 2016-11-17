using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class terrorist : MonoBehaviour {

    bool bakudanphase = false;
    bool bommax = false;
    int k;
    int l;
    int m;
    int me;
    string ternplayer;
    bool mati = false;
    GameObject plefab_b;
    GameObject b;
    int layerMask;



    public void play() {
        ternplayer = this.gameObject.name;
        Debug.Log(ternplayer + "　テロリストのターン");
        Debug.Log(spy1.mieru);
        Debug.Log(spy2.mieru);
        manager.saikorobutton.SetActive(true);
        if(manager.itemterrorist.Count>0) manager.itembutton.SetActive(true);
        manager.playflag = true;
        foreach (Transform child in manager.boms.transform) child.GetComponent<Renderer>().sortingOrder = 5;
        GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
    }

    void hantei() {
        for(int i=1; i< manager.playerpos.Length; ++i)
        {
            //if(manager.playerpos[0]==manager.playerpos[i]) //スパイがテロリストに踏まれたら
            //{
            //    manager.playerpos[i] = 0;
            //    //Destroy(GameObject.Find("spy" + i.ToString()));
            //    GameObject.Find("spy" + i.ToString()).GetComponent<Renderer>().sortingOrder = -5;
            //    Debug.Log(GameObject.Find("spy" + i.ToString()) + "テロリストに殺される");
            //}
        }
    }

    // Use this for initialization
    void Start () {
        plefab_b = (GameObject)Resources.Load("bom");
        layerMask = LayerMask.GetMask(new string[] { LayerMask.LayerToName(8) }); //レイヤーマスクbom
    }
	
	// Update is called once per frame
	void Update () {
        if (manager.item && manager.terroristtern)
        {
            manager.itemcanvas.SetActive(true);
            int defaulty = 130;
            foreach (int i in manager.itemterrorist) {
                Debug.Log(i);
                string str = "item" + i.ToString();
                GameObject plefab_a = (GameObject)Resources.Load(str);
                GameObject a = (GameObject)Instantiate(plefab_a);
                a.name = plefab_a.name;
                a.transform.parent = manager.itemcanvas.transform;
                RectTransform a_rect = a.GetComponent<RectTransform>();
                a_rect.anchoredPosition = new Vector2(-130, defaulty);
                defaulty += 70;
            }
            manager.saikorobutton.SetActive(false);
            manager.itembutton.SetActive(false);
            manager.item = false;
        }
        if (manager.modoru && manager.terroristtern)
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag("item");
            foreach (GameObject g in items) Destroy(g);
            manager.saikorobutton.SetActive(true);
            manager.itemcanvas.SetActive(false);
            manager.modoru = false;
        }
        if (manager.saikoro && manager.terroristtern)
        {
            me = Random.Range(1, 7);
            Debug.Log(me.ToString() + "の目が出た");
            if (me <= 3)
            {
                manager.itemterrorist.Add(Random.Range(1, 5));
                foreach (int i in manager.itemterrorist)
                {
                    Debug.Log(i);
                }
            }
            manager.saikoro = false;
            manager.saikorobutton.SetActive(false);
            manager.itembutton.SetActive(false);
            manager.susumubutton.SetActive(true);
        }
        if (manager.susumu && manager.terroristtern)
        {
            manager.susumu = false;
            manager.susumubutton.SetActive(false);
            //Debug.Log(manager.saikorobutton.activeInHierarchy);
            //Debug.Log(this.gameObject.name);
            l = manager.playerpos[0];
            k = l + me;
            if (k > manager.total) k -= manager.total; //一周した場合
            manager.playerpos[0] = k;
            //Debug.Log("テロリストの現在位置" + manager.playerpos[0]);
            Vector3 tpos = GameObject.Find(k.ToString()).transform.position;
            tpos.y += 0.4f; // コマの位置調整
            //GameObject.Find("terrorist").transform.position = pos;
            this.gameObject.transform.position = tpos;
            hantei();
            // lからkの一個前のマスに爆弾を置ける

            int j=1;
            foreach (int i in manager.bompos)
            {
                j = j * i;
            }
            if (j != 0) {
                bommax = true;
                Debug.Log("いらない爆弾を撤去してから新しい爆弾を設置してください");
            }

            int cnt = 0;
            for (int i = 1; i <= me; ++i)
            {
                m = l + i - 1; //m=l+1,l+2,l+3,,,l+me-1
                if (m > 30) m -= 30;
                foreach (int p in manager.playerpos)
                {
                    if (p == m) ++cnt;
                }
                foreach (int p in manager.bompos)
                {
                    if (p == m) ++cnt;
                }
            }
            if (cnt == me)
            {
                Debug.Log("爆弾置けない");
                mati = true;
            }
            else
            {
                bakudanphase = true;
            }
        }
        
        if (Input.GetMouseButtonDown(0) && manager.terroristtern)
        {
            if (bommax)
            {
                // http://bribser.co.jp/blog/tappobject/
                Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint, layerMask); //レイヤーマスク
                if (aCollider2d)
                {
                    GameObject obj = aCollider2d.transform.gameObject;
                    Debug.Log(obj.name);
                    if (obj.name.Substring(0,3) == "bom")
                    {
                        int b = int.Parse(obj.name.Substring(3));
                        for(int i = 0; i < manager.bompos.Length; ++i)
                        {
                            if (manager.bompos[i] == b)
                            {
                                Debug.Log("爆弾を撤去しました");
                                bommax = false;
                                Destroy(obj);
                                manager.bompos[i] = 0;
                            }
                        }
                    }
                }
            }
            if (bakudanphase && bommax==false)
            {
                // http://bribser.co.jp/blog/tappobject/
                Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);
                if (aCollider2d)
                {
                    GameObject obj = aCollider2d.transform.gameObject;
                    for (int i = 1; i <= me; ++i)
                    {
                        m = l + i - 1;
                        if (m > 30) m -= 30;
                        if (obj.name == m.ToString())
                        {
                            // 爆弾置く
                            if (manager.bompos[0] == 0)
                            {
                                manager.bompos[0] = m;
                                Vector3 bpos = obj.transform.position;
                                b = (GameObject)Instantiate(plefab_b, bpos, Quaternion.identity);
                                b.name = "bom" + m.ToString();
                                b.transform.parent = manager.boms.transform;
                                //int.Parse(b.name.Substring(3))
                            }
                            else if (manager.bompos[1] == 0)
                            {
                                manager.bompos[1] = m;
                                Vector3 bpos = obj.transform.position;
                                b = (GameObject)Instantiate(plefab_b, bpos, Quaternion.identity);
                                b.name = "bom" + m.ToString();
                                b.transform.parent = manager.boms.transform;
                            }
                            else if (manager.bompos[2] == 0)
                            {
                                manager.bompos[2] = m;
                                Vector3 bpos = obj.transform.position;
                                b = (GameObject)Instantiate(plefab_b, bpos, Quaternion.identity);
                                b.name = "bom" + m.ToString();
                                b.transform.parent = manager.boms.transform;
                            }
                            else
                            {
                                Debug.Log("爆弾上限");
                            }
                            bakudanphase = false;
                            mati = true;
                        }
                    }
                }
            }
            else if (mati)
            {
                mati = false;
                Debug.Log("ターンエンド");
                manager.playflag = false;
                manager.terroristtern = false;
                Debug.Log("スパイ１　" + manager.playerpos[1].ToString() + "　　スパイ２　" + manager.playerpos[2].ToString());
                if (manager.playerpos[1] != 0)
                {
                    manager.spy1tern = true; // 次のターンへ
                }
                else if (manager.playerpos[2] != 0)
                {
                    manager.spy2tern = true; // 次のターンへ
                }
                else
                {
                    Debug.Log("テロリストの勝利");
                }
            }
        }

    }
}
