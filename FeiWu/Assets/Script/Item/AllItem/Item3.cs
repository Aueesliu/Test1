using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item3 : ItemInfor
{

    private Button btn_this;

    protected Transform change_Pane;
    protected Button button;

    protected Image Infor_img;
    protected Text Infor_effect;
    protected Text Infor_coin;
    protected Text count_txt;

    protected Button btn_add;
    protected Button btn_del;
    protected InputField input_addordel;
    void Start()
    {
        btn_this = this.GetComponent<Button>();

        Infor_img = GameTool.FindTheChild(transform.root.gameObject, "Infor_Img").GetComponent<Image>();
        Infor_effect = GameTool.FindTheChild(transform.root.gameObject, "Infor_Effect").GetComponent<Text>();
        Infor_coin = GameTool.FindTheChild(transform.root.gameObject, "Infor_Coin").GetComponent<Text>();
        count_txt = GameTool.FindTheChild(this.gameObject, "Item_txt").GetComponent<Text>();
        btn_add = GameTool.FindTheChild(transform.root.gameObject, "Btn_add").GetComponent<Button>();
        btn_del = GameTool.FindTheChild(transform.root.gameObject, "Btn_del").GetComponent<Button>();

        input_addordel = GameTool.FindTheChild(transform.root.gameObject, "IF_count").GetComponent<InputField>();


        change_Pane = GameTool.FindTheChild(transform.root.gameObject, "Change_Pane");

        btn_this.onClick.AddListener(ShowInfor);
       //btn_add.onClick.AddListener(AddCount);
    }

    protected override void AddCount()
    {
        Text if_Txt = GameTool.GetTheChildComponent<Text>(input_addordel.gameObject, "IF_Txt");
        int add = int.Parse(if_Txt.text);
        int count_now = int.Parse(count_txt.text);
        ItemData.Instance.SetCount(id, add + count_now);
        this.count_txt.text = (ItemData.Instance.GetCount(id)).ToString();
        change_Pane.gameObject.SetActive(false);
    }

    protected override void ShowInfor()                            //显示物品信息
    {
        base.ShowInfor();
        change_Pane.gameObject.SetActive(true);
        Infor_img.sprite = gameObject.GetComponent<Image>().sprite;
        Infor_effect.text = effect;
        Infor_coin.text = coins.ToString();
        ItemManager.Instance.ShowItemId(id);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
