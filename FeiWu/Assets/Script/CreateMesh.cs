using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateMesh : MonoBehaviour {

    private int goods=10;       //物品栏
    private int x=50;
    private int y=-50;
    private int high = 50;      //content长度      
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

        btn_add = transform.Find("Btn_add").GetComponent<Button>();
        
        AddMesh(goods);
        CreateItem();
    }

    void Update()
    {
        X_add();
        Z_del();
    }
    private void CreateItem()
    {   
        GameObject item_go = Instantiate(item);
        item_go.transform.SetParent(parentTranform.GetChild(0));
       // item_go.GetComponent<Image>().sprite = Resources.Load<Image>("Img/2");
        item_go.transform.localPosition = Vector3.zero;
        item_go.transform.localScale = Vector3.one;
        
    }

    private void AddMesh(int a)
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
        contentParent.sizeDelta = new Vector2(1500, high);           //contenet变化
    }
    private void SetMesh()
    {
        GameObject box_obj = Instantiate(box);
        box_obj.transform.SetParent(parentTranform);
        box_obj.transform.localPosition = new Vector3(x, y, 0);
    }
    private void X_add()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            switch (parentTranform.childCount%3)
            {
                case 0:
                    y -= 450;
                    high += 450;
                    x = 50;
                    SetMesh();
                    contentParent.sizeDelta = new Vector2(1500, high);
                    break;
                case 1:
                    x = 530;
                    SetMesh();
                    break;
                case 2:
                    x = 1010;
                    SetMesh();
                    break;
                default:
                    break;
            }
        }
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
