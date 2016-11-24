using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class terrorist : MonoBehaviour
{

    bool bakudanphase = false;
    bool bommax = false;
    int k;
    int l;
    int m;
    int me;
    string ternplayer;
    bool mati = false;
    GameObject plefab_b;
    GameObject plefab_b2;
    GameObject b;
    GameObject b2;
    int layerMask;


    public void play()
    {
        ternplayer = this.gameObject.name;
        Debug.Log(ternplayer + "　テロリストのターン");
        manager.message.text = "テロリストのターン";
        Debug.Log(spy1.mieru);
        Debug.Log(spy2.mieru);
        if (manager.koudouseigen[0] > 0)
        {
            Debug.Log("麻酔状態で動けない");
            manager.message.text = "麻酔状態で動けない";
            manager.koudouseigen[0] -= 1;
            mati = true;
        }
        else
        {
            manager.saikorobutton.SetActive(true);
            if (manager.itemterrorist.Count > 0) manager.itembutton.SetActive(true);
        }
        manager.playflag = true;
        foreach (Transform child in manager.boms.transform) child.GetComponent<Renderer>().sortingOrder = 5;
        foreach (Transform child in manager.bom2s.transform) child.GetComponent<Renderer>().sortingOrder = 5;
        GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
    }

    void hantei()
    {
        for (int i = 1; i < manager.playerpos.Length; ++i)
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

    void settihantei(int x)
    {
        for (int j = 1; j <= manager.spy1tpos.Length; ++j)
        {
            if (x == manager.spy1tpos[j - 1])
            {
                Debug.Log("盗聴器を解除した");
                manager.message.text = "盗聴器を解除した";
                GameObject t = GameObject.Find("spy1toutyouki" + (x).ToString());
                t.GetComponent<Renderer>().sortingOrder = 5;
                manager.spy1tpos[j - 1] = 0;
                Destroy(t);
            }
            if (x == manager.spy2tpos[j - 1])
            {
                Debug.Log("盗聴器を解除した");
                manager.message.text = "盗聴器を解除した";
                GameObject t = GameObject.Find("spy2toutyouki" + (x).ToString());
                t.GetComponent<Renderer>().sortingOrder = 5;
                manager.spy2tpos[j - 1] = 0;
                Destroy(t);
            }
        }
    }


    // Use this for initialization
    void Start()
    {
        plefab_b = (GameObject)Resources.Load("bom");
        plefab_b2 = (GameObject)Resources.Load("bom2");
        //layerMask = LayerMask.GetMask(new string[] { LayerMask.LayerToName(8) }); //レイヤーマスクbom
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.terroristtern)
        {
            if (manager.item)
            {
                manager.itemcanvas.SetActive(true);
                int defaulty = 180;
                foreach (int i in manager.itemterrorist)
                {
                    Debug.Log(i);
                    string str = "item" + i.ToString();
                    GameObject plefab_a = (GameObject)Resources.Load(str);
                    GameObject a = (GameObject)Instantiate(plefab_a);
                    a.name = plefab_a.name;
                    a.transform.SetParent(manager.itemcanvas.transform);
                    RectTransform a_rect = a.GetComponent<RectTransform>();
                    a_rect.anchoredPosition = new Vector2(-180, defaulty);
                    a_rect.localScale = new Vector3(1,1,1);
                    defaulty += 120;
                }
                manager.saikorobutton.SetActive(false);
                manager.itembutton.SetActive(false);
                manager.item = false;
            }
            if (manager.modoru)
            {
                GameObject[] items = GameObject.FindGameObjectsWithTag("item");
                foreach (GameObject g in items) Destroy(g);
                manager.saikorobutton.SetActive(true);
                manager.itemcanvas.SetActive(false);
                manager.modoru = false;
            }
            if (manager.item4) //自転車
            {
                manager.item4 = false;
                manager.saikorobutton.SetActive(false);
                manager.itembutton.SetActive(false);
                manager.maebutton.SetActive(true);
                manager.usirobutton.SetActive(true);
            }
            if (manager.mae) //自転車
            {
                manager.mae = false;
                manager.maebutton.SetActive(false);
                manager.usirobutton.SetActive(false);
                me = 3;
                manager.susumu = true;
            }
            if (manager.usiro) //自転車
            {
                manager.usiro = false;
                manager.maebutton.SetActive(false);
                manager.usirobutton.SetActive(false);
                me = -3;
                manager.susumu = true;
            }
            if (manager.item5) //麻酔銃
            {
                manager.item5 = false;
                manager.saikorobutton.SetActive(false);
                manager.itembutton.SetActive(false);
                manager.spy1button.SetActive(true);
                manager.spy2button.SetActive(true);
            }
            if (manager.player_spy1) //麻酔銃
            {
                Debug.Log("スパイ１は次のターン動けない");
                manager.message.text = "スパイ１は次のターン動けない";
                manager.player_spy1 = false;
                manager.spy1button.SetActive(false);
                manager.spy2button.SetActive(false);
                manager.koudouseigen[1] += 1;
                mati = true;
            }
            else if (manager.player_spy2) //麻酔銃
            {
                Debug.Log("スパイ２は次のターン動けない");
                manager.message.text = "スパイ２は次のターン動けない";
                manager.player_spy2 = false;
                manager.spy1button.SetActive(false);
                manager.spy2button.SetActive(false);
                manager.koudouseigen[2] += 1;
                mati = true;
            }
            if (manager.saikoro)
            {
                me = Random.Range(1, 7);
                Debug.Log(me.ToString() + "の目が出た");
                manager.message.text = me.ToString() + "の目が出た";
                if (me <= 2)
                {
                    int item = Random.Range(2, 6);
                    manager.itemterrorist.Add(item);
                    if (item == 1) manager.message.text = me.ToString() + "の目が出た　プロテクターを手に入れた";
                    else if (item == 2) manager.message.text = me.ToString() + "の目が出た　車を手に入れた";
                    else if (item == 3) manager.message.text = me.ToString() + "の目が出た　ヘリを手に入れた";
                    else if (item == 4) manager.message.text = me.ToString() + "の目が出た　自転車を手に入れた";
                    else if (item == 5) manager.message.text = me.ToString() + "の目が出た　麻酔銃を手に入れた";
                    Debug.Log(item);
                }
                if (manager.item2)
                {
                    me = me * 2;
                    manager.item2 = false;
                }
                else if (manager.item3)
                {
                    me = me * 3;
                    manager.item3 = false;
                }

                manager.saikoro = false;
                manager.saikorobutton.SetActive(false);
                manager.itembutton.SetActive(false);
                manager.susumubutton.SetActive(true);
            }
            if (manager.susumu)
            {
                manager.susumu = false;
                manager.susumubutton.SetActive(false);
                //Debug.Log(manager.saikorobutton.activeInHierarchy);
                //Debug.Log(this.gameObject.name);
                l = manager.playerpos[0];
                k = l + me;
                if (k > manager.total) k -= manager.total; //一周した場合
                if (k < 1) k += manager.total; //一周した場合
                manager.playerpos[0] = k;
                //Debug.Log("テロリストの現在位置" + manager.playerpos[0]);
                Vector3 tpos = GameObject.Find(k.ToString()).transform.position;
                tpos.y += 0.4f; // コマの位置調整
                                //GameObject.Find("terrorist").transform.position = pos;
                this.gameObject.transform.position = tpos;
                hantei();
                settihantei(manager.playerpos[0]);

                
                int cnt = 0;
                for (int i = 1; i <= Mathf.Abs(me); ++i)
                {
                    if (me < 0)
                    {
                        m = l + me + i; //後ろに戻るとき
                    }
                    else
                    {
                        m = l + i - 1;
                    }
                    if (m > manager.total) m -= manager.total; //一周した場合
                    if (m < 1) m += manager.total; //一周した場合

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
                    manager.message.text = "爆弾置けない";
                    mati = true;
                }
                else
                {
                    manager.bombutton.SetActive(true);
                    if (manager.bom2pos[0] == 0) manager.bom2button.SetActive(true);
                    manager.nonebutton.SetActive(true);
                }
            }

            if (manager.none)
            {
                manager.bombutton.SetActive(false);
                manager.bom2button.SetActive(false);
                manager.nonebutton.SetActive(false);
                manager.none = false;
                mati = true;
            }

            if (Input.GetMouseButtonDown(0))
            {

                if (manager.bom)
                {
                    // lからkの一個前のマスに爆弾を置ける
                    int j = 1;
                    foreach (int i in manager.bompos)
                    {
                        j = j * i;
                    }
                    if (j != 0)
                    {
                        bommax = true;
                        Debug.Log("いらない爆弾を撤去してから新しい爆弾を設置してください");
                        manager.message.text = "いらない爆弾を撤去してから新しい爆弾を設置してください";
                    }
                    if (bommax)
                    {
                        // http://bribser.co.jp/blog/tappobject/
                        int enablelayer = 1 << LayerMask.NameToLayer("bom");
                        // http://stepism.sakura.ne.jp/unity/wiki/doku.php?id=wiki:unity:tips:073
                        Vector3 bTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Collider2D bCollider2d = Physics2D.OverlapPoint(bTapPoint, enablelayer);
                        //>>>>>>> origin/master
                        if (bCollider2d)
                        {
                            GameObject obj = bCollider2d.transform.gameObject;
                            Debug.Log(obj.name);
                            if (obj.name.Substring(0, 3) == "bom")
                            {
                                int b = int.Parse(obj.name.Substring(3));
                                for (int i = 0; i < manager.bompos.Length; ++i)
                                {
                                    if (manager.bompos[i] == b)
                                    {
                                        Debug.Log("爆弾を撤去しました");
                                        manager.message.text = "爆弾を撤去しました";
                                        bommax = false;
                                        Destroy(obj);
                                        manager.bompos[i] = 0;
                                    }
                                }
                            }
                        }
                    }
                    // http://bribser.co.jp/blog/tappobject/
                    int masulayer = 1 << LayerMask.NameToLayer("masu");
                    Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint, masulayer);
                    if (aCollider2d)
                    {
                        GameObject obj = aCollider2d.transform.gameObject;
                        for (int i = 1; i <= Mathf.Abs(me); ++i) //絶対値meにする
                        {
                            if (me < 0)
                            {
                                m = l + me + i; //後ろに戻るとき
                            }else
                            {
                                m = l + i - 1;
                            }
                            if (m > manager.total) m -= manager.total; //一周した場合
                            if (m < 1) m += manager.total; //一周した場合
                            if (obj.name == m.ToString())
                            {
                                if (m != manager.playerpos[0] && m != manager.playerpos[1] && m != manager.playerpos[2] && m != manager.bom2pos[0] && m != manager.bompos[0] && m != manager.bompos[1] && m != manager.bompos[2])
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
                                        settihantei(m);
                                        manager.bom = false;
                                        mati = true;
                                    }
                                    else if (manager.bompos[1] == 0)
                                    {
                                        manager.bompos[1] = m;
                                        Vector3 bpos = obj.transform.position;
                                        b = (GameObject)Instantiate(plefab_b, bpos, Quaternion.identity);
                                        b.name = "bom" + m.ToString();
                                        b.transform.parent = manager.boms.transform;
                                        settihantei(m);
                                        manager.bom = false;
                                        mati = true;
                                    }
                                    else if (manager.bompos[2] == 0)
                                    {
                                        manager.bompos[2] = m;
                                        Vector3 bpos = obj.transform.position;
                                        b = (GameObject)Instantiate(plefab_b, bpos, Quaternion.identity);
                                        b.name = "bom" + m.ToString();
                                        b.transform.parent = manager.boms.transform;
                                        settihantei(m);
                                        manager.bom = false;
                                        mati = true;
                                    }
                                    else
                                    {
                                        Debug.Log("爆弾上限");
                                        manager.message.text = "爆弾上限";
                                    }
                                }
                            }
                        }
                    }
                }

                else if (manager.bom2)
                {
                    // http://bribser.co.jp/blog/tappobject/
                    int masulayer = 1 << LayerMask.NameToLayer("masu");
                    Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint, masulayer);
                    if (aCollider2d)
                    {
                        GameObject obj = aCollider2d.transform.gameObject;
                        for (int i = 1; i <= Mathf.Abs(me); ++i)
                        {
                            if (me < 0)
                            {
                                m = l + me + i; //後ろに戻るとき
                            }
                            else
                            {
                                m = l + i - 1;
                            }
                            if (m > manager.total) m -= manager.total; //一周した場合
                            if (m < 1) m += manager.total; //一周した場合
                            if (obj.name == m.ToString())
                            {
                                if (m != manager.playerpos[0] && m != manager.playerpos[1] && m != manager.playerpos[2] && m != manager.bom2pos[0] && m != manager.bompos[0] && m != manager.bompos[1] && m != manager.bompos[2])
                                {
                                    // 爆弾置く
                                    if (manager.bom2pos[0] == 0)
                                    {
                                        manager.bom2pos[0] = m;
                                        Vector3 bpos = obj.transform.position;
                                        b = (GameObject)Instantiate(plefab_b2, bpos, Quaternion.identity);
                                        b.name = "bom2" + m.ToString();
                                        b.transform.parent = manager.bom2s.transform;
                                        //int.Parse(b.name.Substring(3))
                                        settihantei(m);
                                        manager.bom2 = false;
                                        mati = true;
                                    }
                                    else
                                    {
                                        Debug.Log("爆弾上限");
                                        manager.message.text = "爆弾上限";
                                    }
                                }
                            }
                        }
                    }
                }

                else if (mati)
                {
                    mati = false;
                    Debug.Log("ターンエンド");
                    manager.message.text = "ターンエンド";
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
                        manager.message.text = "テロリストの勝利";
                    }
                }
            }

        }
    }
}
