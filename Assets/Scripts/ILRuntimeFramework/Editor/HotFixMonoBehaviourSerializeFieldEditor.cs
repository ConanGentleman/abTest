using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.IO; 

/// <summary>
/// 该编辑器工具负责
/// 读取热更程序集，通过反射获取 类型T的所有字段和成员，根据U3D的序列化流程 绘制到 组件面板
/// </summary>
[CustomEditor(typeof(HotFixMonoBehaviourSerializeField))]
public class HotFixMonoBehaviourSerializeFieldEditor : Editor
{
    bool isint;
    bool? mContainsHotFixClass = null;
    private string className;


    private Type rootClassType;


    private FieldInfo[] m_Fields;

    HotFixMonoBehaviourSerializeField m_runtime_target;
    private bool isChangeValue;

    private HotFixMonoBehaviourSerializeField runtime_target
    {
        get
        {
            if (m_runtime_target == null)
            {

                m_runtime_target = target as HotFixMonoBehaviourSerializeField;

            }
            return m_runtime_target;
        }
    }

    List<string> unkonwFields = new List<string>();



    bool ContainsHotFixClass(string className)
    {
        if (string.IsNullOrEmpty(className)) return false;
        if (mContainsHotFixClass == null)
        {
            mContainsHotFixClass = HotFixMgrEditor.instance.assembly.GetType(className) != null;
        }
        return (bool)mContainsHotFixClass;
    }

    Dictionary<string, Type> mapShowArryFieldType = new Dictionary<string, Type>();



    public override void OnInspectorGUI()
    {
        if (runtime_target.GetType() == typeof(HotFixMonoBehaviourAdapter)) return;
        var preClassName = runtime_target.bindClass;



        // 更新显示
        this.serializedObject.Update();

        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("bindClass"));




        m_DrawDefaultInspector = EditorGUILayout.Toggle("默认显示方式", m_DrawDefaultInspector);
        m_DrawCustomInspector = EditorGUILayout.Toggle("自定义显示方式", m_DrawCustomInspector);

        if (GUILayout.Button("刷新热更程序集"))
        {
            this.RefershHotFixClass();
        }
        if (m_DrawDefaultInspector)
        {
            DrawDefaultInspector();

        }





        if (!m_DrawCustomInspector)
        {
            return;
        }


        //if (Application.isPlaying) return;


        if (isint == false)
        {
            className = runtime_target.bindClass;
            if (ContainsHotFixClass(className))
            {
                className = runtime_target.bindClass;

                isint = true;


                //Debug.Log(runtime_target.classData.int32Field.Count);
                // Debug.Log(runtime_target.classData.gameObjectFields.Count);
            }
        }
        else
        {

            if (this.m_runtime_target == null) return;

            className = runtime_target.bindClass;
            var classData = runtime_target.classData;
            if (!ContainsHotFixClass(className)) return;


            //Debug.Log("gameObjectFields.Count " +runtime_target.classData.gameObjectFields.Count);

            if (rootClassType == null)
            {
                rootClassType = Class_Handler.SetClassFields(className, classData);

                //获取所有测试方法
                //获取所有方法 
                System.Reflection.MethodInfo[] methods = rootClassType.GetMethods();
                testFunc = methods.Where(s => s.GetCustomAttribute<ContextMenu>() != null);


            }

            GUILayout.Space(10);

            Class_Handler.Draw(classData);

            //显示测试方法
            foreach (var item in testFunc)
            {

                if (GUILayout.Button(item.GetCustomAttribute<ContextMenu>().menuItem))
                {
                    var p_testfunc = m_runtime_target.classType.GetMethod(item.Name, 0);
                    HotFixMgr.instance.appdomain.Invoke(p_testfunc, m_runtime_target.GetCLRInstance());

                }
            }
        }



        if (preClassName != runtime_target.bindClass)
        {
            ResetView();
        }

