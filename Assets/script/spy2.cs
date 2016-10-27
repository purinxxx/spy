using UnityEngine;
using System.Collections;

public class spy2 : MonoBehaviour {

    int k;
    int l;
    int m;
    int me;
    string ternplayer;

    public void play()
    {
        GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = -5;
        if (manager.bompos[0] != 0) GameObject.Find("bom1").GetComponent<Renderer>().sortingOrder = -10;
        if (manager.bompos[1] != 0) GameObject.Find("bom2").GetComponent<Renderer>().sortingOrder = -10;
        if (manager.bompos[2] != 0) GameObject.Find("bom3").GetComponent<Renderer>().sortingOrder = -10;
        ternplayer = this.gameObject.name;
        Debug.Log(ternplayer + "のターン");
        manager.saikorobutton.SetActive(true);
        manager.playflag = true;
    }

    // Use this for initialization
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (manager.saikoro && manager.spy2tern)
        {
            me = Random.Range(1, 7);
            Debug.Log(me.ToString() + "の目が出た");
            manager.saikoro = false;
            manager.saikorobutton.SetActive(false);
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
            Debug.Log("スパイ2の現在位置" + manager.playerpos[2]);
            Vector3 pos = GameObject.Find(k.ToString()).transform.position;
            pos.y += 0.4f; // コマの位置調整
            GameObject.Find(ternplayer).transform.position = pos;
            manager.spy2tern = false;
            manager.playflag = false;
            manager.terroristtern = true; // 次のターンへ
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
            // l+1からl+meまで探索する
            for (int i = 1; i <= me; ++i)
            {
                //l+i
                for (int j = 1; j <= manager.bompos.Length; ++j)
                {
                    if (l + i == manager.bompos[j - 1])
                    {
                        //爆弾見つけた
                        Debug.Log("爆弾見つけた");
                    }
                }
                if (l + i == manager.playerpos[0])
                {
                    //テロリスト見つけた
                    Debug.Log("テロリスト見つけた");
                    GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = 10;
                }
            }
            manager.spy2tern = false;
            manager.playflag = false;
            manager.terroristtern = true; // 次のターンへ
        }

    }
}