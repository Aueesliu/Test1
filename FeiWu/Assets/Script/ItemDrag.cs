using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Item记得添加两个组件Cavans Group，Layout Element
    //1.Cavans Group:主要是Blocks Raycast属性,当勾选时无法进行当前鼠标对武器image下方覆盖的其他image进行检测,在拖动时让此bool为false,关掉block开启检测,检测其他image的name
    //2.Layout Element:勾选Ignore layout后当武器image会按照调整好的尺寸随着鼠标移动时,不会更改变换图标大小

    //定义临时组件,存储当前组件位置,用于后续位置的交换
    private Transform originalTransform;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalTransform = transform.parent;
        this.transform.position = eventData.position;
        transform.SetParent(transform.parent.parent);   //把Item拉到外面，不在格子里
        //关闭block,开启检测
        GetComponent<CanvasGroup>().blocksRaycasts = false;   //关闭blockRaycasts 可以检测到下面IteXm的名字
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        //当前鼠标射线检测到的游戏物体的name
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //检测到的物体name为item,即下方有其他item物品,进行置换位置
        if (eventData.pointerCurrentRaycast.gameObject.tag.Equals("Item"))
        {
            //当前的物体设置检测到的物体的父类,当前物体设置position为检测到的物体的位置
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.position;
            //检测到的物体设置父类原来物体的父类,检测到的物体设置position为原来物体的位置,
            eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originalTransform);
            eventData.pointerCurrentRaycast.gameObject.transform.position = originalTransform.position;
        }
        //检测到为空,设置物体到此位置
        else if (eventData.pointerCurrentRaycast.gameObject.tag.Equals("Box"))
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        }
        //即不能交换物体也不为空,回到原来位置
        else
        {
            transform.SetParent(originalTransform);
            transform.position = originalTransform.transform.position;
        }
        //打开block停止检测
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

