using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 作为一个唯一的数据模型 一般情况 要不自己是个单例模式对象
/// 要不自己存在在一个单例模式对象中
/// </summary>
public class PlayerModel
{
    //数据内容
    private string playerName;
    //保护不被修改 又可以被外部访问
    public string PlayerName
    {
        get { return playerName; }
    }
    private int lev;
    public int Lev
    {
        get { return lev; }
    }
    //private int gem;
    //private int power;

    //private int hp;
    //private int atk;
    //private int def;
    //private int crit;
    //private int miss;
    //private int luck;

    //通知外部更新的委托事件 （触发这个事件的时候把自己传出去，外面用自己来进行更新）
    //通过它和外部建立联系，而不是直接获取外部的面板
    private Action<PlayerModel> updateEvent;

    //在外部第一次获取这个数据 如何获取
    //通过单例模式，来达到数据的唯一性 和数据的获取
    private static PlayerModel data = null;

    public static PlayerModel Data
    {
        get
        {
            if (data == null)
            {
                data = new PlayerModel();
                data.init();
            }
            return data;
        }
    }

    //数据相关的操作
    // 初始化
    public void init()
    {
        playerName = PlayerPrefs.GetString("PlayerName","cheng");
        lev = PlayerPrefs.GetInt("lev", 1);
    }
    // 更新 升级
    public void LevUp()
    {
        //升级 改变内容
        lev += 1;

        SaveData();
    }
    // 保存
    public void SaveData()
    {
        //把数据内容 存储到本地
        PlayerPrefs.SetInt("lev", lev);

        //保存数据 触发事件
        UpdateInfo();
    }
    //添加事件
    public void AddEvent(Action<PlayerModel> func)
    {
        updateEvent += func; //传一个函数进来，把函数存到事件容器里
    }
    //移除事件
    public void RemoveEvent(Action<PlayerModel> func)
    {
        updateEvent -= func; 
    }
    // 通知外部更新数据的方式
    private void UpdateInfo()
    {
        //找到对应的 使用数据的脚本，去更新数据
        if (updateEvent != null)
        {
            updateEvent(this);//把自己传出去Action的<>中的最后一个参数为其返回值
        }
    }
}
