using ILRuntime.CLR.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ClassData类
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
/// 自定义类型
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
/// 自定义类型数组
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
/// 序列化热更类型
/// 当前支持 int,int[],string,string[],float,bool,枚举,GameObject
/// T,T[]
/// </summary>
public class HotFixMonoBehaviourSerializeField : HotFixMonoBehaviourAdapter
{
    //用于记录日更工程包含哪些字段及其类型和默认值
    //但是这种在Unity中的显示方式和编辑方式都和unity原生的MonoBehaviour相差甚远，不符合编辑习惯
    //因此需要写一个编辑器，来还原成MonoBehaviour的编辑和显示样式
    //
    public ClassData classData;
    private IMethod deserialize_Method;

    //相对父类多了一个反序列化方法的调用
    protected override void CallHotFixProject_awake()
    {
       
        //调用日更工程的反序列化方法
        deserialize_Method = classType.GetMethod("MainProjectCall_Deserialize", 1);

        if (deserialize_Method != null)
        {
            HotFixMgr.instance.appdomain.Invoke(deserialize_Method, instance, this);
        }

        base.CallHotFixProject_awake();
    }

#if UNITY_EDITOR
    //刷新数据只有在编辑器内有效
    public void EditorCall_GuiChange()
    {
        if (deserialize_Method != null)
        {
            HotFixMgr.instance.appdomain.Invoke(deserialize_Method, instance, this);
        }
    }
#endif
}