        if (GUI.changed)
        {
            if (Application.isPlaying == false)
            {
                //触发保存机制改为UI改变后生效
                Debug.Log("SetDirty");
                EditorUtility.SetDirty(target);
            }
            else if (Application.isPlaying)
            {
                m_runtime_target.EditorCall_GuiChange();
            }
        }
        this.serializedObject.ApplyModifiedProperties();







    }

    public static void SetSerialize_Vector3(FieldInfo item, List<Vector3Field> vector3Fields)
    {


        //不存在字段增加字段
        if (!ContainsField(item.Name, vector3Fields))
        {
            Vector3_Handler.Set_Field(item, vector3Fields);
        }
    }

    public static void SetSerialize_Float(FieldInfo item, List<FloatField> floatFields)
    {
        //不存在字段增加字段
        if (!ContainsField(item.Name, floatFields))
        {
            Single_Handler.Set_Field(item, floatFields);
        }
    }

    public static void SetSerialize_Int32(FieldInfo item, List<Int32Field> fs)
    {
        //不存在字段增加字段
        if (!ContainsField(item.Name, fs))
        {
            Debug.Log("SetSerialize_Int32 !ContainsField " + item.Name);
            Int32_Handler.Set_Field(item, fs);
        }
    }

    public static bool SetSerialize_GameObject(FieldInfo item, List<GameObjectField> serilze_fields)
    {
        // Debug.Log(item.Name);
        //  Debug.Log(serilze_fields.Count);
        //不存在字段增加字段
        if (!ContainsField(item.Name, serilze_fields))
        {
            Debug.Log("SetSerialize_GameObject !ContainsField " + item.Name);
            GameObject_Handler.Set_Field(item, serilze_fields);
        }

        return true;
    }


    internal static void SetSerialize_Boolean(FieldInfo item, List<BooleanField> booleanField)
    {
        //不存在字段增加字段
        if (!ContainsField(item.Name, booleanField))
        {
            Boolean_Handler.Set_Field(item, booleanField);
        }
    }

    public static void SetSerialize_String(FieldInfo item, List<StringField> serilze_fields)
    {


        //不存在字段增加字段
        if (!ContainsField(item.Name, serilze_fields))
        {
            String_Handler.Set_BaseField(item, serilze_fields);
        }
        else
        {
            //字段存在重新匹配类型,类型不匹配设置为空
            var t = serilze_fields.Where(s => s.fieldName == item.Name).FirstOrDefault();
            //if (t != null)
            //{
            //    var oldType = t.typeNum;
            //    t.typeNum = FieldType.TypeStr2Num(item.FieldType.Name);
            //    if (oldType != t.typeNum)
            //        t.filedValue = "";
            //}
        }
    }

    public static void SetSerialize_StringArray(FieldInfo item, List<StringArrayField> serilze_fields)
    {


        //不存在字段增加字段
        if (!ContainsField(item.Name, serilze_fields))
        {
            StringArray_Handler.Set_BaseField(item, serilze_fields);
        }
        else
        {
            //字段存在重新匹配类型,类型不匹配设置为空
            var t = serilze_fields.Where(s => s.fieldName == item.Name).FirstOrDefault();
            //if (t != null)
            //{
            //    var oldType = t.typeNum;
            //    t.typeNum = FieldType.TypeStr2Num(item.FieldType.Name);
            //    if (oldType != t.typeNum)
            //        t.filedValue = "";
            //}
        }
    }

    public static void SetSerialize_Enum(FieldInfo item, List<EnumField> serilze_fields)
    {


        //不存在字段增加字段
        if (!ContainsField(item.Name, serilze_fields))
        {
            Enum_Handler.Set_BaseField(item, serilze_fields);
        }
        else
        {
            //字段存在重新匹配类型,类型不匹配设置为空
            var t = serilze_fields.Where(s => s.fieldName == item.Name).FirstOrDefault();
            if (t != null)
            {
                var oldType = t.type;
                t.type = item.FieldType.ToString();
                if (oldType != t.type)
                    t.filedValue = 0;
            }
        }
    }

    internal static void SetSerialize_Int32Array(FieldInfo item, List<Int32ArrayField> int32ArrayField)
    {
        //不存在字段增加字段
        if (!ContainsField(item.Name, int32ArrayField))
        {
            Int32Array_Handler.Set_BaseField(item, int32ArrayField);
        }
    }

    public static void SetSerialize_Class(string fieldName, Type fieldType, ClassData clsdata)
    {


        //不存在字段增加字段
        if (!ContainsField(fieldName, clsdata.classFields))
        {
            //class Test{
            //      string str;(Update)
            //      int value;(Update)
            //      Test3 test
            //}

            Class_Handler.Set_Field(fieldName, fieldType, clsdata.classFields);
        }
        else
        {

            //字段存在重新匹配类型,类型不匹配设置为空
            var t = clsdata.classFields.Where(s => s.fieldName == fieldName).FirstOrDefault();
            if (t != null)
            {
                var oldType = t.classType;
                t.classType = fieldType.ToString();
                if (oldType != t.classType)
                    t.clear();
            }

            //嵌套类型字段更新
            //class Test{
            //      string str;
            //      int value;
            //      Test3 test;(Update)
            //}
            var child = clsdata.classFields.Find(s => s.fieldName == fieldName);
            Class_Handler.SetClassFields(fieldType.Name, child.classData);
        }

    }

    public static void SetSerialize_Class_Array(string fieldName, Type fieldType, ClassData clsdata)
    {
        List<ClassArrayField> list = clsdata.classArrayFields;

        //不存在字段增加字段
        if (!ContainsField(fieldName, list))
        {
            ClassArray_Handler.Set_Field(fieldName, fieldType, clsdata.classArrayFields);
        }
        else
        {

            //字段存在重新匹配类型,类型不匹配设置为空
            var t = clsdata.classArrayFields.Where(s => s.fieldName == fieldName).FirstOrDefault();
            if (t != null)
            {
                var oldType = t.classType;
                t.classType = fieldType.ToString();
                if (oldType != t.classType)
                    t.clear();
            }


            var child = clsdata.classArrayFields.Find(s => s.fieldName == fieldName);
            // ClassArray_Draw.SetClassFields(fieldType.Name, child.classData);
        }

    }

    void ResetView()
    {
        mContainsHotFixClass = null;
        rootClassType = null;
        runtime_target.classData.Clear();
        isint = false;

        mapShowArryFieldType.Clear();
        // mapFieldInfo.Clear();
    }

    void RefershHotFixClass()
    {
        HotFixMgrEditor.instance.reload();
        mContainsHotFixClass = null;
        rootClassType = null;
        // runtime_target.hotFixField.Clear();
        isint = false;

        mapShowArryFieldType.Clear();


    }



    public static bool ContainsField(string FieldName, List<Int32ArrayField> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }

    public static bool ContainsField(string FieldName, List<ClassArrayField> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }

    public static bool ContainsField(string FieldName, List<BooleanField> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }

    public static bool ContainsField(string FieldName, List<StringField> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }

    public static bool ContainsField(string FieldName, List<Int32Field> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }

    public static bool ContainsField(string FieldName, List<ClassField> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }

    public static bool ContainsField(string FieldName, List<EnumField> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }

    public static bool ContainsField(string FieldName, List<StringArrayField> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }

    public static bool ContainsField(string FieldName, List<Vector3Field> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }


    public static bool ContainsField(string FieldName, List<FloatField> hf)
    {
        foreach (var item in hf)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }

    public static bool ContainsField(string FieldName, List<GameObjectField> hf)
    {
        foreach (var item in hf)
        {


            if (item.fieldName == FieldName) return true;
        }

        return false;
    }




    public bool Contains_baseArryFields_FieldName(string FieldName, List<StringArrayField> stringArryFields)
    {
        foreach (var item in stringArryFields)
        {
            if (item.fieldName == FieldName) return true;
        }

        return false;
    }




    Dictionary<object, bool> foldoutMap = new Dictionary<object, bool>();
    private bool m_DrawDefaultInspector = false;
    private bool m_DrawCustomInspector = true;


    private void OnDisable()
    {
        //isint = false;
        // mContainsHotFixClass = null;
        // Debug.Log("OnDisable");
    }

    public Type GetDllType(string t)
    {
        return HotFixMgrEditor.instance.assembly.GetType(t);
    }

    static Assembly unityAssembly;
    private IEnumerable<MethodInfo> testFunc;

    Type GetUnityType(string t)
    {
        if (unityAssembly == null) unityAssembly = typeof(GameObject).Assembly;


        return unityAssembly.GetType(t);
    }


}



