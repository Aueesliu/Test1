using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class LitJsonTest : MonoBehaviour {

    //private List<Goods> goodsdata = new List<Goods>();
    void Start () {
       // MyCallLitJson();

    }

    private void MyCallLitJson()
    {
        string strJson = "{'heros':[{'name':'SuperMan','power':90}," +  "{'name':'BatMan','power':87}]}";
    
        JsonData herosJD = JsonMapper.ToObject(strJson);
        JsonData heros = herosJD["heros"];
        foreach (JsonData hero in heros)
        {
            string name = hero["name"].ToString();
            int power = (int)hero["power"];
            Debug.Log("name:" + name + "\npower:" + power);
            
        }
    }
    private void ItemJson()
    {
        TextAsset itemText = Resources.Load<TextAsset>("Txt/Goods");
        JsonData itemDate = JsonMapper.ToObject(itemText.text);
        for (int i = 0; i < itemDate.Count; i++)
        {
            //nt id = ()
        }

    }

}
