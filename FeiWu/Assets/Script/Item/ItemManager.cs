using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ItemManager : UnitySingleton<ItemManager> {

	private Button btn_random;
	private GameObject item;
	private Transform content;
	private ItemInfor itemInfor;

    private Button btn_add;             //增加减少
    private Button btn_del;
    private InputField input_addordel;
    protected Transform change_Pane;        //面板

	private Button btn_neat;
	private Button btn_clean;
	private List<GameObject> listItemInfor =new List<GameObject>();
	private Dictionary<int,GameObject> dicItemInfro=new Dictionary<int, GameObject>();			// 背包存储物品的类型 <ItemInfor的id，Item物品>
	private Dictionary<int,GameObject> dicItemInfro_now=new Dictionary<int, GameObject>();		// 当前背包存储物品的类型 <ItemInfor的id，Item物品>
	
    public int id_showNow=0;           //正在面板上显示的物品id           
	
	void Start () {
        btn_add = GameTool.GetTheChildComponent<Button>(transform.parent.gameObject, "Btn_add");
        btn_del = GameTool.GetTheChildComponent<Button>(transform.parent.gameObject, "Btn_del");
		btn_random=GameTool.GetTheChildComponent<Button>(transform.parent.gameObject,"Btn_random");
		btn_neat=GameTool.GetTheChildComponent<Button>(transform.parent.gameObject,"Btn_neat");
		btn_clean=GameTool.GetTheChildComponent<Button>(transform.parent.gameObject,"Btn_clean");

        input_addordel = GameTool.FindTheChild(transform.parent.gameObject, "IF_count").GetComponent<InputField>();
        change_Pane = GameTool.FindTheChild(transform.root.gameObject, "Change_Pane");
		content=GameTool.FindTheChild(transform.parent.gameObject,"Content");
		item=Resources.Load<GameObject>("Item");


        btn_add.onClick.AddListener(AddCount);
        btn_del.onClick.AddListener(DelCount);

		btn_random.onClick.AddListener(AddItem_Random);
		btn_neat .onClick.AddListener(NeatItem);
		btn_clean.onClick.AddListener(CleanItem);

	}

    private void DelCount()
    {
        if (id_showNow == 0)
        {
            return;
        }
        if (dicItemInfro_now.ContainsKey(id_showNow))
        {
            Text if_Txt = GameTool.GetTheChildComponent<Text>(input_addordel.gameObject, "IF_Txt");
            int del = int.Parse(if_Txt.text);
            Debug.Log("输入框为" + del);
            Text count_txt = dicItemInfro[id_showNow].transform.GetChild(0).GetComponent<Text>();
            int count_now = int.Parse(count_txt.text);
            int allcount = count_now - del;
            if (allcount <= 0)
            {
                allcount = 0;
            }
            ItemData.Instance.SetCount(id_showNow, allcount);
            count_txt.text = (ItemData.Instance.GetCount(id_showNow)).ToString();
            change_Pane.gameObject.SetActive(false);
        }
    }
    
    private void AddCount()
    {
        if (id_showNow==0)
        {
            return;
        }
        if (dicItemInfro_now.ContainsKey(id_showNow))
        {
           Text if_Txt = GameTool.GetTheChildComponent<Text>(input_addordel.gameObject, "IF_Txt");
           int add = int.Parse(if_Txt.text);
           Debug.Log("输入框为" + add);
           Text count_txt= dicItemInfro[id_showNow].transform.GetChild(0).GetComponent<Text>();
           int count_now = int.Parse(count_txt.text);

           ItemData.Instance.SetCount(id_showNow, add + count_now);
           count_txt.text = (ItemData.Instance.GetCount(id_showNow)).ToString();
           change_Pane.gameObject.SetActive(false);
        }
        Debug.Log(dicItemInfro_now.ContainsKey(id_showNow));
       
       
    }

    private void AddItem_Random()
    {
		foreach (Transform trans in content)
		{	
			if (trans.childCount==0)
			{				
				GameObject item_go=Instantiate(item);
				item_go.AddComponent<ItemDrag>();
				Random a=new Random();
			    

                
				ItemInfor itemInfor=item_go.AddComponent<ItemInfor>();				//可优
             	
				 
				ItemInfor itemInfor_copy=CfgManager.Instance.JsonAdd_ItemInfor(a.Next(4));
				
             	itemInfor.Goodsname=itemInfor_copy.Goodsname;
             	itemInfor.Id=itemInfor_copy.Id;
             	itemInfor.Effect=itemInfor_copy.Effect;
             	itemInfor.Coins=itemInfor_copy.Coins; 


				item_go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Img/"+itemInfor.Id);
				//item_go.GetComponent<ItemInfor>()=CfgManager.instance.JsonAdd_ItemInfor(a.Next(5));
				item_go.transform.SetParent(trans);
				item_go.transform.localPosition=Vector3.zero;
				item_go.transform.localScale=Vector3.one;
				return;
			}
		}
    }
    //public GameObject WhoItemIfor(int Iteminfor_id, GameObject itemobj)          //添加脚本
    //{

    //    switch (Iteminfor_id)
    //    {
    //        case 1:
    //            itemobj.AddComponent<Item1>();
            
    //            break;
    //        case 2:
    //            itemobj.AddComponent<Item2>();
                
    //            break;
    //        case 3:
    //            itemobj.AddComponent<Item3>();
               
    //            break;
    //        case 4:
    //            itemobj.AddComponent<Item4>();
                
    //            break;
    //        default:
                
    //            break;
    //    }
    //    return itemobj;
    //}

    private void CleanItem()
	{
        dicItemInfro_now.Clear();
		for (int i = 0; i < content.childCount; i++)								//整理格子，将多余的Item合并
		{
			Transform child_box=content.GetChild(i);								//遍历content下所有的盒子
			if (child_box.childCount!=0)
			{
				ItemInfor itemInfor= child_box.GetChild(0).GetComponent<ItemInfor>();
				if (!dicItemInfro_now.ContainsKey(itemInfor.Id))						//字典找不到Item的情况下，添加到字典
				{
					GameObject item_object =child_box.GetChild(0).gameObject;
					dicItemInfro_now.Add(itemInfor.Id,item_object);						//把Item添加到字典
					//Debug.Log("添加到字典:"+itemInfor.Id);
					continue;
				}
				if (dicItemInfro_now.ContainsKey(itemInfor.Id))							//字典里面有当前物体
				{
					Text item_txt = dicItemInfro_now[itemInfor.Id].transform.GetChild(0).GetComponent<Text>();
					Text item_other =child_box.GetChild(0).GetChild(0).GetComponent<Text>();
					int item_other_count=int.Parse(item_other.text);

					int add_one=int.Parse(item_txt.text.ToString());
					ItemData.Instance.SetCount(itemInfor.Id,add_one+item_other_count);
					dicItemInfro_now[itemInfor.Id].transform.GetChild(0).GetComponent<Text>().text=(ItemData.Instance.GetCount(itemInfor.Id)).ToString();
					Destroy(child_box.GetChild(0).gameObject);
				}
			}
		}
		dicItemInfro=dicItemInfro_now;
		NeatItem();
		
		
	}
	private void NeatItem()	                             //				Item归前
	{
		for (int i = 0; i < dicItemInfro.Count; i++)
		{
			dicItemInfro[i+1].transform.SetParent(content);
			for (int j = 0; j < content.childCount; j++)
			{
				if (content.GetChild(j).childCount==0)
				{
					dicItemInfro[i+1].transform.SetParent(content.GetChild(i));
					dicItemInfro[i+1].transform.localPosition=Vector3.one;
				}
			}
		}
		
	}
    public void ShowItemId(int item_id)         //当前面板正在显示的物品ID
    {
        id_showNow = item_id;
    }

}
