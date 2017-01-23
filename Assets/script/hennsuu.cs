using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[SerializeField]
public class hennsuu : MonoBehaviour {
    public Text test;
    public static Text message;
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

    // Use this for initialization
    

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
