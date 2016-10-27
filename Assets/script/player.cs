using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {

    bool bakudanphase = false;
    int k;
    int l;
    int m;
    int me;
    string ternplayer;
    GameObject plefab_b;
    GameObject b;

    public void play() {
        ternplayer = this.gameObject.name;
        Debug.Log(ternplayer + "のターン");
        manager.canvas.SetActive(true);
    }
    

    // Use this for initialization
    void Start () {
        plefab_b = (GameObject)Resources.Load("bom");
    }
	
	// Update is called once per frame
	void Update () {
        if (manager.saikoro) {
            me = Random.Range(1, 7);
            Debug.Log(me.ToString() + "の目が出た");
            manager.canvas.SetActive(false);
            manager.saikoro = false;
            if (manager.terroristtern) {
                Debug.Log(this.gameObject.name);
                l = manager.playerpos[0];
                k = l + me;
                if (k > manager.total) k -= manager.total; //一周した場合
                manager.playerpos[0] = k;
                Debug.Log("テロリストの現在位置" + manager.playerpos[0]);
                Vector3 pos = GameObject.Find(k.ToString()).transform.position;
                pos.y += 0.4f; // コマの位置調整
                //GameObject.Find("terrorist").transform.position = pos;
                this.gameObject.transform.position = pos;
                // lからkの一個前のマスに爆弾を置ける
                bakudanphase = true;
            } else {
                Debug.Log("ここ");
                manager.spycanvas.SetActive(true);
            }
            manager.terroristtern = false;
        }

        if (manager.susumu && manager.spy1tern) {
            manager.susumu = false;
            Debug.Log(ternplayer + "のターンだよね？");
            string s = ternplayer.Substring(3);
            int i = int.Parse(s);
            Debug.Log(i);
            l = manager.playerpos[i];
            k = l + me;
            if (k > manager.total) k -= manager.total; //一周した場合
            manager.playerpos[i] = k;
            Debug.Log("スパイ１の現在位置" + manager.playerpos[0]);
            Vector3 pos = GameObject.Find(k.ToString()).transform.position;
            pos.y += 0.4f; // コマの位置調整
            GameObject.Find(ternplayer).transform.position = pos;
            manager.spy1tern = false;
        } else if (manager.tansaku) {
            manager.tansaku = false;
        }

        if (Input.GetMouseButtonDown(0)) {
            if (bakudanphase) {
                // http://bribser.co.jp/blog/tappobject/
                Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);
                if (aCollider2d) {
                    GameObject obj = aCollider2d.transform.gameObject;
                    Debug.Log(obj.name);
                    for (int i = 1; i <= me; ++i) {
                        m = l + i - 1;
                        if (m > 30) m -= 30;
                        if (obj.name == m.ToString()) {
                            // 爆弾置く
                            Vector3 bpos = obj.transform.position;
                            b = (GameObject)Instantiate(plefab_b, bpos, Quaternion.identity);
                            bakudanphase = false;
                            manager.spy1tern = true;
                        }
                    }
                }
            }
        }

    }
}
