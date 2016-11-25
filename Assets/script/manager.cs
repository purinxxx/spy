using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class manager : MonoBehaviour
{
    //public static Text message;
    public static Text message;
    public static int[] playerpos = new int[3]; // 各プレイヤーの現在位置を保存するパブリックな配列（とりあえず３人プレイ）
    public static int[] bompos = { 0, 0, 0 }; // 爆弾上限３個
    public static int[] bom2pos = { 0 }; // 爆弾上限１個
    public static int[] spy1tpos = { 0, 0 }; // 盗聴器上限２個
    public static int[] spy2tpos = { 0, 0 }; // 盗聴器上限２個
    public static int[] spylife = { 1, 1 }; // スパイライフ
    public static int[] koudouseigen = { 0, 0, 0 }; // 行動制限
    public static GameObject canvas;
    public static GameObject itemcanvas;
    public static GameObject saikorobutton;
    public static GameObject itembutton;
    public static GameObject modorubutton;
    public static GameObject susumubutton;
    public static GameObject tansakubutton;
    public static GameObject bombutton;
    public static GameObject bom2button;
    public static GameObject toutyoubutton;
    public static GameObject nonebutton;
    public static GameObject maebutton;
    public static GameObject usirobutton;
    public static GameObject spy1button;
    public static GameObject spy2button;
    public static GameObject terroristbutton;
    public static GameObject boms;
    public static GameObject bom2s;
    public static GameObject ts;
    public static bool saikoro = false;
    public static bool item = false;
    public static bool modoru = false;
    public static bool susumu = false;
    public static bool tansaku = false;
    public static bool bom = false;
    public static bool bom2 = false;
    public static bool toutyou = false;
    public static bool none = false;
    public static bool mae = false;
    public static bool usiro = false;
    public static bool player_spy1 = false;
    public static bool player_spy2 = false;
    public static bool player_terrorist = false;
    public static bool item1 = false;
    public static bool item2 = false;
    public static bool item3 = false;
    public static bool item4 = false;
    public static bool item5 = false;
    //public static bool item6 = false;
    public static bool terroristtern = true;
    public static bool spy1tern = false;
    public static bool spy2tern = false;
    public static int total;
    public static bool playflag = false;
    public static List<int> itemterrorist = new List<int>();
    public static List<int> itemspy1 = new List<int>();
    public static List<int> itemspy2 = new List<int>();
    GameObject plefab_t;
    GameObject t;
    GameObject plefab_s1;
    GameObject s1;
    GameObject plefab_s2;
    GameObject s2;

    // Use this for initialization

    void Awake()
    {
        message = GameObject.Find("message").GetComponentInChildren<Text>();
        canvas = GameObject.Find("Canvas");
        itemcanvas = GameObject.Find("itemCanvas");
        //canvas.SetActive(false);
        //spycanvas = GameObject.Find("spyCanvas");
        //spycanvas.SetActive(false);
        saikorobutton = GameObject.Find("saikoro");
        itembutton = GameObject.Find("item");
        modorubutton = GameObject.Find("modoru");
        saikorobutton.SetActive(false);
        itembutton.SetActive(false);
        itemcanvas.SetActive(false);
        susumubutton = GameObject.Find("susumu");
        susumubutton.SetActive(false);
        tansakubutton = GameObject.Find("tansaku");
        tansakubutton.SetActive(false);
        bombutton = GameObject.Find("bom");
        bombutton.SetActive(false);
        bom2button = GameObject.Find("bom2");
        bom2button.SetActive(false);
        toutyoubutton = GameObject.Find("toutyou");
        toutyoubutton.SetActive(false);
        nonebutton = GameObject.Find("none");
        nonebutton.SetActive(false);
        maebutton = GameObject.Find("mae");
        maebutton.SetActive(false);
        usirobutton = GameObject.Find("usiro");
        usirobutton.SetActive(false);
        spy1button = GameObject.Find("player_spy1");
        spy1button.SetActive(false);
        spy2button = GameObject.Find("player_spy2");
        spy2button.SetActive(false);
        terroristbutton = GameObject.Find("player_terrorist");
        terroristbutton.SetActive(false);
        boms = GameObject.Find("boms");
        bom2s = GameObject.Find("bom2s");
        ts = GameObject.Find("ts");
    }

    void Start()
    {
        //itemspy1.Add(2);

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

        //Debug.Log(terroristpos);
        //Debug.Log(spy1pos);
        //Debug.Log(spy2pos);

        GameObject terroristmasu = GameObject.Find(terroristpos.ToString());
        //terroristmasu.tag = "terrorist"; // テロリストのいるマスにterroristタグをつける
        Vector3 tpos = terroristmasu.transform.position;
        tpos.y += 0.4f; // コマの位置調整
        t = (GameObject)Instantiate(plefab_t, tpos, Quaternion.identity);
        t.name = plefab_t.name;
        t.GetComponent<Renderer>().sortingOrder = -5;
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
    void Update()
    {
        //Debug.Log(playflag);
        if (playflag == false)
        {
            if (playerpos[0] == 0)
            {
                Debug.Log("スパイの勝利");
            }
            else if (playerpos[1] == 0 && playerpos[2] == 0)
            {
                Debug.Log("テロリストの勝利");
            }
            else
            {
                if (terroristtern)
                {
                    foreach (int i in itemterrorist)
                    {
                        Debug.Log("item : " + i.ToString());
                    }
                    GameObject.Find("terrorist").GetComponent<terrorist>().play();
                }
                else if (spy1tern)
                {
                    foreach (int i in itemspy1)
                    {
                        Debug.Log("item : " + i.ToString());
                    }
                    if (playerpos[1]!=0) GameObject.Find("spy1").GetComponent<spy1>().play();
                }
                else if (spy2tern)
                {
                    foreach (int i in itemspy2)
                    {
                        Debug.Log("item : " + i.ToString());
                    }
                    if (playerpos[2] != 0) GameObject.Find("spy2").GetComponent<spy2>().play();
                }
            }
        }
    }
}
