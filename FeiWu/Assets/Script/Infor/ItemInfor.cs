using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfor :MonoBehaviour {


	private int id;
	private string  goodsname;
	private string effect;
	private int  coins;

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

    private Button button;
    private Image Infor_img;
    private Text Infor_effect;
    private Text Infor_coin;


     void Start()
    {
        button=this.GetComponent<Button>();   
        Infor_img=GameTool.FindTheChild(transform.root.gameObject,"Infor_Img").GetComponent<Image>();
        Infor_effect=GameTool.FindTheChild(transform.root.gameObject,"Infor_Effect").GetComponent<Text>();
        Infor_coin=GameTool.FindTheChild(transform.root.gameObject,"Infor_Coin").GetComponent<Text>();

        button.onClick.AddListener(ShowInfor);
    }
    
    
    void ShowInfor()
    {
        Infor_img.sprite=gameObject.GetComponent<Image>().sprite;
        Infor_effect.text=effect;
        Infor_coin.text=coins.ToString();
    }
    

}
