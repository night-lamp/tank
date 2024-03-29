﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //装饰初始化地图说需物体的数组
    //0.老家 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙 7.
    public GameObject[] item;

    //存储已经实例化物体的位置
    private List<Vector3> itemPositionList = new List<Vector3>();
    private void Awake()
    {
        InitMap();
    }

    private void InitMap()
    {
        //实例化老家
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //用墙把家围起来
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        //实例化空气墙
        for (int i = -11; i < 12; i++) //上
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++) //下
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++) //左
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++) //右
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }
        //初始化玩家
        GameObject player = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        player.GetComponent<Born>().createPlayer = true;
        //初始化敌人
        Instantiate(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        Instantiate(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        Instantiate(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 4, 5);
        //实例化地图
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[1], CreateRandomPositong(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[2], CreateRandomPositong(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[4], CreateRandomPositong(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[5], CreateRandomPositong(), Quaternion.identity);
        }
    }

    private void CreateItem(GameObject createGameObject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    private Vector3 CreateRandomPositong()
    {
        //在 X = -10 ，10；Y = -8，8这两行两列的位置上不生成物体
        while (true)
        {
            Vector3 createPositon = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8),0);
            if (!HasThePosition(createPositon))
            {
                return createPositon;
            }
            
        }
    }
    
    //用来判断位置列表内是否有这个位置
    private bool HasThePosition(Vector3 createPos)
    {
        for(int i = 0; i <itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
                return true;
        }
        return false;
    }
    
    //周期性的产生敌人
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if (num == 0)
            EnemyPos = new Vector3(-10, 8, 0);
        else if (num == 1)
            EnemyPos = new Vector3(0, 8, 0);
        else
            EnemyPos = new Vector3(10, 8, 0);
        CreateItem(item[3], EnemyPos, Quaternion.identity);

    }
}
