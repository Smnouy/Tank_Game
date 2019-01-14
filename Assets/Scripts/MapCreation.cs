using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreation : MonoBehaviour
{
    //地图各个组件
    //0.老家 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    public GameObject[] Item;
    //已有东西的位置列表
    private List<Vector3> ItemPosition = new List<Vector3>();


    private void Awake()
    {
        InitMap();
    }

    //地图初始化
    private void InitMap()
    {
        //实例化老家和老家周围的墙
        CreateItem(Item[0], new Vector3(0, -8, 0), Quaternion.identity);
        CreateItem(Item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(Item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(Item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }

        //实例化外围空气墙
        for (int i = -11; i < 12; i++)
        {
            CreateItem(Item[6], new Vector3(i, 9, 0), Quaternion.identity);
            CreateItem(Item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -9; i < 10; i++)
        {
            CreateItem(Item[6], new Vector3(11, i, 0), Quaternion.identity);
            CreateItem(Item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }

        //实例化地图
        for (int i = 0; i < 60; i++)//墙
        {
            CreateItem(Item[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 25; i++)//障碍
        {
            CreateItem(Item[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 25; i++)//河流
        {
            CreateItem(Item[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 25; i++)//草
        {
            CreateItem(Item[5], CreateRandomPosition(), Quaternion.identity);
        }
        //初始化玩家敌人
        GameObject play = Instantiate(Item[3], new Vector3(-2, -8, 0), Quaternion.identity);//玩家
        play.GetComponent<Born>().CreatPlayer = true;

        Instantiate(Item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        Instantiate(Item[3], new Vector3(10, 8, 0), Quaternion.identity);
        Instantiate(Item[3], new Vector3(0, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 4, 5);
    }
    //创建地图物体的方法
    private void CreateItem(GameObject CreateObject,Vector3 Position,Quaternion Rotation)
    {
        GameObject item = Instantiate(CreateObject, Position, Rotation);
        item.transform.SetParent(gameObject.transform);
        ItemPosition.Add(Position);
    }

    //产生随机位置的方法
    private Vector3 CreateRandomPosition()
    {
        //不生成x=-10,10的两列，y=-8,8的两行的位置
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
            
        }
    }

    //判定位置上是否物体
    private bool HasThePosition(Vector3 Position)
    {
        for(int i = 0; i < ItemPosition.Count; i++)
        {
            if(Position == ItemPosition[i])
            {
                return true;
            }
        }
        return false;
    }

    //产生敌人的方法
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPosition = new Vector3();
        if (num == 0)
        {
            EnemyPosition = new Vector3(-10, 8, 0);
        }
        else if(num == 1)
        {
            EnemyPosition = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPosition = new Vector3(10, 8, 0);
        }
        Instantiate(Item[3], EnemyPosition, Quaternion.identity);
    }
}
