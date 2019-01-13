using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //地图各个组件
    //0.老家 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    public GameObject[] Item;

    private void Awake()
    {
        //实例化老家
        CreateItem(Item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //老家周围的墙
        CreateItem(Item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(Item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(Item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }
    private void CreateItem(GameObject CreateObject,Vector3 Position,Quaternion Rotation)
    {
        GameObject item = Instantiate(CreateObject, Position, Rotation);
        item.transform.SetParent(gameObject.transform);
    }
}
