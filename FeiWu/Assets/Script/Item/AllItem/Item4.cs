using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item4 : ItemInfor
{

    private Button btn_this;

    private Transform change_Pane;
    private Button button;

    private Image Infor_img;
    private Text Infor_effect;
    private Text Infor_coin;
    private Text count_txt;

    private Button btn_add;
    private Button btn_del;
    private InputField input_addordel;
    void Start()
    {
        item_Type=Item_type.Item_4;


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
       // btn_add.onClick.AddListener(AddCount);
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
