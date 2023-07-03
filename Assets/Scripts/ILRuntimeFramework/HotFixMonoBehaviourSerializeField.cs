using ILRuntime.CLR.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ClassData��
[System.Serializable]
public class ClassData
{
    public List<ClassArrayField> classArrayFields = new List<ClassArrayField>();

    public List<ClassField> classFields = new List<ClassField>();

    public List<StringField> stringFields = new List<StringField>();

    public List<StringArrayField> stringArrayFields = new List<StringArrayField>();

    public List<GameObjectField> gameObjectFields = new List<GameObjectField>();

    public List<FloatField> floatFields = new List<FloatField>();

    public List<Int32Field> int32Field = new List<Int32Field>();

    public List<Int32ArrayField> Int32ArrayField = new List<Int32ArrayField>();

    public List<BooleanField> booleanField = new List<BooleanField>();

    public List<Vector3Field> vector3Fields = new List<Vector3Field>();

    public List<EnumField> enumFields = new List<EnumField>();

    public void Clear()
    {

    }
}



[System.Serializable]
public class StringField
{
    public string fieldName;
    public string filedValue;
}

[System.Serializable]
public class EnumField
{
    public string fieldName;
    public int filedValue;
    public string type;
}

[System.Serializable]
public class GameObjectField
{
    public string fieldName;
    public UnityEngine.GameObject filedValue;
}

[System.Serializable]
public class FloatField
{
    public string fieldName;
    public float filedValue;
}

[System.Serializable]
public class Int32Field
{
    public string fieldName;
    public int filedValue;
}

[System.Serializable]
public class BooleanField
{

    public string fieldName;
    public bool filedValue;
}



[System.Serializable]
public class Vector3Field
{
    public string fieldName;
    public Vector3 filedValue;

}




/// <summary>
/// �Զ�������
/// </summary>
[System.Serializable]
public class ClassField
{
    public string fieldName;
    public string classType;
    public ClassData classData;

    public void clear()
    {
        classData.Clear();
    }
}

/// <summary>
/// �Զ�����������
/// </summary>
[System.Serializable]
public class ClassArrayField
{
    public string fieldName;
    public string classType;
    public int size = 0;
    public List<ClassData> classDatas;

    public void clear()
    {
        classDatas.Clear();
    }
}

[System.Serializable]
public class StringArrayField
{
    public string fieldName;
    public List<string> values = new List<string>();
    public int size;
}

[System.Serializable]
public class Int32ArrayField
{

    public string fieldName;
    public List<int> values = new List<int>();
    public int size;
}
#endregion 



/// <summary>
/// ���л��ȸ�����
/// ��ǰ֧�� int,int[],string,string[],float,bool,ö��,GameObject
/// T,T[]
/// </summary>
public class HotFixMonoBehaviourSerializeField : HotFixMonoBehaviourAdapter
{
    //���ڼ�¼�ո����̰�����Щ�ֶμ������ͺ�Ĭ��ֵ
    //����������Unity�е���ʾ��ʽ�ͱ༭��ʽ����unityԭ����MonoBehaviour�����Զ�������ϱ༭ϰ��
    //�����Ҫдһ���༭��������ԭ��MonoBehaviour�ı༭����ʾ��ʽ
    //
    public ClassData classData;
    private IMethod deserialize_Method;

    //��Ը������һ�������л������ĵ���
    protected override void CallHotFixProject_awake()
    {
       
        //�����ո����̵ķ����л�����
        deserialize_Method = classType.GetMethod("MainProjectCall_Deserialize", 1);

        if (deserialize_Method != null)
        {
            HotFixMgr.instance.appdomain.Invoke(deserialize_Method, instance, this);
        }

        base.CallHotFixProject_awake();
    }

#if UNITY_EDITOR
    //ˢ������ֻ���ڱ༭������Ч
    public void EditorCall_GuiChange()
    {
        if (deserialize_Method != null)
        {
            HotFixMgr.instance.appdomain.Invoke(deserialize_Method, instance, this);
        }
    }
#endif
}
