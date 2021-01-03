using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfor :MonoBehaviour {


	protected int id;
    protected string goodsname;
    protected string effect;
    protected int coins;
    protected int count;


   


    protected void Start()
    {

        
       // button.onClick.AddListener(ShowInfor);
       // btn_add.onClick.AddListener(AddCount);
    }


     protected virtual void AddCount()
    {
        //Text if_Txt = GameTool.GetTheChildComponent<Text>(input_addordel.gameObject,"IF_Txt");
        //int  add =int.Parse(if_Txt.text);
        //int  count_now=int.Parse(count_txt.text);
        //ItemData.Instance.SetCount(id,add+count_now);
        //this. count_txt.text=(ItemData.Instance.GetCount(id)).ToString();
        //change_Pane.gameObject.SetActive(false);
    }

     protected virtual void ShowInfor()                            //显示物品信息
    {
        Debug.Log(id);
        //change_Pane.gameObject.SetActive(true);
        //Infor_img.sprite=gameObject.GetComponent<Image>().sprite;
        //Infor_effect.text=effect;
        //Infor_coin.text=coins.ToString();
    }
    

     public int Coins
    {
        get
        {
            return coins;
        }

        set
        {
            coins = value;
        }
    }

    public string Goodsname
    {
        get
        {

            return goodsname;
        }

        set
        {
            goodsname = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Effect
    {
        get{ return effect;}
        set{ effect = value;}
    }

    public int Count
    {
        get
        {
            return count;
        }

        set
        {
            count = value;
        }
    }


}
