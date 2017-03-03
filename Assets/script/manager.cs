using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class manager : MonoBehaviour
{
	bool gameend=false;

	public static AudioSource bgm;
	public static AudioSource win;
	public AudioClip soundwin;
	AudioSource audio;

	public static Text itemtitle;
	public static Text itemsentence;
	public static int selecteditem = 0;
    public static Text message;
    public static Text logcontent;
    public static int[] playerpos = new int[3]; // 各プレイヤーの現在位置を保存するパブリックな配列（とりあえず３人プレイ）
    public static int[] bompos = { 0, 0, 0 }; // 爆弾上限３個
    public static int[] bom2pos = { 0 }; // 爆弾上限１個
    public static int[] spy1tpos = { 0, 0 }; // 盗聴器上限２個
    public static int[] spy2tpos = { 0, 0 }; // 盗聴器上限２個
    public static int[] spylife = { 1, 1 }; // スパイライフ
    public static int[] koudouseigen = { 0, 0, 0 }; // 行動制限
    public static GameObject maincamera;
    public static GameObject canvas;
    public static GameObject itemcanvas;
	public static GameObject logcanvas;
	public static GameObject wincanvas;
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
	public static GameObject mapwindow;
	public static GameObject itemwindow;
    public static GameObject logwindow;
    public static GameObject mapbutton;
	public static GameObject logbutton;
	public static GameObject settingbutton;
	public static GameObject koudou;
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

    public static void player_direction(int who, int j)
    {
        float direction;
        if (j > total) j -= total; //一周した場合
        if (j < 1) j += total; //一周した場合
        int k = j + 1;
        if (k > total) k -= total; //一周した場合
        if (k < 1) k += total; //一周した場合
        float x1 = GameObject.Find(j.ToString()).transform.position.x;
        float x2 = GameObject.Find(k.ToString()).transform.position.x;
        if (x1 > x2) direction = 0.1f;
        else direction = -0.1f;
        if (who==0) GameObject.Find("terrorist").transform.localScale = new Vector3(direction, 0.1f, 0.1f);
        if (who==1) GameObject.Find("spy1").transform.localScale = new Vector3(direction, 0.1f, 0.1f);
        if (who==2) GameObject.Find("spy2").transform.localScale = new Vector3(direction, 0.1f, 0.1f);
    }


    void Awake()
	{
		maincamera = GameObject.Find("Main Camera");
		itemtitle = GameObject.Find("title").GetComponentInChildren<Text>();
		itemsentence = GameObject.Find("sentence").GetComponentInChildren<Text>();
		message = GameObject.Find("message").GetComponentInChildren<Text>();
        logcontent = GameObject.Find("LogContent").GetComponentInChildren<Text>();
        canvas = GameObject.Find("Canvas");
		itemcanvas = GameObject.Find("itemCanvas");
		logcanvas = GameObject.Find("LogCanvas");
		wincanvas = GameObject.Find("winCanvas");
        //canvas.SetActive(false);
        //spycanvas = GameObject.Find("spyCanvas");
        //spycanvas.SetActive(false);
        saikorobutton = GameObject.Find("saikoro");
        itembutton = GameObject.Find("item");
        modorubutton = GameObject.Find("modoru");
        //saikorobutton.SetActive(false);
        //itembutton.SetActive(false);
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
		itemwindow = GameObject.Find("itemwindow");
		itemwindow.SetActive(false);
		mapwindow = GameObject.Find("mapwindow");
		mapwindow.SetActive(false);
        logwindow = GameObject.Find("logwindow");
        logwindow.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        //logwindow.SetActive(false);
        manager.logcanvas.GetComponent<Canvas>().enabled = false;
        mapbutton = GameObject.Find("map");
		logbutton = GameObject.Find("log");
		settingbutton = GameObject.Find("setting");
		koudou = GameObject.Find("koudou");
		koudou.SetActive(false);
		//Debug.Log ("Awake");
    }

    void Start()
	{
		//初期化
		bompos[0] = 0;
		bompos[1] = 0;
		bompos[2] = 0;
		bom2pos[0] = 0;
		spy1tpos[0] = 0;
		spy1tpos[1] = 0;
		spy2tpos[0] = 0;
		spy2tpos[1] = 0;
		spylife[0] = 1;
		spylife[1] = 1;
		koudouseigen[0] = 0;
		koudouseigen[1] = 0;
		koudouseigen[2] = 0;
		selecteditem = 0;
		saikoro = false;
		item = false;
		modoru = false;
		susumu = false;
		tansaku = false;
		bom = false;
		bom2 = false;
		toutyou = false;
		none = false;
		mae = false;
		usiro = false;
		player_spy1 = false;
		player_spy2 = false;
		player_terrorist = false;
		item1 = false;
		item2 = false;
		item3 = false;
		item4 = false;
		item5 = false;
		terroristtern = true;
		spy1tern = false;
		spy2tern = false;
		playflag = false;
		itemterrorist = new List<int>();
		itemspy1 = new List<int>();
		itemspy2 = new List<int>();



		audio = GetComponent<AudioSource>();
		AudioSource[] audioSources = GetComponents<AudioSource>();
		bgm = audioSources[0];
		win = audioSources[1];
		bgm.Play ();

        plefab_t = (GameObject)Resources.Load("terrorist");
        plefab_s1 = (GameObject)Resources.Load("spy1");
        plefab_s2 = (GameObject)Resources.Load("spy2");


        total = GameObject.Find("masu").transform.childCount; //85
        int terroristpos = Random.Range(1, total + 1);
        int spy1pos = Random.Range(1, total + 1);
        while (spy1pos == terroristpos) spy1pos = Random.Range(1, total + 1); // プレイヤーの初期位置が重ならないように
        int spy2pos = Random.Range(1, total + 1);
        while (spy2pos == terroristpos || spy2pos == spy1pos) spy2pos = Random.Range(1, total + 1); // プレイヤーの初期位置が重ならないように

		//デバッグ用
		/*
		itemspy1.Add(2);
		itemspy1.Add(3);
		itemspy1.Add(4);
		itemspy2.Add(2);
		itemspy2.Add(3);
		itemspy2.Add(4);
		itemterrorist.Add(2);
		itemterrorist.Add(3);
		itemterrorist.Add(4);
		itemterrorist.Add(5);
		itemterrorist.Add(3);

		terroristpos = 4;
		spy1pos = 2;
		spy2pos = 3;
		*/

        playerpos[0] = terroristpos;
        playerpos[1] = spy1pos;
        playerpos[2] = spy2pos;

        //Debug.Log(terroristpos);
        //Debug.Log(spy1pos);
        //Debug.Log(spy2pos);

        GameObject terroristmasu = GameObject.Find(terroristpos.ToString());
        //terroristmasu.tag = "terrorist"; // テロリストのいるマスにterroristタグをつける
        Vector3 tpos = terroristmasu.transform.position;
        tpos.y += 0.5f; // コマの位置調整
        t = (GameObject)Instantiate(plefab_t, tpos, Quaternion.identity);
        t.name = plefab_t.name;
        t.GetComponent<Renderer>().sortingOrder = -5;
        //GameObject terrorist = GameObject.Find("terrorist");
        //GameObject.Find("terrorist").GetComponent<Renderer>().sortingOrder = -1; // テロリストを非表示にする（実際はマップより後ろのレイヤーに追いやっているだけ）

        GameObject spy1masu = GameObject.Find(spy1pos.ToString());
        //spy1masu.tag = "spy1";
        Vector3 s1pos = spy1masu.transform.position;
        s1pos.y += 0.5f;
        s1 = (GameObject)Instantiate(plefab_s1, s1pos, Quaternion.identity);
        s1.name = plefab_s1.name;

        GameObject spy2masu = GameObject.Find(spy2pos.ToString());
        //spy2masu.tag = "spy2";
        Vector3 s2pos = spy2masu.transform.position;
        s2pos.y += 0.5f;
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

		//Debug.Log ("Start");
    }

    // Update is called once per frame
    void Update()
	{
		//Debug.Log ("Update");
		if (gameend == false) {
			//Debug.Log(playflag);
			if (playflag == false) {
				if (playerpos [0] == 0) {
					bgm.Stop ();
					win.Play ();
					if (playerpos [1] == 0) {
						//Debug.Log ("スパイ２の勝利");
						StartCoroutine (wingamen (2));
					}
					if (playerpos [2] == 0) {
						//Debug.Log ("スパイ１の勝利");
						StartCoroutine (wingamen (1));
					}
				} else if (playerpos [1] == 0 && playerpos [2] == 0) {
					//bgm.Stop ();
					//win.Play ();
					//wincanvas.GetComponent<Canvas>().enabled = true;
					//Debug.Log("テロリストの勝利");
				} else {
					//Debug.Log (terroristtern);
					if (terroristtern) {
						foreach (int i in itemterrorist) {
							//Debug.Log ("item : " + i.ToString ());
						}
						GameObject.Find ("terrorist").GetComponent<terrorist> ().play ();
					} else if (spy1tern) {
						foreach (int i in itemspy1) {
							//Debug.Log ("item : " + i.ToString ());
						}
						if (playerpos [1] != 0)
							GameObject.Find ("spy1").GetComponent<spy1> ().play ();
					} else if (spy2tern) {
						foreach (int i in itemspy2) {
							//Debug.Log ("item : " + i.ToString ());
						}
						if (playerpos [2] != 0)
							GameObject.Find ("spy2").GetComponent<spy2> ().play ();
					}
				}
			}
		}
    }

	private IEnumerator wingamen(int who){
		gameend = true;
		if (who == 0) {
			GameObject.Find("winspy1").SetActive(false);
			GameObject.Find("winspy2").SetActive(false);
		}
		if(who==1) {
			GameObject.Find("winterrorist").SetActive(false);
			GameObject.Find("winspy2").SetActive(false);
		}
		if(who==2) {
			GameObject.Find("winspy1").SetActive(false);
			GameObject.Find("winterrorist").SetActive(false);
		}
		//Debug.Log (who);
		yield return new WaitForSeconds(1f);
		manager.bgm.Stop ();
		manager.wincanvas.GetComponent<Canvas>().enabled = true;
		//manager.win.Play ();
		audio.PlayOneShot(soundwin);
	}

}
