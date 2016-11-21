using UnityEngine;
using System.Collections;

public class koudoubutton : MonoBehaviour
{

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
        Debug.Log("進む");
        manager.susumu = true;
    }

    public void tansaku()
    {
        Debug.Log("探索する");
        manager.tansaku = true;
    }

    public void modoru()
    {
        Debug.Log("アイテムを使わないで前の画面に戻る");
        manager.modoru = true;
        manager.itembutton.SetActive(true);
    }

    public void bom2()
    {
        Debug.Log("トラップ爆弾を仕掛ける（１個まで）");
        manager.bom2 = true;
        manager.bombutton.SetActive(false);
        manager.bom2button.SetActive(false);
        manager.nonebutton.SetActive(false);
    }

    public void bom()
    {
        Debug.Log("通常爆弾を仕掛ける");
        manager.bom = true;
        manager.bombutton.SetActive(false);
        manager.bom2button.SetActive(false);
        manager.nonebutton.SetActive(false);
    }

    public void toutyou()
    {
        Debug.Log("盗聴器を仕掛ける（未実装）");
        manager.toutyou = true;
    }

    public void none()
    {
        Debug.Log("何もしない");
        manager.none = true;
    }

    public void spy1()
    {
        Debug.Log("spy1押した");
        manager.player_spy1 = true;
    }

    public void spy2()
    {
        Debug.Log("spy2押した");
        manager.player_spy2 = true;
    }

    public void terrorist()
    {
        Debug.Log("terrorist押した");
        manager.player_terrorist = true;
    }

    public void item1()
    {
        Debug.Log("プロテクター");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        manager.item1 = true;
        removeitem(1);
    }

    public void item2()
    {
        Debug.Log("車");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        manager.item2 = true;
        removeitem(2);
    }

    public void item3()
    {
        Debug.Log("ヘリ");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        manager.item3 = true;
        removeitem(3);
    }

    public void item4()
    {
        Debug.Log("自転車(前か後ろに３マス進める)");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        manager.item4 = true;
        removeitem(4);
    }

    public void item5()
    {
        Debug.Log("麻酔銃(任意のプレイヤーを一回休みにする)");
        manager.modoru = true;
        manager.itembutton.SetActive(false);
        manager.item5 = true;
        removeitem(5);
    }

    public void item6()
    {
        Debug.Log("麻酔銃");
        //manager.modoru = true;
        //manager.itembutton.SetActive(false);
        //manager.item6 = true;
        //removeitem(6);
    }

    public void item7()
    {
        Debug.Log("アイテム7");
        //manager.item7 = true;
    }

    public void item8()
    {
        Debug.Log("アイテム8");
        //manager.item8 = true;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
