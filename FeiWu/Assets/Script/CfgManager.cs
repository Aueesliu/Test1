using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class CfgManager : UnitySingleton<CfgManager> {

	List<ItemInfor> list_ItemInfor =new List<ItemInfor>();
	TextAsset goodText;
	TextAsset goodText2;
	
	void Start () {
		goodText = Resources.Load<TextAsset>("Txt/Goods");
		goodText2 =Resources.Load<TextAsset>("Txt/Goods2");

		JsonCreate_ItemInfor();
	}
	

	public  void JsonCreate_ItemInfor()		//将Json导入内容导入链表
	{
		JsonData goodjson = JsonMapper.ToObject(goodText2.text);
		JsonData goodsdata =goodjson["Goods"];
		foreach (JsonData Goods in goodsdata)
		{
			ItemInfor itemInfor=new ItemInfor();
			itemInfor.Id =  (int)Goods["GoodsId"]; 
			itemInfor.Goodsname = Goods["GoodsName"].ToString();
			itemInfor.Effect =Goods["GoodsEffect"].ToString();
			itemInfor.Coins = (int)Goods["GoodCoins"];
			list_ItemInfor.Add(itemInfor);
			//Debug.Log("id"+ id +"  名字"+name+"  效果"+effect+"  金币"+coins);
		}
	}

	public ItemInfor JsonAdd_ItemInfor(int index)
	{
		ItemInfor itemInfor=new ItemInfor();
		for (int i = 0; i <list_ItemInfor.Count ; i++)
		{
			if (index+1==list_ItemInfor[i].Id)
			{
				return list_ItemInfor[i];
			}
		}
		Debug.LogError("查找不到ItemInfor");
		return null;
	}
	public void Test()
	{
		Debug.Log(123);
	}

}