public class ItemInfo
{
    public string[] fieldsName;
    public Type type;
    public FieldInfo[] fields;
}

public class HotFixMgrEditor
{
    //AppDomain是ILRuntime的入口，最好是在一个单例类中保存
    static HotFixMgrEditor minstance;
    public static HotFixMgrEditor instance
    {
        get
        {
            if (minstance == null)
            {

                minstance = new HotFixMgrEditor();
                minstance.LoadHotFixAssembly();

            }
            return minstance;
        }

    }

    public Assembly assembly;

    void LoadHotFixAssembly()
    {

        Debug.Log("LoadHotFixAssembly");


        var rawAssembly = File.ReadAllBytes(Application.streamingAssetsPath + "/HotFix_Project.dll");
        // var rawSymbolStore = File.ReadAllBytes(Application.streamingAssetsPath + "/HotFix_Project.pdb");
        assembly = Assembly.Load(rawAssembly);


    }

    public bool compile = true;
    public void reload()
    {
        this.compile = true;
        //  LoadHotFixAssembly();
        MonoScript cMonoScript = MonoImporter.GetAllRuntimeMonoScripts().Where(s => s.name == "ReLoadHotDll").FirstOrDefault();
        MonoImporter.SetExecutionOrder(cMonoScript, UnityEngine.Random.Range(0, 15555));


    }

