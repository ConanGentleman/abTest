using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��Ϊһ��Ψһ������ģ�� һ����� Ҫ���Լ��Ǹ�����ģʽ����
/// Ҫ���Լ�������һ������ģʽ������
/// </summary>
public class PlayerModel
{
    //��������
    private string playerName;
    //���������޸� �ֿ��Ա��ⲿ����
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

    //֪ͨ�ⲿ���µ�ί���¼� ����������¼���ʱ����Լ�����ȥ���������Լ������и��£�
    //ͨ�������ⲿ������ϵ��������ֱ�ӻ�ȡ�ⲿ�����
    private Action<PlayerModel> updateEvent;

    //���ⲿ��һ�λ�ȡ������� ��λ�ȡ
    //ͨ������ģʽ�����ﵽ���ݵ�Ψһ�� �����ݵĻ�ȡ
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

    //������صĲ���
    // ��ʼ��
    public void init()
    {
        playerName = PlayerPrefs.GetString("PlayerName","cheng");
        lev = PlayerPrefs.GetInt("lev", 1);
    }
    // ���� ����
    public void LevUp()
    {
        //���� �ı�����
        lev += 1;

        SaveData();
    }
    // ����
    public void SaveData()
    {
        //���������� �洢������
        PlayerPrefs.SetInt("lev", lev);

        //�������� �����¼�
        UpdateInfo();
    }
    //����¼�
    public void AddEvent(Action<PlayerModel> func)
    {
        updateEvent += func; //��һ�������������Ѻ����浽�¼�������
    }
    //�Ƴ��¼�
    public void RemoveEvent(Action<PlayerModel> func)
    {
        updateEvent -= func; 
    }
    // ֪ͨ�ⲿ�������ݵķ�ʽ
    private void UpdateInfo()
    {
        //�ҵ���Ӧ�� ʹ�����ݵĽű���ȥ��������
        if (updateEvent != null)
        {
            updateEvent(this);//���Լ�����ȥAction��<>�е����һ������Ϊ�䷵��ֵ
        }
    }
}
