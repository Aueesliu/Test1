using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfor :MonoBehaviour {


	public int id;
    protected string goodsname;
    protected string effect;
    protected int coins;
    protected int count;

    public Item_type item_Type=Item_type.Null;


    protected void Start()
    {

    }

     protected virtual void AddCount()
    {
    }

     protected virtual void ShowInfor()                            //显示物品信息
    {
        Debug.Log(id);
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
