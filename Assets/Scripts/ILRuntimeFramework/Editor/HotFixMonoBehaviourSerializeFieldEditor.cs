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
/// �ñ༭�����߸���
/// ��ȡ�ȸ����򼯣�ͨ�������ȡ ����T�������ֶκͳ�Ա������U3D�����л����� ���Ƶ� ������
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



        // ������ʾ
        this.serializedObject.Update();

        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("bindClass"));




        m_DrawDefaultInspector = EditorGUILayout.Toggle("Ĭ����ʾ��ʽ", m_DrawDefaultInspector);
        m_DrawCustomInspector = EditorGUILayout.Toggle("�Զ�����ʾ��ʽ", m_DrawCustomInspector);

        if (GUILayout.Button("ˢ���ȸ�����"))
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

                //��ȡ���в��Է���
                //��ȡ���з��� 
                System.Reflection.MethodInfo[] methods = rootClassType.GetMethods();
                testFunc = methods.Where(s => s.GetCustomAttribute<ContextMenu>() != null);


            }

            GUILayout.Space(10);

            Class_Handler.Draw(classData);

            //��ʾ���Է���
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
                //����������Ƹ�ΪUI�ı����Ч
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


        //�������ֶ������ֶ�
        if (!ContainsField(item.Name, vector3Fields))
        {
            Vector3_Handler.Set_Field(item, vector3Fields);
        }
    }

    public static void SetSerialize_Float(FieldInfo item, List<FloatField> floatFields)
    {
        //�������ֶ������ֶ�
        if (!ContainsField(item.Name, floatFields))
        {
            Single_Handler.Set_Field(item, floatFields);
        }
    }

    public static void SetSerialize_Int32(FieldInfo item, List<Int32Field> fs)
    {
        //�������ֶ������ֶ�
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
        //�������ֶ������ֶ�
        if (!ContainsField(item.Name, serilze_fields))
        {
            Debug.Log("SetSerialize_GameObject !ContainsField " + item.Name);
            GameObject_Handler.Set_Field(item, serilze_fields);
        }

        return true;
    }


    internal static void SetSerialize_Boolean(FieldInfo item, List<BooleanField> booleanField)
    {
        //�������ֶ������ֶ�
        if (!ContainsField(item.Name, booleanField))
        {
            Boolean_Handler.Set_Field(item, booleanField);
        }
    }

    public static void SetSerialize_String(FieldInfo item, List<StringField> serilze_fields)
    {


        //�������ֶ������ֶ�
        if (!ContainsField(item.Name, serilze_fields))
        {
            String_Handler.Set_BaseField(item, serilze_fields);
        }
        else
        {
            //�ֶδ�������ƥ������,���Ͳ�ƥ������Ϊ��
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


        //�������ֶ������ֶ�
        if (!ContainsField(item.Name, serilze_fields))
        {
            StringArray_Handler.Set_BaseField(item, serilze_fields);
        }
        else
        {
            //�ֶδ�������ƥ������,���Ͳ�ƥ������Ϊ��
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


        //�������ֶ������ֶ�
        if (!ContainsField(item.Name, serilze_fields))
        {
            Enum_Handler.Set_BaseField(item, serilze_fields);
        }
        else
        {
            //�ֶδ�������ƥ������,���Ͳ�ƥ������Ϊ��
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
        //�������ֶ������ֶ�
        if (!ContainsField(item.Name, int32ArrayField))
        {
            Int32Array_Handler.Set_BaseField(item, int32ArrayField);
        }
    }

    public static void SetSerialize_Class(string fieldName, Type fieldType, ClassData clsdata)
    {


        //�������ֶ������ֶ�
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

            //�ֶδ�������ƥ������,���Ͳ�ƥ������Ϊ��
            var t = clsdata.classFields.Where(s => s.fieldName == fieldName).FirstOrDefault();
            if (t != null)
            {
                var oldType = t.classType;
                t.classType = fieldType.ToString();
                if (oldType != t.classType)
                    t.clear();
            }

            //Ƕ�������ֶθ���
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

        //�������ֶ������ֶ�
        if (!ContainsField(fieldName, list))
        {
            ClassArray_Handler.Set_Field(fieldName, fieldType, clsdata.classArrayFields);
        }
        else
        {

            //�ֶδ�������ƥ������,���Ͳ�ƥ������Ϊ��
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
    //AppDomain��ILRuntime����ڣ��������һ���������б���
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
    /// ��ȡ����ʵ���о���ָ������t�� Type ����
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
        //����FieldInfo[]   ��ʾΪ��ǰ FieldInfo �����ƥ��ָ����Լ���������ֶε� Type �������顣
        // ���û��Ϊ��ǰ FieldInfo ������ֶΣ��������û��һ��������ֶ�ƥ���Լ������Ϊ Type ���͵Ŀ����顣
        /*
        GetFields(BindingFlags)Ҫʹ���سɹ�����������Ϣ��bindingAttr�����������ٰ��� �� BindingFlags.Static��BindingFlags.Instanceһ�� ���Լ� ����һ�� BindingFlags.NonPublic �� BindingFlags.Public��
            ���� BindingFlags ɸѡ��־�����ڶ���Ҫ�������а������ֶΣ�

            ָ�� BindingFlags.Instance �԰���ʵ��������

            ָ�� BindingFlags.Static �԰�����̬������

            ָ�� BindingFlags.Public ���������а��������ֶΡ�

            ָ�� BindingFlags.NonPublic �԰����ǹ����ֶ� (����������) ˽���ֶΡ��ڲ��ֶκ��ܱ����ֶΡ� �����ػ����ϵ��ܱ����ֶκ��ڲ��ֶ�;�����ػ����ϵ�˽���ֶΡ�

            ָ��Ҫ BindingFlags.FlattenHierarchy �ڲ�νṹ�а��� public �� protected ��̬��Ա; private �̳����еľ�̬��Ա�������ڲ�νṹ�С�

            ����ָ�� BindingFlags.Default �Է��ؿ� PropertyInfo ���顣
         */
        var m_Fields = rootClassType.GetFields(BindingFlags.Instance | BindingFlags.Public);

        //����ȡ[HideInInspector]
        m_Fields = m_Fields.Where(s => s.GetCustomAttribute<HideInInspector>() == null).ToArray();

        var m_FieldNames = m_Fields.Select(s => s.Name).ToArray();

        return new ItemInfo() { type = rootClassType, fieldsName = m_FieldNames, fields = m_Fields };
    }



}








