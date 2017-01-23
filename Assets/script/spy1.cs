using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class spy1 : MonoBehaviour
{
    bool go = false;
    bool bagutubusi = false;
    int k;
    int l;
    int m;
    int me;
    int idoume;
    string ternplayer;
    public static bool mati = false;
    GameObject mituketabom;
    public static int mieru = 0;
    bool setti = false;
    bool tmax = false;
    GameObject plefab_t;
    GameObject t;
    Vector3 pos;
    Vector3 camerapos = manager.maincamera.transform.position;

    public void play()
    {
        bagutubusi = true;
        camerapos.x = this.transform.position.x;
        camerapos.y = this.transform.position.y;
        camerapos.z = -10;
        manager.maincamera.transform.position = camerapos;
        Debug.Log(camerapos);
        foreach (Transform child in manager.boms.transform) child.GetComponent<Renderer>().sortingOrder = -5;
        foreach (Transform child in manager.bom2s.transform) child.GetComponent<Renderer>().sortingOrder = -5;
        if (spy2.mieru > 0) GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
        else if (mieru > 0)
        {
            GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
            mieru -= 1;
        }
        else GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = -5;
        ternplayer = this.gameObject.name;
        Debug.Log(ternplayer + "　スパイ１（緑）のターン");
        manager.message.text = "スパイ１（緑）のターン";
        if (manager.spy1tpos[0] != 0) toutyousuru(manager.spy1tpos[0]);
        if (manager.spy1tpos[1] != 0) toutyousuru(manager.spy1tpos[1]);
        if (manager.spy2tpos[0] != 0) toutyousuru(manager.spy2tpos[0]);
        if (manager.spy2tpos[1] != 0) toutyousuru(manager.spy2tpos[1]);
        if (manager.koudouseigen[1] > 0)
        {
            Debug.Log("麻酔状態で動けない");
            manager.message.text = "麻酔状態で動けない";
            manager.koudouseigen[1] -= 1;
            mati = true;
        }
        else
        {
            //manager.saikorobutton.SetActive(true);
            manager.saikorobutton.GetComponent<Button>().interactable = true;
            if (manager.itemspy1.Count > 0) manager.itembutton.GetComponent<Button>().interactable = true;
        }
        manager.playflag = true;
    }

    void hantei()
    {
        for (int i = 0; i < manager.bompos.Length; ++i)
        {
            if (manager.playerpos[1] == manager.bompos[i]) //スパイが爆弾を踏んだら
            {
                manager.spylife[0] -= 1;
                Destroy(GameObject.Find("bom" + manager.bompos[i].ToString()));
                manager.bompos[i] = 0;
                if (manager.spylife[0] == 0)
                {
                    GameObject.Find("spy1").GetComponent<Renderer>().sortingOrder = -5;
                    manager.playerpos[1] = 0;
                    mieru = 0;
                    Debug.Log("爆弾を踏んでスパイ１死亡");
                    manager.message.text = "爆弾を踏んでスパイ１死亡";
                    mati = true;
                    break;
                }else
                {
                    Debug.Log("爆弾を踏んだがプロテクターに守られた");
                    manager.message.text = "爆弾を踏んだがプロテクターに守られた";
                }
            }
        }


        //for (int i = 1; i < manager.playerpos.Length; ++i)
        //{
        if (manager.playerpos[0] == manager.playerpos[1]) //スパイがテロリストを踏んだら
        {
            manager.playerpos[0] = 0;
            //Destroy(GameObject.Find("terrorist"));
            Debug.Log("テロリスト死亡");
            manager.message.text = "テロリスト死亡";
            mati = true;
        }
        //}

        if (manager.playerpos[1] == -1)
        {
            manager.playerpos[1] = 0;
            mati = true;
        }

    }

    void toutyousuru(int x)
    {
        l = x;
        k = l + 3;
        // l-3からl+3まで探索する
        for (int i = -1 * 3; i <= 3; ++i)
        {
            k = l + i;
            if (k > manager.total) k -= manager.total; //一周した場合
            else if (k < 1) k += manager.total; //一周した場合
            Debug.Log(k);
            //l+i
            for (int j = 1; j <= manager.bompos.Length; ++j)
            {
                if (k == manager.bompos[j - 1])
                {
                    //爆弾見つけた
                    Debug.Log("爆弾見つけた");
                    manager.message.text = "爆弾見つけた";
                    GameObject b = GameObject.Find("bom" + (k).ToString());
                    b.GetComponent<Renderer>().sortingOrder = 5;
                }
            }
            if (k == manager.bom2pos[0])
            {
                //トラップ爆弾見つけた
                Debug.Log("トラップ爆弾見つけた");
                manager.message.text = "トラップ爆弾見つけた";
                GameObject b = GameObject.Find("bom2" + (k).ToString());
                b.GetComponent<Renderer>().sortingOrder = 5;
            }
            if (k == manager.playerpos[0])
            {
                //テロリスト見つけた
                Debug.Log("テロリスト見つけた");
                manager.message.text = "テロリスト見つけた";
                GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
            }
        }
    }

    void settihantei(int x)
    {
        for (int j = 1; j <= manager.bompos.Length; ++j)
        {
            if (x == manager.bompos[j - 1])
            {
                Debug.Log("爆弾見つけた");
                manager.message.text = "爆弾見つけた";
                GameObject b = GameObject.Find("bom" + (x).ToString());
                b.GetComponent<Renderer>().sortingOrder = 5;
                manager.bompos[j - 1] = 0;
                StartCoroutine(bomkesu(b));
            }
        }
        if (x == manager.bom2pos[0])
        {
            Debug.Log("トラップ爆弾見つけた");
            manager.message.text = "トラップ爆弾見つけた";
            GameObject b = GameObject.Find("bom2" + (x).ToString());
            b.GetComponent<Renderer>().sortingOrder = 5;
            manager.bom2pos[0] = 0;
            StartCoroutine(bomkesu(b));
        }

    }

    // Use this for initialization
    void Start()
    {
        plefab_t = (GameObject)Resources.Load("toutyouki");
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.spy1tern)
        {
            if (bagutubusi)
            {
                Debug.Log("バグ潰し！");
                manager.maincamera.transform.position = camerapos;
                Swipe.prevX = camerapos.x;
                Swipe.prevY = camerapos.y;
                bagutubusi = false;
                StartCoroutine(mukouka());
            }
            if (manager.item)
            {
                manager.maincamera.transform.position = camerapos;
                manager.itemcanvas.SetActive(true);
                int defaulty = 180;
                foreach (int i in manager.itemspy1)
                {
                    Debug.Log(i);
                    string str = "item" + i.ToString();
                    GameObject plefab_a = (GameObject)Resources.Load(str);
                    GameObject a = (GameObject)Instantiate(plefab_a);
                    a.name = plefab_a.name;
                    //a.transform.parent = manager.itemcanvas.transform;
                    a.transform.SetParent(manager.itemcanvas.transform);
                    RectTransform a_rect = a.GetComponent<RectTransform>();
                    a_rect.anchoredPosition = new Vector2(-180, defaulty);
                    a_rect.localScale = new Vector3(1, 1, 1);
                    defaulty += 120;
                }
                //manager.saikorobutton.SetActive(false);
                //manager.itembutton.SetActive(false);
                manager.itembutton.GetComponent<Button>().interactable = false;
                manager.item = false;
            }
            if (manager.modoru)
            {
                GameObject[] items = GameObject.FindGameObjectsWithTag("item");
                foreach (GameObject g in items) Destroy(g);
                //manager.saikorobutton.SetActive(true);
                manager.saikorobutton.GetComponent<Button>().interactable = true;
                //manager.itembutton.SetActive(true);
                manager.itemcanvas.SetActive(false);
                manager.modoru = false;
            }
            if (manager.item4) //自転車
            {
                manager.item4 = false;
                //manager.saikorobutton.SetActive(false);
                manager.saikorobutton.GetComponent<Button>().interactable = false;
                //manager.itembutton.SetActive(false);
                manager.itembutton.GetComponent<Button>().interactable = false;
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
                //manager.saikorobutton.SetActive(false);
                manager.saikorobutton.GetComponent<Button>().interactable = false;
                //manager.itembutton.SetActive(false);
                manager.itembutton.GetComponent<Button>().interactable = false;
                manager.terroristbutton.SetActive(true);
                manager.spy2button.SetActive(true);
            }
            if (manager.player_terrorist) //麻酔銃
            {
                Debug.Log("テロリストは次のターン動けない");
                manager.message.text = "テロリストは次のターン動けない";
                manager.player_terrorist = false;
                manager.terroristbutton.SetActive(false);
                manager.spy2button.SetActive(false);
                manager.koudouseigen[0] += 1;
                //mati = true;
                //manager.saikorobutton.SetActive(true);
                manager.saikorobutton.GetComponent<Button>().interactable = true;
            }
            else if (manager.player_spy2) //麻酔銃
            {
                Debug.Log("スパイ２は次のターン動けない");
                manager.message.text = "スパイ２は次のターン動けない";
                manager.player_spy2 = false;
                manager.terroristbutton.SetActive(false);
                manager.spy2button.SetActive(false);
                manager.koudouseigen[2] += 1;
                //mati = true;
                //manager.saikorobutton.SetActive(true);
                manager.saikorobutton.GetComponent<Button>().interactable = true;
            }
            if (manager.item1)
            {
                //manager.saikorobutton.SetActive(false);
                manager.saikorobutton.GetComponent<Button>().interactable = false;
                //manager.itembutton.SetActive(false);
                manager.itembutton.GetComponent<Button>().interactable = false;
                manager.item1 = false;
                manager.spylife[0] += 1;
                //mati = true;
                //manager.saikorobutton.SetActive(true);
                manager.saikorobutton.GetComponent<Button>().interactable = true;
            }
            if (manager.saikoro)
            {
                manager.maincamera.transform.position = camerapos;
                me = Random.Range(1, 7);
                Debug.Log(me.ToString() + "の目が出た");
                manager.message.text = me.ToString() + "の目が出た";
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
                //manager.saikorobutton.SetActive(false);
                manager.saikorobutton.GetComponent<Button>().interactable = false;
                //manager.itembutton.SetActive(false);
                manager.itembutton.GetComponent<Button>().interactable = false;
                manager.susumubutton.SetActive(true);
                manager.tansakubutton.SetActive(true);
            }

            if (manager.susumu)
            {
                manager.maincamera.transform.position = camerapos;
                manager.susumu = false;
                manager.susumubutton.SetActive(false);
                manager.tansakubutton.SetActive(false);
                l = manager.playerpos[1];
                for (int i = 1; i <= Mathf.Abs(me); ++i)
                {
                    if (me < 0)
                    {
                        k = l - i; //後ろに戻るとき
                    }
                    else
                    {
                        k = l + i;
                    }
                    if (k > manager.total) k -= manager.total; //一周した場合
                    if (k < 1) k += manager.total; //一周した場合

                    //pos = GameObject.Find(k.ToString()).transform.position;
                    //pos.y += 0.4f; // コマの位置調整
                    //GameObject.Find(ternplayer).transform.position = pos;
                    manager.playerpos[1] = k;
                    pos = GameObject.Find(k.ToString()).transform.position;
                    pos.y += 0.5f; // コマの位置調整
                    camerapos.x = pos.x;
                    camerapos.y = pos.y;
                    camerapos.z = -10;
                    StartCoroutine(idou(pos, camerapos, i));
                    if (k == manager.bom2pos[0])
                    {
                        manager.spylife[0] -= 1;
                        Destroy(GameObject.Find("bom2" + manager.bom2pos[0].ToString()));
                        manager.bom2pos[0] = 0;
                        if (manager.spylife[0] == 0)
                        {
                            Debug.Log("トラップ爆弾に引っかかりスパイ１死亡");
                            manager.message.text = "トラップ爆弾に引っかかりスパイ１死亡";
                            GameObject.Find("spy1").GetComponent<Renderer>().sortingOrder = -5;
                            manager.playerpos[1] = -1;
                            mieru = 0;
                            break;
                        }else
                        {
                            Debug.Log("トラップ爆弾に引っかかったがプロテクターに守られた");
                            manager.message.text = "トラップ爆弾に引っかかったがプロテクターに守られた";
                        }
                    }
                }
                StartCoroutine(matu(me));
                //Debug.Log("スパイ1の現在位置" + manager.playerpos[1]);
                //Vector3 pos = GameObject.Find(k.ToString()).transform.position;
                //pos.y += 0.4f; // コマの位置調整
                //GameObject.Find(ternplayer).transform.position = pos;
                
            }
            if (go)
            {
                go = false;
                hantei();
                if (GameObject.Find(manager.playerpos[1].ToString()).transform.tag == "itemmasu")
                {
                    int item = Random.Range(1, 6);
                    manager.itemspy1.Add(item);
                    if (item == 1) manager.message.text = "プロテクターを手に入れた";
                    else if (item == 2) manager.message.text = "車を手に入れた";
                    else if (item == 3) manager.message.text = "ヘリを手に入れた";
                    else if (item == 4) manager.message.text = "自転車を手に入れた";
                    else if (item == 5) manager.message.text = "麻酔銃を手に入れた";
                    Debug.Log(item);
                }
                if (manager.playerpos[0] > 0 && manager.playerpos[1] > 0)
                {
                    manager.toutyoubutton.SetActive(true);
                    manager.nonebutton.SetActive(true);
                }
            }
            else if (manager.tansaku)
            {
                manager.message.text = "探索中";
                manager.tansaku = false;
                manager.susumubutton.SetActive(false);
                manager.tansakubutton.SetActive(false);
                l = manager.playerpos[1];
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
                        if (k == manager.bompos[j - 1])
                        {
                            //爆弾見つけた
                            Debug.Log("爆弾見つけた");
                            manager.message.text = "爆弾見つけた";
                            GameObject b = GameObject.Find("bom" + (k).ToString());
                            b.GetComponent<Renderer>().sortingOrder = 5;
                            manager.bompos[j - 1] = 0;
                            StartCoroutine(bomkesu(b));
                        }
                    }
                    if (k == manager.bom2pos[0])
                    {
                        //トラップ爆弾見つけた
                        Debug.Log("トラップ爆弾見つけた");
                        manager.message.text = "トラップ爆弾見つけた";
                        GameObject b = GameObject.Find("bom2" + (k).ToString());
                        b.GetComponent<Renderer>().sortingOrder = 5;
                        manager.bom2pos[0] = 0;
                        StartCoroutine(bomkesu(b));
                    }
                    if (k == manager.playerpos[0])
                    {
                        //テロリスト見つけた
                        Debug.Log("テロリスト見つけた");
                        manager.message.text = "テロリスト見つけた";
                        GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
                        mieru = 3;
                        if (manager.playerpos[0] == manager.playerpos[1]) manager.playerpos[0] = 0;
                        manager.message.text = "スパイの勝利 ";
                    }
                }
                Debug.Log("探索終了");
                pos = this.transform.position;
                mati = true;
            }

            if (manager.toutyou)
            {
                manager.toutyou = false;
                manager.toutyoubutton.SetActive(false);
                manager.nonebutton.SetActive(false);
                setti = true;
            }
            else if (manager.none)
            {
                manager.none = false;
                manager.toutyoubutton.SetActive(false);
                manager.nonebutton.SetActive(false);
                mati = true;
            }

            if (mati)
            {
                manager.message.text = "画面をタッチして次のプレイヤーに変わってください。";
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (setti)
                {
                    // lからkの一個前のマスに盗聴器を置ける
                    int j = 1;
                    foreach (int i in manager.spy1tpos)
                    {
                        j = j * i;
                    }
                    if (j != 0)
                    {
                        tmax = true;
                        Debug.Log("いらない盗聴器を撤去してから新しい盗聴器を設置してください");
                        manager.message.text = "いらない盗聴器を撤去してから新しい盗聴器を設置してください";
                    }
                    if (tmax)
                    {
                        // http://bribser.co.jp/blog/tappobject/
                        int enablelayer = 1 << LayerMask.NameToLayer("toutyouki");
                        // http://stepism.sakura.ne.jp/unity/wiki/doku.php?id=wiki:unity:tips:073
                        Vector3 bTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Collider2D bCollider2d = Physics2D.OverlapPoint(bTapPoint, enablelayer);
                        //>>>>>>> origin/master
                        if (bCollider2d)
                        {
                            GameObject obj = bCollider2d.transform.gameObject;
                            Debug.Log(obj.name);
                            if (obj.name.Substring(0, 13) == "spy1toutyouki")
                            {
                                int t = int.Parse(obj.name.Substring(13));
                                for (int i = 0; i < manager.spy1tpos.Length; ++i)
                                {
                                    if (manager.spy1tpos[i] == t)
                                    {
                                        Debug.Log("盗聴器を撤去しました");
                                        manager.message.text = "盗聴器を撤去しました";
                                        tmax = false;
                                        Destroy(obj);
                                        manager.spy1tpos[i] = 0;
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
                        Debug.Log(obj.name);
                        for (int i = 1; i <= Mathf.Abs(me); ++i) //絶対値meにする
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
                                // 盗聴器置く
                                if (manager.spy1tpos[0] == 0)
                                {
                                    manager.spy1tpos[0] = m;
                                    Vector3 tpos = obj.transform.position;
                                    t = (GameObject)Instantiate(plefab_t, tpos, Quaternion.identity);
                                    t.name = "spy1toutyouki" + m.ToString();
                                    t.transform.parent = manager.ts.transform;
                                    //int.Parse(b.name.Substring(3))
                                    settihantei(m);
                                    setti = false;
                                    mati = true;
                                }
                                else if (manager.spy1tpos[1] == 0)
                                {
                                    manager.spy1tpos[1] = m;
                                    Vector3 tpos = obj.transform.position;
                                    t = (GameObject)Instantiate(plefab_t, tpos, Quaternion.identity);
                                    t.name = "spy1toutyouki" + m.ToString();
                                    t.transform.parent = manager.ts.transform;
                                    settihantei(m);
                                    setti = false;
                                    mati = true;
                                }
                                else
                                {
                                    Debug.Log("盗聴器上限");
                                    manager.message.text = "盗聴器上限";
                                }
                            }
                        }
                    }
                }
                else if (mati && this.transform.position==pos)
                {
                    GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = false;
                    mati = false;
                    Debug.Log("ターンエンド");
                    manager.message.text = "ターンエンド";
                    manager.playflag = false;
                    manager.spy1tern = false;
                    Debug.Log("スパイ１　" + manager.playerpos[1].ToString() + "　　スパイ２　" + manager.playerpos[2].ToString());
                    if (manager.playerpos[1] == 0 && manager.playerpos[2] == 0)
                    {
                        manager.message.text = "テロリストの勝利 ";
                    }
                    else if (manager.playerpos[0] == 0)
                    {
                        manager.message.text = "スパイの勝利 ";
                    }
                    else if (manager.playerpos[2] != 0)
                    {
                        manager.spy2tern = true; // 次のターンへ
                    }
                    else if (manager.playerpos[0] != 0)
                    {
                        manager.terroristtern = true; // 次のターンへ
                    }
                    else
                    {
                        manager.message.text = "その他 ";
                    }
                }
            }
        }
    }

    private IEnumerator bomkesu(GameObject mituketabom)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(mituketabom);
        Debug.Log("爆弾を除去した " + mituketabom.name);
        manager.message.text = "爆弾を除去した " + mituketabom.name;
    }

    private IEnumerator idou(Vector3 player, Vector3 camera, int time)
    {
        time = Mathf.Abs(time);
        yield return new WaitForSeconds(time * 0.5f);
        //this.gameObject.transform.position = player;
        //manager.maincamera.transform.position = camera;
        //this.gameObject.GetComponent<Rigidbody2D>().MovePosition(player);
        //manager.maincamera.GetComponent<Rigidbody2D>().MovePosition(camera);
        float step = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(transform.position.x - player.x), 2) + Mathf.Pow(Mathf.Abs(transform.position.x - player.x), 2)) / 10;
        Debug.Log(step);
        step = 0.15f;
        while (transform.position != player)
        {
            yield return new WaitForSeconds(0.01f);
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, player, step);
            manager.maincamera.transform.position = Vector3.MoveTowards(manager.maincamera.transform.position, camera, step);
        }
        Debug.Log(time);
        idoume = time;
    }

    private IEnumerator matu(int me)
    {
        while (Mathf.Abs(me) != idoume)
        {
            yield return new WaitForSeconds(0.01f);
        }
        go = true;
    }

    private IEnumerator mukouka()
    {
        Debug.Log("コルーチン");
        yield return new WaitForSeconds(0.1f);
        Swipe.prevX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        Swipe.prevY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
    }
}
