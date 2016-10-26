using UnityEngine;
using System.Collections;

public class manager : MonoBehaviour {
    public static int[] playerpos = new int[3]; // 各プレイヤーの現在位置を保存するパブリックな配列（とりあえず３人プレイ）
    public static GameObject canvas;
    public static GameObject spycanvas;
    public static bool saikoro = false;
    public static bool terroristtern = true;
    public static bool spy1tern = false;
    public static bool spy2tern = false;
    public static int total;
    GameObject plefab_t;
    GameObject t;
    GameObject plefab_s1;
    GameObject s1;
    GameObject plefab_s2;
    GameObject s2;

    // Use this for initialization

    void Awake() {
        canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);
        spycanvas = GameObject.Find("spyCanvas");
        spycanvas.SetActive(false);
    }
    
    void Start () {
        plefab_t = (GameObject)Resources.Load("terrorist");
        plefab_s1 = (GameObject)Resources.Load("spy1");
        plefab_s2 = (GameObject)Resources.Load("spy2");
        
        total = GameObject.Find("masu").transform.childCount; //30
        int terroristpos = Random.Range(1, total + 1);
        int spy1pos = Random.Range(1, total + 1);
        while (spy1pos == terroristpos) spy1pos = Random.Range(1, total + 1); // プレイヤーの初期位置が重ならないように
        int spy2pos = Random.Range(1, total + 1);
        while (spy2pos == terroristpos || spy2pos == spy1pos) spy2pos = Random.Range(1, total + 1); // プレイヤーの初期位置が重ならないように

        playerpos[0] = terroristpos;
        playerpos[1] = spy1pos;
        playerpos[2] = spy2pos;
        Debug.Log(terroristpos);
        Debug.Log(spy1pos);
        Debug.Log(spy2pos);

        GameObject terroristmasu = GameObject.Find(terroristpos.ToString());
        //terroristmasu.tag = "terrorist"; // テロリストのいるマスにterroristタグをつける
        Vector3 tpos = terroristmasu.transform.position;
        tpos.y += 0.4f; // コマの位置調整
        t = (GameObject)Instantiate(plefab_t, tpos, Quaternion.identity);
        t.name = plefab_t.name;
        //GameObject terrorist = GameObject.Find("terrorist");
        //GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = -1; // テロリストを非表示にする（実際はマップより後ろのレイヤーに追いやっているだけ）

        GameObject spy1masu = GameObject.Find(spy1pos.ToString());
        //spy1masu.tag = "spy1";
        Vector3 s1pos = spy1masu.transform.position;
        s1pos.y += 0.4f;
        s1 = (GameObject)Instantiate(plefab_s1, s1pos, Quaternion.identity);
        s1.name = plefab_s1.name;

        GameObject spy2masu = GameObject.Find(spy2pos.ToString());
        //spy2masu.tag = "spy2";
        Vector3 s2pos = spy2masu.transform.position;
        s2pos.y += 0.4f;
        s2 = (GameObject)Instantiate(plefab_s2, s2pos, Quaternion.identity);
        s2.name = plefab_s2.name;

        //Debug.Log(GameObject.FindWithTag("terrorist").transform.position);

        /*
        Debug.Log(data.map1[0]);
        //GameObject a = GameObject.Find("70");
        GameObject a = GameObject.FindWithTag("terrorist");
        Debug.Log(a);
        Vector3 pos = a.transform.position;
        pos.y += 2;
        a.transform.position = pos;
        */
        
    }
	
	// Update is called once per frame
	void Update () {
        if(terroristtern) GameObject.Find("terrorist").GetComponent<player>().play();
        if (spy1tern) GameObject.Find("spy1").GetComponent<player>().play();
        if (spy2tern) GameObject.Find("spy2").GetComponent<player>().play();

    }
}
