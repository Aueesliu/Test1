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

	private GameObject item_obj;
	private List<GameObject> listItemInfor =new List<GameObject>();
	private Dictionary<int,GameObject> dicItemInfro=new Dictionary<int, GameObject>();			// 背包存储物品的类型 <ItemInfor的id，Item物品>
	private Dictionary<int,GameObject> dicItemInfro_now=new Dictionary<int, GameObject>();		// 当前背包存储物品的类型 <ItemInfor的id，Item物品>
	//	private Dictionary<int ,int > dicItemId=new Dictionary<int, int>();							// <一般顺序，物品的ID> 
	private List<int >  listId=new List<int>();													//存储物品的ID
	private  List<int > listId_now=new List<int>();
	private Dictionary<Item_type,GameObject> dicItemInfor_this=new Dictionary<Item_type, GameObject>();		// 背包存储物品的类型 <ItemInfor的Item_type，Item物品>
    public int id_showNow=0;           //正在面板上显示的物品id           
	
	void Start () {
        btn_add = GameTool.GetTheChildComponent<Button>(transform.parent.gameObject, "Btn_add");
        btn_del = GameTool.GetTheChildComponent<Button>(transform.parent.gameObject, "Btn_del");
		btn_random=GameTool.GetTheChildComponent<Button>(transform.parent.gameObject,"Btn_random");
		btn_neat=GameTool.GetTheChildComponent<Button>(transform.parent.gameObject,"Btn_neat");
		btn_clean=GameTool.GetTheChildComponent<Button>(transform.parent.gameObject,"Btn_clean");

		item_obj = Resources.Load<GameObject>("Item");
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
	//删除数目
    private void DelCount()
    {
		Debug.Log("删除--");
        if (id_showNow == 0)
        {
            return;
        }
		ResetAllList();
		Text if_Txt = GameTool.GetTheChildComponent<Text>(input_addordel.gameObject, "IF_Txt");
		int format=ItemData.Instance.GetCount(id_showNow)-int.Parse(if_Txt.text);
		if (format<=0)
		{
			format=0;
			ItemData.Instance.SetCount(id_showNow,format);
			Destroy(dicItemInfro_now[id_showNow]);
			change_Pane.gameObject.SetActive(false);
			return;
		}
		ItemData.Instance.SetCount(id_showNow,format);
		ShowCount(id_showNow);
		if_Txt.text=null;
		change_Pane.gameObject.SetActive(false);
    }
    //增加数目
    private void AddCount()
    {
        if (id_showNow==0)
        {
            return;
        }
		ResetAllList();
		Debug.Log("111");
		Text if_Txt = GameTool.GetTheChildComponent<Text>(input_addordel.gameObject, "IF_Txt");
		int add=int.Parse(if_Txt.text);
		int format=ItemData.Instance.GetCount(id_showNow);
		ItemData.Instance.SetCount(id_showNow,add+format);
		ShowCount(id_showNow);
		if_Txt.text=0.ToString();
		change_Pane.gameObject.SetActive(false);
	    
    }
	//随机添加新物体
    private void AddItem_Random()
    {
		foreach (Transform trans in content)
		{	
			if (trans.childCount==0)
			{				
				GameObject item_go=Instantiate(item);
				item_go.AddComponent<ItemDrag>();
				Random a=new Random();
			    int rand=a.Next(0,4);
                
				ItemInfor itemInfor=CreateMesh._instance.WhoItemIfor(rand+1,item_go);//item_go.AddComponent<ItemInfor>();				//可优
             	//ItemInfor itemInfor=WhoItemIfor(rand,item_go);
				 
				ItemInfor itemInfor_copy=CfgManager.Instance.JsonAdd_ItemInfor(rand);
				
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

	private void ResetAllList()
	{
		dicItemInfro_now.Clear();
		dicItemInfor_this.Clear();
		dicItemInfro.Clear();
		listId.Clear();
		for (int i = 0; i < content.childCount; i++)									//整理格子，将多余的Item合并
		{
			Transform child_box=content.GetChild(i);									//遍历content下所有的盒子
			if (child_box.childCount!=0)
			{
				ItemInfor itemInfor= child_box.GetChild(0).GetComponent<ItemInfor>();
				if (!dicItemInfro_now.ContainsKey(itemInfor.Id))						//字典找不到Item的情况下，添加到字典
				{
					GameObject item_object =child_box.GetChild(0).gameObject;
					dicItemInfro_now.Add(itemInfor.Id,item_object);						//把Item添加到字典
					dicItemInfor_this.Add(itemInfor.item_Type,item_object);
					listId.Add(itemInfor.Id);
					//Debug.Log("添加到字典:"+itemInfor.Id);
					continue;
				}
			}
		}
		listId_now=listId;
		dicItemInfro=dicItemInfro_now;
	}


    private void CleanItem()
	{
        dicItemInfro_now.Clear();
		dicItemInfor_this.Clear();
		dicItemInfro.Clear();
		listId.Clear();
		for (int i = 0; i < content.childCount; i++)									//整理格子，将多余的Item合并
		{
			Transform child_box=content.GetChild(i);									//遍历content下所有的盒子
			if (child_box.childCount!=0)
			{
				ItemInfor itemInfor= child_box.GetChild(0).GetComponent<ItemInfor>();
				if (!dicItemInfro_now.ContainsKey(itemInfor.Id))						//字典找不到Item的情况下，添加到字典
				{
					GameObject item_object =child_box.GetChild(0).gameObject;
					dicItemInfro_now.Add(itemInfor.Id,item_object);						//把Item添加到字典
					dicItemInfor_this.Add(itemInfor.item_Type,item_object);
					listId.Add(itemInfor.Id);
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
		Debug.Log("dicItemInfor_this:"+dicItemInfor_this.Count);
		dicItemInfro=dicItemInfro_now;
		Debug.Log("dicItemInfor:"+dicItemInfro.Count);
		//NeatItem();
		NeatItem_2();
		// NeatItem_1();
		
	}
	private void NeatItem()	                             								//物品排序
	{
		int enum_count=Enum.GetNames(typeof(Item_type)).Length;							//获取物品枚举类型的数量
		int count_a=dicItemInfro.Count;
		//Debug.Log(count);
		int c=0;
		for (int i = 0; i < enum_count; i++)									
		{
			Debug.Log(c);
			for (int j = c; j < content.childCount; j++)								//遍历背包的格子
			{	
				if (content.GetChild(j).childCount!=0)
				{
					c++;
					Debug.Log("111113333");
					break;
				}
				GameObject item_go1;
				Debug.Log(i+":  "+dicItemInfor_this.TryGetValue((Item_type)i,out item_go1)+content.GetChild(j).childCount+"   "+j);
				if (content.GetChild(j).childCount==0)									//格子是否为空
				{
					GameObject item_go;
					if (dicItemInfor_this.TryGetValue((Item_type)i,out item_go))		//物品是否在字典里面
					{
						item_go.transform.SetParent(content.GetChild(j));
						item_go.transform.localPosition=Vector3.one;					//物品设置到格子下面
						c++;
						break;
					}
				}
			}
		}
	}
	private void NeatItem_2()
	{
		for (int i = 0; i < content.childCount; i++)
		{
			if (content.GetChild(i).childCount!=0)
			{
				Destroy(content.GetChild(i).GetChild(0).gameObject);
			}	
		}
		for (int i = 0; i < listId.Count; i++)
		{
			GameObject itemgood=Instantiate(item_obj);
			itemgood.transform.SetParent(content.GetChild(i));
			ItemInfor itemInfor = CreateMesh._instance.WhoItemIfor(listId[i], itemgood);

             ItemInfor itemInfor_copy=CfgManager.Instance.JsonAdd_ItemInfor(listId[i]-1);
             itemInfor.Goodsname=itemInfor_copy.Goodsname;
             itemInfor.Id=itemInfor_copy.Id;
             itemInfor.Effect=itemInfor_copy.Effect;
             itemInfor.Coins=itemInfor_copy.Coins;                       //此处可以优化
            
             itemgood.GetComponent<Image>().sprite = Resources.Load<Sprite>("Img/"+itemInfor.Id);
             itemgood.AddComponent<ItemDrag>();
			 ShouCount(itemgood);
             itemgood.transform.localPosition = Vector3.zero;
             itemgood.transform.localScale = Vector3.one;
		}
	}
	// private void NeatItem_1()
	// {
	// 	GameObject item_go;
	// 	for (int i = 0; i < 5; i++)
	// 	{
	// 		if (dicItemInfro.TryGetValue(i,out item_go))
	// 		{
	// 			for (int j = 0; j < content.childCount; j++)
	// 			{
	// 				if (content.GetChild(j).childCount==0)
	// 				{
	// 					item_go.transform.SetParent(content.GetChild(j));
	// 					item_go.transform.localPosition=Vector3.one;					
	// 					break;
	// 				}
	// 			}
	// 		}
	// 	}
	// }
	private void ShouCount( GameObject item)					//显示物品数量
	{
		Text item_txt= item.transform.GetChild(0).GetComponent<Text>();
		item_txt.text=ItemData.Instance.GetCount(item.GetComponent<ItemInfor>().Id).ToString();
	}
	private void ShowCount (int item_id)					
	{
		
		Debug.Log(dicItemInfro[item_id]);
		Text text= dicItemInfro_now[item_id].transform.GetChild(0).GetComponent<Text>();
		text.text=ItemData.Instance.GetCount(item_id).ToString();
	}

    public void ShowItemId(int item_id)         //当前面板正在显示的物品ID
    {
        id_showNow = item_id;
    }

}
