﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateMesh : UnitySingleton<CreateMesh> {

    private int boxcount=12;        //物品栏
    private int x=50;               
    private int y=-50;
    private int high = 50;          //content长度      
    private GameObject box;
    private GameObject item;
    private Transform parentTranform;
    private RectTransform contentParent;

    //按钮
    private Button btn_add; 

    void Start()
    {
        item = Resources.Load<GameObject>("Item");
        box = Resources.Load<GameObject>("Box");
        parentTranform = GameObject.Find("Sv_Panel/Viewport/Content").transform;
        contentParent = parentTranform.GetComponent<RectTransform>();

       
        
        CreatBox(boxcount);     //先创建背包格子
        CreateItem();           //再创建物品
    }

    void Update()
    {
        X_add();
        Z_del();
    }
    private void CreateItem()
    {   
        for (int i = 0; i < 4; i++)
        {
             GameObject item_go = Instantiate(item);
             item_go.transform.SetParent(parentTranform.GetChild(i));
    
             ItemInfor itemInfor=item_go.AddComponent<ItemInfor>();
             ItemInfor itemInfor_copy=CfgManager.Instance.JsonAdd_ItemInfor(i);
             itemInfor.Goodsname=itemInfor_copy.Goodsname;
             itemInfor.Id=itemInfor_copy.Id;
             itemInfor.Effect=itemInfor_copy.Effect;
             itemInfor.Coins=itemInfor_copy.Coins;                       //此处可以优化
            
             item_go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Img/"+itemInfor.Id);
             item_go.AddComponent<ItemDrag>();
             item_go.transform.localPosition = Vector3.zero;
             item_go.transform.localScale = Vector3.one;
        
        }

        //item_go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Img/2");
        //ItemInfor itemInfor=item_go.AddComponent<ItemInfor>();          //要继承Mono才能作为组件添加
        //itemInfor.Id=2;
        
        
    }

    private void CreatBox(int a)                                //可优化
    {
        int line = a / 3;
        int remain = a % 3;
        
        for (int i = 0; i < line; i++)
        {
            x = 50;
            for (int j = 0; j < 3; j++)
            {
                SetMesh();
                x += 480;
            }
            y -= 450;
            high += 450;
        }
        if (remain!=0)
        {
            x = 50;
            for (int j = 0; j < remain; j++)
            {
                SetMesh();
                x += 480;
            }
            high += 450;
        }
        if (remain==0)
        {
            y +=450;
        }
        contentParent.sizeDelta = new Vector2(1500, high); 
        
          
    }
    public GameObject SetMesh()
    {
        GameObject box_obj = Instantiate(box);
        box_obj.transform.SetParent(parentTranform);
        box_obj.transform.localPosition = new Vector3(x, y, 0);
        return box_obj;
    }
    private void X_add()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddBox();
        }
    }
    public GameObject AddBox()
    {
        switch (parentTranform.childCount%3)
            {
                case 0:
                    y -= 450;
                    x = 50;              
                    high += 450;
                    contentParent.sizeDelta = new Vector2(1500, high);
                    return SetMesh();
                case 1:
                    x = 530;
                    return SetMesh();
                case 2:
                    x = 1010;
                    return SetMesh();
                default:
                    Debug.LogError("无法生成box");
                    return null;
            }
    }
    public void test()
    {
        Debug.Log(11122233);
    }
    private void Z_del()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(parentTranform.GetChild(parentTranform.childCount - 1).gameObject);
            if (parentTranform.childCount % 3 == 1)
            {
                Debug.Log("Del");
                high -= 450;
                y += 450;
                contentParent.sizeDelta = new Vector2(1500, high);
            }
        }
    }

    private void GetPosition(int index)
    {

    }

	
}
