using UnityEngine;
using System.Collections;

public class terrorist : MonoBehaviour {

    bool bakudanphase = false;
    int k;
    int l;
    int m;
    int me;
    string ternplayer;
    bool mati = false;
    GameObject plefab_b;
    GameObject b;

    public void play() {
        ternplayer = this.gameObject.name;
        Debug.Log(ternplayer + "　テロリストのターン");
        manager.saikorobutton.SetActive(true);
        manager.playflag = true;
    }

    void hantei() {
        for(int i=1; i< manager.playerpos.Length; ++i)
        {
            if(manager.playerpos[0]==manager.playerpos[i]) //スパイがテロリストに踏まれたら
            {
                manager.playerpos[i] = 0;
                Destroy(GameObject.Find("spy" + i.ToString()));
                Debug.Log(GameObject.Find("spy" + i.ToString()) + "テロリストに殺される");
            }
        }
    }

    // Use this for initialization
    void Start () {
        plefab_b = (GameObject)Resources.Load("bom");
    }
	
	// Update is called once per frame
	void Update () {
        if (manager.saikoro && manager.terroristtern) {
            foreach (Transform child in manager.boms.transform) child.GetComponent<Renderer>().sortingOrder = 5;
            GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
            me = Random.Range(1, 7);
            Debug.Log(me.ToString() + "の目が出た");
            //Debug.Log(manager.saikorobutton.activeInHierarchy);
            manager.saikorobutton.SetActive(false);
            manager.saikoro = false;
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
            bakudanphase = true;
        }
        
        if (Input.GetMouseButtonDown(0) && manager.terroristtern)
        {
            if (bakudanphase)
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
