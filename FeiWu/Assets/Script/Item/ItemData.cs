using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : UnitySingleton<ItemData> {

	public int RedDrug=1;
	public int BuleDrug=1;
	public int EnergyDrug=1;
	public int MagicDrug=1;

	public int GetCount(int item_ID)
	{
        int a;
		switch (item_ID)
		{
			case 1:
				a= RedDrug;
                break;
			case 2:
				a= BuleDrug;
                break;
			case 3:
                a= EnergyDrug;
                break;
			case 4:
				a= MagicDrug;
                break;
			default:
                Debug.Log("获取错误");
				return 0;
		}
        return a;
	}
	
	public void SetCount(int item_ID, int count)
	{
		switch (item_ID)
		{
			case 1:
				 RedDrug=count;
				 break;
			case 2:
				 BuleDrug=count;
				 break;
			case 3:
				 EnergyDrug=count;
				 break;
			case 4:
				 MagicDrug=count;
				 break;
			default:
				break;
		}
	}
		
	}

