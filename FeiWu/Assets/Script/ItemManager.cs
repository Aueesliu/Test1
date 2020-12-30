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
	
	void Start () {
		btn_random=GameTool.GetTheChildComponent<Button>(transform.parent.gameObject,"Btn_random");
		content=GameTool.FindTheChild(transform.parent.gameObject,"Content");
		item=Resources.Load<GameObject>("Item");
		btn_random.onClick.AddListener(AddItem_Random);
	}

    private void AddItem_Random()
    {
		foreach (Transform trans in content)
		{	
			if (trans.childCount==0)
			{				
				GameObject item_go=Instantiate(item);
				item_go.AddComponent<ItemDrag>();
				item_go.AddComponent<ItemInfor>();
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
		
		

		//item_go.transform.SetParent(box.transform);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