    /// <summary>
    /// 获取程序集实例中具有指定名称t的 Type 对象。
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public Type GetDllType(string t)
    {
        return HotFixMgrEditor.instance.assembly.GetType(t);
    }

    public static ItemInfo GetFieldClass(string className)
    {
        var rootClassType = HotFixMgrEditor.instance.assembly.GetType(className);
        if (rootClassType == null)
        {
            return null;
        }
        //返回FieldInfo[]   表示为当前 FieldInfo 定义的匹配指定绑定约束的所有字段的 Type 对象数组。
        // 如果没有为当前 FieldInfo 定义的字段，或者如果没有一个定义的字段匹配绑定约束，则为 Type 类型的空数组。
        /*
        GetFields(BindingFlags)要使重载成功检索属性信息，bindingAttr参数必须至少包含 和 BindingFlags.Static的BindingFlags.Instance一个 ，以及 至少一个 BindingFlags.NonPublic 和 BindingFlags.Public。
            以下 BindingFlags 筛选标志可用于定义要在搜索中包括的字段：

            指定 BindingFlags.Instance 以包含实例方法。

            指定 BindingFlags.Static 以包含静态方法。

            指定 BindingFlags.Public 以在搜索中包含公共字段。

            指定 BindingFlags.NonPublic 以包括非公共字段 (，即搜索中) 私有字段、内部字段和受保护字段。 仅返回基类上的受保护字段和内部字段;不返回基类上的私有字段。

            指定要 BindingFlags.FlattenHierarchy 在层次结构中包括 public 和 protected 静态成员; private 继承类中的静态成员不包括在层次结构中。

            单独指定 BindingFlags.Default 以返回空 PropertyInfo 数组。
         */
        var m_Fields = rootClassType.GetFields(BindingFlags.Instance | BindingFlags.Public);

        //不获取[HideInInspector]
        m_Fields = m_Fields.Where(s => s.GetCustomAttribute<HideInInspector>() == null).ToArray();

        var m_FieldNames = m_Fields.Select(s => s.Name).ToArray();

        return new ItemInfo() { type = rootClassType, fieldsName = m_FieldNames, fields = m_Fields };
    }



}








