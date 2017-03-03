using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class terrorist : MonoBehaviour
{
	bool targetflag;

	public AudioClip soundkoma;
	public AudioClip soundcar;
	public AudioClip soundair;
	public AudioClip soundbicycle;
	public AudioClip soundsetti;
	public AudioClip soundkaijo;
	public AudioClip soundwin;
	AudioSource audio;

    public bool go = false;
    bool bagutubusi = false;
    bool bakudanphase = false;
    bool bommax = false;
    int k;
    int l;
    int m;
    public int me;
    public int idoume;
    string ternplayer;
    public static bool mati = false;
    bool masui = false;
	GameObject plefab_b;
	GameObject plefab_b2;
	GameObject plefab_target;
	GameObject b;
	GameObject b2;
	GameObject ta;
    int layerMask;
    Vector3 tpos;
    Vector3 camerapos;
    int bairitu;


    public void play()
	{
		targetflag=false;
		go = false;
		idoume = 0;
        manager.player_direction(0, manager.playerpos[0]);
        manager.player_direction(1, manager.playerpos[1]);
        manager.player_direction(2, manager.playerpos[2]);
        bairitu = 1;
        bagutubusi = true;
        ternplayer = this.gameObject.name;
        camerapos.x = this.transform.position.x;
        camerapos.y = this.transform.position.y;
        camerapos.z = -10;
        manager.maincamera.transform.position = camerapos;
        //Debug.Log(camerapos);
        //Debug.Log(camerapos.x);
        //Debug.Log(ternplayer + "　テロリストのターン");
        manager.message.text = "テロリストのターン";
        manager.logcontent.text = manager.logcontent.text + "\nテロリストのターン\n";
        //Debug.Log(spy1.mieru);
        //Debug.Log(spy2.mieru);
        if (manager.koudouseigen[0] > 0)
        {
            //Debug.Log("麻酔状態で動けない");
            manager.message.text = "麻酔状態で動けない";
            manager.logcontent.text = manager.logcontent.text + "\nテロリストは麻酔状態で動けない\n";
            manager.koudouseigen[0] -= 1;
            masui = true;
        }
        else
        {
            //manager.saikorobutton.SetActive(true);
            manager.saikorobutton.GetComponent<Button>().interactable = true;
            if (manager.itemterrorist.Count > 0) manager.itembutton.GetComponent<Button>().interactable = true;
            else manager.itembutton.GetComponent<Button>().interactable = false;
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
                //manager.playerpos[i] = 0;
                //Destroy(GameObject.Find("spy" + i.ToString()));
                //GameObject.Find("spy" + i.ToString()).GetComponent<Renderer>().sortingOrder = -5;
                //Debug.Log(GameObject.Find("spy" + i.ToString()) + "テロリストに殺される");
            //}
        }
    }

    void settihantei(int x)
    {
        for (int j = 1; j <= manager.spy1tpos.Length; ++j)
        {
            if (x == manager.spy1tpos[j - 1])
			{
				audio.PlayOneShot (soundkaijo);
                //Debug.Log("盗聴器を解除した");
                manager.message.text = "盗聴器を解除した";
                manager.logcontent.text = manager.logcontent.text + "\nテロリストは盗聴器を解除した\n";
                GameObject t = GameObject.Find("spy1toutyouki" + (x).ToString());
                t.GetComponent<Renderer>().sortingOrder = 5;
                manager.spy1tpos[j - 1] = 0;
                Destroy(t);
            }
            if (x == manager.spy2tpos[j - 1])
			{
				audio.PlayOneShot (soundkaijo);
                //Debug.Log("盗聴器を解除した");
                manager.message.text = "盗聴器を解除した";
                manager.logcontent.text = manager.logcontent.text + "\nテロリストは盗聴器を解除した\n";
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
		//初期化
		targetflag=false;
		go = false;
		bagutubusi = false;
		bakudanphase = false;
		bommax = false;
		mati = false;
		masui = false;


		camerapos = manager.maincamera.transform.position;
		audio = GetComponent<AudioSource>();
		plefab_b = (GameObject)Resources.Load("bom");
		plefab_b2 = (GameObject)Resources.Load("bom2");
		plefab_target = (GameObject)Resources.Load("target");
        //layerMask = LayerMask.GetMask(new string[] { LayerMask.LayerToName(8) }); //レイヤーマスクbom
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.terroristtern)
        {
            if (bagutubusi)
            {
                //Debug.Log("バグ潰し！");
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
                int defaultx = -260;
                foreach (int i in manager.itemterrorist)
                {
                    //Debug.Log(i);
                    string str = "item" + i.ToString();
                    GameObject plefab_a = (GameObject)Resources.Load(str);
                    GameObject a = (GameObject)Instantiate(plefab_a);
                    a.name = plefab_a.name;
                    a.transform.SetParent(manager.itemcanvas.transform);
                    RectTransform a_rect = a.GetComponent<RectTransform>();
                    a_rect.anchoredPosition = new Vector2(defaultx, 145);
                    a_rect.localScale = new Vector3(1,1,1);
                    defaultx += 160;
                }
                //manager.saikorobutton.SetActive(false);
                //manager.saikorobutton.GetComponent<Button>().interactable = false;
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
                manager.itemcanvas.SetActive(false);
                manager.modoru = false;
            }
            if (manager.item4) //自転車
            {
                manager.message.text = "テロリストは自転車を使った";
                manager.logcontent.text = manager.logcontent.text + "\nテロリストは自転車を使った\n";
                manager.item4 = false;
                //manager.saikorobutton.SetActive(false);
                manager.saikorobutton.GetComponent<Button>().interactable = false;
                //manager.itembutton.SetActive(false);
                manager.itembutton.GetComponent<Button>().interactable = false;
				manager.koudou.SetActive (true);
                manager.maebutton.SetActive(true);
                manager.usirobutton.SetActive(true);
            }
            if (manager.mae) //自転車
            {
				audio.PlayOneShot (soundbicycle);
                manager.mae = false;
                manager.maebutton.SetActive(false);
                manager.usirobutton.SetActive(false);
                me = 3;
                manager.susumu = true;
            }
            if (manager.usiro) //自転車
			{
				audio.PlayOneShot (soundbicycle);
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
				manager.koudou.SetActive (true);
				manager.spy1button.GetComponent<RectTransform>().localPosition=new Vector3(0,60,0);
				manager.spy1button.SetActive(true);
				manager.spy2button.GetComponent<RectTransform>().localPosition=new Vector3(0,-60,0);
                manager.spy2button.SetActive(true);
            }
            if (manager.player_spy1) //麻酔銃
            {
                //Debug.Log("スパイ１は次のターン動けない");
                manager.message.text = "スパイ１は次のターン動けない";
                manager.logcontent.text = manager.logcontent.text + "\nテロリストはスパイ１に麻酔銃を撃った\n";
                manager.player_spy1 = false;
                manager.spy1button.SetActive(false);
                manager.spy2button.SetActive(false);
                manager.koudouseigen[1] += 1;
                //mati = true;
                //manager.saikorobutton.SetActive(true);
                manager.saikorobutton.GetComponent<Button>().interactable = true;
            }
            else if (manager.player_spy2) //麻酔銃
            {
                //Debug.Log("スパイ２は次のターン動けない");
                manager.message.text = "スパイ２は次のターン動けない";
                manager.logcontent.text = manager.logcontent.text + "\nテロリストはスパイ２に麻酔銃を撃った\n";
                manager.player_spy2 = false;
                manager.spy1button.SetActive(false);
                manager.spy2button.SetActive(false);
                manager.koudouseigen[2] += 1;
                //mati = true;
                //manager.saikorobutton.SetActive(true);
                manager.saikorobutton.GetComponent<Button>().interactable = true;
            }
            if (manager.item2)
            {
                manager.message.text = "テロリストは車を使った";
                manager.logcontent.text = manager.logcontent.text + "\nテロリストは車を使った\n";
                bairitu = 2;
                manager.item2 = false;
            }
            if (manager.item3)
            {
                manager.message.text = "テロリストはヘリを使った";
                manager.logcontent.text = manager.logcontent.text + "\nテロリストはヘリを使った\n";
                bairitu = 3;
                manager.item3 = false;
            }
            if (manager.saikoro)
            {
                manager.maincamera.transform.position = camerapos;
                me = Random.Range(1, 7);
                //Debug.Log(me.ToString() + "の目が出た");
                manager.message.text = me.ToString() + "の目が出た";
                manager.logcontent.text = manager.logcontent.text + "\n" + me.ToString() + "の目が出た\n";
				me = me * bairitu;

                manager.saikoro = false;
                //manager.saikorobutton.SetActive(false);
                manager.saikorobutton.GetComponent<Button>().interactable = false;
                //manager.itembutton.SetActive(false);
				manager.itembutton.GetComponent<Button>().interactable = false;
				manager.koudou.SetActive (true);
				manager.susumubutton.GetComponent<RectTransform>().localPosition=new Vector3(0,0,0);
                manager.susumubutton.SetActive(true);
            }
            if (manager.susumu)
			{
				if (bairitu == 2) audio.PlayOneShot(soundcar);
				if (bairitu == 3) audio.PlayOneShot(soundair);

                //Debug.Log(me);
                manager.maincamera.transform.position = camerapos;
                manager.susumu = false;
                manager.susumubutton.SetActive(false);
                //Debug.Log(manager.saikorobutton.activeInHierarchy);
                //Debug.Log(this.gameObject.name);
                l = manager.playerpos[0];
                for(int i = 1; i <= Mathf.Abs(me); ++i)
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
                    //Debug.Log(k);
                    tpos = GameObject.Find(k.ToString()).transform.position;
                    tpos.y += 0.5f; // コマの位置調整
                    camerapos.x = tpos.x;
                    camerapos.y = tpos.y;
                    camerapos.z = -10;
                    StartCoroutine(idou(tpos, camerapos, i));
                }
                StartCoroutine(matu(Mathf.Abs(me)));
                
            }

            if (go)
            {
                go = false;
                manager.player_direction(0, manager.playerpos[0]);
                manager.player_direction(1, manager.playerpos[1]);
                manager.player_direction(2, manager.playerpos[2]);
                k = l + me;
                if (k > manager.total) k -= manager.total; //一周した場合
                if (k < 1) k += manager.total; //一周した場合
                manager.playerpos[0] = k;
                //Debug.Log("テロリストの現在位置" + manager.playerpos[0]);
                //tpos = GameObject.Find(k.ToString()).transform.position;
                //tpos.y += 0.4f; // コマの位置調整
                //this.gameObject.transform.position = tpos;
                //camerapos.x = this.transform.position.x;
                //camerapos.y = this.transform.position.y;
                //camerapos.z = -10;
                //manager.maincamera.transform.position = camerapos;
                //manager.maincamera.transform.Translate(this.transform.position.x - camerapos.x, this.transform.position.y - camerapos.y, 0);
                hantei();
                settihantei(manager.playerpos[0]);

                if (GameObject.Find(manager.playerpos[0].ToString()).transform.tag == "itemmasu")
                {
                    int item = Random.Range(2, 6);
                    manager.itemterrorist.Add(item);
                    if (item == 1) { manager.message.text = "プロテクターを手に入れた"; manager.logcontent.text = manager.logcontent.text + "\nテロリストはプロテクターを手に入れた\n"; }
                    else if (item == 2) { manager.message.text = "車を手に入れた"; manager.logcontent.text = manager.logcontent.text + "\nテロリストは車を手に入れた\n"; }
                    else if (item == 3) { manager.message.text = "ヘリを手に入れた"; manager.logcontent.text = manager.logcontent.text + "\nテロリストはヘリを手に入れた\n"; }
                    else if (item == 4) { manager.message.text = "自転車を手に入れた"; manager.logcontent.text = manager.logcontent.text + "\nテロリストは自転車を手に入れた\n"; }
                    else if (item == 5) { manager.message.text = "麻酔銃を手に入れた"; manager.logcontent.text = manager.logcontent.text + "\nテロリストは麻酔銃を手に入れた\n"; }
                    //Debug.Log(item);
                }

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
                    //Debug.Log("爆弾置けない");
                    manager.message.text = "爆弾置けない";
                    mati = true;
                }
                else
				{
					manager.koudou.SetActive(true);
					if (manager.bom2pos [0] == 0) {
						manager.bombutton.GetComponent<RectTransform>().localPosition=new Vector3(0,90,0);
						manager.bombutton.SetActive(true);
						manager.bom2button.GetComponent<RectTransform>().localPosition=new Vector3(0,0,0);
						manager.bom2button.SetActive (true);
						manager.nonebutton.GetComponent<RectTransform>().localPosition=new Vector3(0,-90,0);
						manager.nonebutton.SetActive(true);
					} else {
						manager.bombutton.GetComponent<RectTransform>().localPosition=new Vector3(0,60,0);
						manager.bombutton.SetActive(true);
						manager.nonebutton.GetComponent<RectTransform>().localPosition=new Vector3(0,-60,0);
						manager.nonebutton.SetActive(true);
					}
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

			if ((manager.bom || manager.bom2) && targetflag==false) {
				int j = 1;
				foreach (int i in manager.bompos)
				{
					j = j * i;
				}
				if (j != 0) {
					bommax = true;
					//Debug.Log("いらない爆弾を撤去してから新しい爆弾を設置してください");
					manager.message.text = "いらない爆弾を撤去してから新しい爆弾を設置してください";
				} else {
					targetflag = true;
					for (int i = 1; i <= Mathf.Abs (me); ++i) { //絶対値meにする
						if (me < 0) {
							m = l + me + i; //後ろに戻るとき
						} else {
							m = l + i - 1;
						}
						if (m > manager.total) m -= manager.total; //一周した場合
						if (m < 1) m += manager.total; //一周した場合
						if (m != manager.playerpos [0] && m != manager.playerpos [1] && m != manager.playerpos [2] && m != manager.bom2pos [0] && m != manager.bompos [0] && m != manager.bompos [1] && m != manager.bompos [2]) {
							Vector3 tapos = GameObject.Find(m.ToString()).transform.position;
							ta = (GameObject)Instantiate(plefab_target, tapos, Quaternion.identity);
							ta.name = "target" + m.ToString();
							ta.transform.parent = GameObject.Find("targets").transform;
						}
					}
				}
			}

            if (mati)
            {
				foreach (Transform n in GameObject.Find("targets").transform )
				{
					Destroy(n.gameObject);
				}
                manager.message.text = "画面をタッチして次のプレイヤーに変わってください。";
            }

            if (Input.GetMouseButtonDown(0))
            {

                if (manager.bom)
                {
                    // lからkの一個前のマスに爆弾を置ける
                    
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
                            //Debug.Log(obj.name);
                            if (obj.name.Substring(0, 3) == "bom")
                            {
                                int b = int.Parse(obj.name.Substring(3));
                                for (int i = 0; i < manager.bompos.Length; ++i)
                                {
                                    if (manager.bompos[i] == b)
                                    {
										audio.PlayOneShot (soundkaijo);
                                        //Debug.Log("爆弾を撤去しました");
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
					if (aCollider2d && bommax==false)
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
										audio.PlayOneShot (soundsetti);
                                        manager.message.text = "爆弾を仕掛けた";
                                        manager.logcontent.text = manager.logcontent.text + "\nテロリストは爆弾を仕掛けた\n";
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
										audio.PlayOneShot (soundsetti);
                                        manager.message.text = "爆弾を仕掛けた";
                                        manager.logcontent.text = manager.logcontent.text + "\nテロリストは爆弾を仕掛けた\n";
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
										audio.PlayOneShot (soundsetti);
                                        manager.message.text = "爆弾を仕掛けた";
                                        manager.logcontent.text = manager.logcontent.text + "\nテロリストは爆弾を仕掛けた\n";
                                    }
                                    else
                                    {
                                        //Debug.Log("爆弾上限");
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
										audio.PlayOneShot (soundsetti);
                                        manager.message.text = "強力な爆弾を仕掛けた";
                                        manager.logcontent.text = manager.logcontent.text + "\nテロリストは強力な爆弾を仕掛けた\n";
                                    }
                                    else
                                    {
                                        //Debug.Log("爆弾上限");
                                        manager.message.text = "爆弾上限";
                                    }
                                }
                            }
                        }
                    }
                }

                else if (mati && this.transform.position == tpos || masui)
                {
                    GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = false;
                    mati = false;
                    masui = false;
                    //Debug.Log("ターンエンド");
                    manager.message.text = "ターンエンド";
                    manager.playflag = false;
                    manager.terroristtern = false;
                    //Debug.Log("スパイ１　" + manager.playerpos[1].ToString() + "　　スパイ２　" + manager.playerpos[2].ToString());
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
                        //Debug.Log("テロリストの勝利");
                        manager.message.text = "テロリストの勝利";
                        manager.logcontent.text = manager.logcontent.text + "\nテロリストの勝利\n";
						StartCoroutine(win (0));
                    }
                }
            }

        }
    }

    private IEnumerator idou(Vector3 player, Vector3 camera, int time)
    {
        //Debug.Log(time);
        yield return new WaitForSeconds(time * 0.5f);
        //this.gameObject.transform.position = player;
        //manager.maincamera.transform.position = camera;
        //this.gameObject.GetComponent<Rigidbody2D>().MovePosition(player);
        //manager.maincamera.GetComponent<Rigidbody2D>().MovePosition(camera);
        float step = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(transform.position.x - player.x), 2) + Mathf.Pow(Mathf.Abs(transform.position.x - player.x), 2)) / 10;
        //Debug.Log(step);
        step = 0.15f;
        while(transform.position!=player)
        {
            yield return new WaitForSeconds(0.01f);
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, player, step);
            manager.maincamera.transform.position = Vector3.MoveTowards(manager.maincamera.transform.position, camera, step);
        }
        //Debug.Log(time);
		idoume = time;
		audio.PlayOneShot (soundkoma);
        //manager.player_direction(manager.playerpos[0] + time);
    }

    private IEnumerator matu(int me)
    {
        while (me != idoume)
        {
            yield return new WaitForSeconds(0.01f);
        }
        go = true;
    }

    private IEnumerator mukouka()
    {
        yield return new WaitForSeconds(0.1f);
        Swipe.prevX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        Swipe.prevY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
    }


	private IEnumerator win(int who){
		//Debug.Log (who);
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
		yield return new WaitForSeconds(1f);
		manager.bgm.Stop ();
		manager.wincanvas.GetComponent<Canvas>().enabled = true;
		//manager.win.Play ();
		audio.PlayOneShot(soundwin);
	}

}
