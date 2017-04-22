using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class koudoubutton : MonoBehaviour
{
	public AudioClip soundtouch;
	public AudioClip sounditem;
	public AudioClip soundsaikoro;
	AudioSource audio;

    void notuseitem()
    {
        manager.modoru = true;
        //manager.itembutton.SetActive(true);
        manager.itembutton.GetComponent<Button>().interactable = true;
        foreach (Transform child in manager.itemcanvas.transform) if (child.name != "modoru") Destroy(child.gameObject);
    }

    void removeitem(int n)
    {
        if (manager.terroristtern)
        {
            foreach (int i in manager.itemterrorist)
            {
                if (i == n)
                {
                    manager.itemterrorist.Remove(i);
                    break;
                }
            }
        }
        else if (manager.spy1tern)
        {
            foreach (int i in manager.itemspy1)
            {
                if (i == n)
                {
                    manager.itemspy1.Remove(i);
                    break;
                }
            }
        }
        else if (manager.spy2tern)
        {
            foreach (int i in manager.itemspy2)
            {
                if (i == n)
                {
                    manager.itemspy2.Remove(i);
                    break;
                }
            }
        }
    }

    public void susumu()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("進む");
		manager.susumu = true;
		manager.koudou.SetActive(false);
    }

    public void tansaku()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("探索する");
		manager.tansaku = true;
		manager.koudou.SetActive(false);
    }

    public void modoru()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("アイテムを使わないで前の画面に戻る");
		notuseitem();
		manager.koudou.SetActive(false);
    }

    public void bom2()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("トラップ爆弾を仕掛ける（１個まで）");
        manager.bom2 = true;
        manager.bombutton.SetActive(false);
        manager.bom2button.SetActive(false);
		manager.nonebutton.SetActive(false);
		manager.koudou.SetActive(false);
    }

    public void bom()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("通常爆弾を仕掛ける");
        manager.bom = true;
        manager.bombutton.SetActive(false);
        manager.bom2button.SetActive(false);
		manager.nonebutton.SetActive(false);
		manager.koudou.SetActive(false);
    }

    public void toutyou()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("盗聴器を仕掛ける");
		manager.toutyou = true;
		manager.koudou.SetActive(false);
    }

    public void none()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("何もしない");
		manager.none = true;
		manager.koudou.SetActive(false);
    }


    public void mae()
    {
        //Debug.Log("前に３マス");
		manager.mae = true;
		manager.koudou.SetActive(false);
    }


    public void usiro()
	{
        //Debug.Log("後ろに３マス");
		manager.usiro = true;
		manager.koudou.SetActive(false);
    }

    public void spy1()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("spy1押した");
		manager.player_spy1 = true;
		manager.koudou.SetActive(false);
    }

    public void spy2()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("spy2押した");
		manager.player_spy2 = true;
		manager.koudou.SetActive(false);
    }

	public void terrorist()
	{
		audio.PlayOneShot (soundtouch);
		//Debug.Log("terrorist押した");
		manager.player_terrorist = true;
		manager.koudou.SetActive(false);
	}

	public void itemsiyousuru()
	{
		//Debug.Log (manager.selecteditem);
		//Debug.Log("使用する押した");
		manager.modoru = true;
		if (manager.selecteditem == 1) {
			manager.item1 = true;
			//Debug.Log("プロテクター");
		}
		if (manager.selecteditem == 2) {
			manager.item2 = true;
			//Debug.Log("自動車");
		}
		if (manager.selecteditem == 3) {
			manager.item3 = true;
			//Debug.Log("ヘリ");
		}
		if (manager.selecteditem == 4) {
			manager.item4 = true;
			//Debug.Log("自転車");
		}
		if (manager.selecteditem == 5) {
			manager.item5 = true;
			//Debug.Log("麻酔銃");
		}
		removeitem(manager.selecteditem);
		foreach (Transform child in manager.itemcanvas.transform) if (child.name != "modoru") Destroy(child.gameObject);
		manager.itemwindow.SetActive (false);
	}

	public void itemcancel()
	{
		//Debug.Log("キャンセル押した");
		manager.itemwindow.SetActive (false);
		manager.itembutton.GetComponent<Button>().interactable = true;
		foreach (Transform child in manager.itemcanvas.transform) if (child.name != "modoru") Destroy(child.gameObject);
	}

    public void item1()
	{
		//Debug.Log("プロテクター");
		manager.itemtitle.text = "プロテクター";
		manager.itemsentence.text = "スパイ専用アイテム\n爆弾から１回身を守れる。\n効果は重複する。";
		manager.modoru = true;
		manager.selecteditem = 1;
		manager.itemcanvas.SetActive(false);
		manager.itemwindow.SetActive (true);
    }

    public void item2()
	{
		//Debug.Log("車");
		manager.itemtitle.text = "自動車";
		manager.itemsentence.text = "さいころの目が２倍になる。";
		manager.selecteditem = 2;
		manager.itemcanvas.SetActive(false);
		manager.itemwindow.SetActive (true);
    }

    public void item3()
	{
		//Debug.Log("ヘリ");
		manager.itemtitle.text = "ヘリコプター";
		manager.itemsentence.text = "さいころの目が３倍になる。";
		manager.selecteditem = 3;
		manager.itemcanvas.SetActive(false);
		manager.itemwindow.SetActive (true);
    }

    public void item4()
	{
		//Debug.Log("自転車(前か後ろに３マス進める)");
		manager.itemtitle.text = "自転車";
		manager.itemsentence.text = "前か後ろに３マス進める。";
		manager.selecteditem = 4;
		manager.itemcanvas.SetActive(false);
		manager.itemwindow.SetActive (true);
    }

    public void item5()
	{
		//Debug.Log("麻酔銃(任意のプレイヤーを一回休みにする)");
		manager.itemtitle.text = "麻酔銃";
		manager.itemsentence.text = "任意のプレイヤーを１回休みにする。\n効果は重複する。";
		manager.modoru = true;
		manager.selecteditem = 5;
		manager.itemcanvas.SetActive(false);
		manager.itemwindow.SetActive (true);
    }

    public void map()
	{
		audio.PlayOneShot (soundtouch);
        //Debug.Log("マップ");
        if (manager.itemcanvas.activeSelf) notuseitem();
        manager.mapbutton.GetComponent<Button>().interactable = false;
        manager.mapwindow.SetActive(true);
        manager.logbutton.GetComponent<Button>().interactable = true;
        manager.logcanvas.GetComponent<Canvas>().enabled = false;
        manager.settingbutton.GetComponent<Button>().interactable = true;
        manager.helpcanvas.GetComponent<Canvas>().enabled = false;
        //manager.logwindow.SetActive(false);
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
        //manager.modoru = true;
        //manager.itembutton.SetActive(false);
        //manager.item6 = true;
        //removeitem(6);
    }

    public void closemap()
	{
        //Debug.Log("マップ閉じる");
        manager.mapbutton.GetComponent<Button>().interactable = true;
        manager.mapwindow.SetActive(false);
    }

    public void closelog()
    {
        //Debug.Log("ログ閉じる");
        manager.logbutton.GetComponent<Button>().interactable = true;
        manager.logcanvas.GetComponent<Canvas>().enabled = false;
        //manager.logwindow.SetActive(false);
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
    }

    public void closehelp()
    {
        //Debug.Log("ヘルプ閉じる");
        manager.settingbutton.GetComponent<Button>().interactable = true;
        manager.helpcanvas.GetComponent<Canvas>().enabled = false;
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
    }

    public void log()
	{
		audio.PlayOneShot (soundtouch);
        manager.logwindow.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        if (manager.itemcanvas.activeSelf) notuseitem();
        manager.logbutton.GetComponent<Button>().interactable = false;
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = false;
        manager.mapbutton.GetComponent<Button>().interactable = true;
        manager.mapwindow.SetActive(false);
        manager.settingbutton.GetComponent<Button>().interactable = true;
        manager.helpcanvas.GetComponent<Canvas>().enabled = false;
        manager.logcanvas.GetComponent<Canvas>().enabled = true;
        //manager.logwindow.SetActive(true);
        //Debug.Log("ログ");
        //manager.item7 = true;
    }
    

    public void setting()
	{
		audio.PlayOneShot (soundtouch);
        if (manager.itemcanvas.activeSelf) notuseitem();
        manager.mapbutton.GetComponent<Button>().interactable = true;
        manager.mapwindow.SetActive(false);
        manager.logbutton.GetComponent<Button>().interactable = true;
        manager.logcanvas.GetComponent<Canvas>().enabled = false;
        manager.helpcanvas.GetComponent<Canvas>().enabled = true;
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
        manager.settingbutton.GetComponent<Button>().interactable = false;
        //Debug.Log("設定");
        //manager.item8 = true;
    }


	public void huru()
	{
		audio.PlayOneShot (soundsaikoro);
		if (manager.itemcanvas.activeSelf) notuseitem();
		manager.mapbutton.GetComponent<Button>().interactable = true;
		manager.mapwindow.SetActive(false);
        manager.logbutton.GetComponent<Button>().interactable = true;
        manager.logcanvas.GetComponent<Canvas>().enabled = false;
        manager.settingbutton.GetComponent<Button>().interactable = true;
        manager.helpcanvas.GetComponent<Canvas>().enabled = false;
        //manager.logwindow.SetActive(false);
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
		manager.saikorobutton.GetComponent<Button>().interactable = false;
		//Debug.Log("さいころをふる");
		manager.saikoro = true;
	}

	public void use()
	{
		audio.PlayOneShot (sounditem);
		manager.mapbutton.GetComponent<Button>().interactable = true;
		manager.mapwindow.SetActive(false);
		manager.logbutton.GetComponent<Button>().interactable = true;
		manager.logcanvas.GetComponent<Canvas>().enabled = false;
        manager.settingbutton.GetComponent<Button>().interactable = true;
        manager.helpcanvas.GetComponent<Canvas>().enabled = false;
        //manager.logwindow.SetActive(false);
        GameObject.Find("TouchManager").GetComponent<Swipe>().enabled = true;
		manager.itembutton.GetComponent<Button>().interactable = false;
		//Debug.Log("アイテムを使う");
		manager.item = true;
	}

	public void restart()
	{
		audio.PlayOneShot (soundtouch);
		StartCoroutine (reload());
	}

	public void end()
	{
		audio.PlayOneShot (soundtouch);
		Application.Quit ();
	}

    // Use this for initialization
    void Start()
	{
		audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

	private IEnumerator reload()
	{
		yield return new WaitForSeconds(3f);
		Application.LoadLevel ("play");
	}

}
