
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Linq;
public interface FieldHandler { }
//常见类型的处理过程


public class String_Handler : FieldHandler
{
    public static void Set_BaseField(FieldInfo item, List<StringField> serilze_fields)
    {
        var typeInfo = item.FieldType.ToString();
        var IsEnum = item.FieldType.IsEnum;
        var fieldName = item.Name;

        serilze_fields.Add(defaultBaseFidle(fieldName));


    }



    public static StringField defaultBaseFidle(string p_fieldName)
    {

        //m_fieldName = p_fieldName;

        return new StringField() { fieldName = p_fieldName, filedValue = "" };
    }

    public static void Draw(StringField i)
    {
        //绘制到unity编辑器中，在unity中提供一个文本输入框
        i.filedValue = EditorGUILayout.TextField(i.fieldName, i.filedValue);
    }


    public static Type SerializeType { get { return typeof(string); } }




}

public class Int32_Handler : FieldHandler
{
    internal static Type SerializeType = typeof(int);

    internal static void Set_Field(FieldInfo item, List<Int32Field> fs)
    {
        var fieldName = item.Name;


        fs.Add(defaultBaseFidle(fieldName));
    }

    public static Int32Field defaultBaseFidle(string p_fieldName)
    {
        return new Int32Field() { fieldName = p_fieldName, filedValue = 0 };
    }

    public static void Draw(Int32Field f)
    {
        f.filedValue = EditorGUILayout.IntField(f.fieldName, f.filedValue);
    }


}

/// <summary>
/// float
/// </summary>
public class Single_Handler
{

    internal static Type SerializeType = typeof(float);

    internal static void Set_Field(FieldInfo item, List<FloatField> floatFields)
    {
        var fieldName = item.Name;

        floatFields.Add(defaultBaseFidle(fieldName));
    }

    public static FloatField defaultBaseFidle(string p_fieldName)
    {
        return new FloatField() { fieldName = p_fieldName, filedValue = 0 };
    }

    public static void Draw(FloatField i)
    {
        i.filedValue = EditorGUILayout.FloatField(i.fieldName, i.filedValue);
    }




}

public class Vector3_Handler
{

    internal static Type SerializeType = typeof(Vector3);

    internal static void Set_Field(FieldInfo item, List<Vector3Field> fields)
    {
        var fieldName = item.Name;

        fields.Add(defaultBaseFidle(fieldName));
    }

    public static Vector3Field defaultBaseFidle(string p_fieldName)
    {
        return new Vector3Field() { fieldName = p_fieldName, filedValue = new Vector3() };
    }

    public static void Draw(Vector3Field i)
    {
        i.filedValue = EditorGUILayout.Vector3Field(i.fieldName, i.filedValue);
    }




}

public class Boolean_Handler
{

    internal static Type SerializeType = typeof(bool);

    internal static void Set_Field(FieldInfo item, List<BooleanField> f)
    {
        var fieldName = item.Name;

        f.Add(defaultBaseFidle(fieldName));
    }

    public static BooleanField defaultBaseFidle(string p_fieldName)
    {
        return new BooleanField() { fieldName = p_fieldName, filedValue = false };
    }

    public static void Draw(BooleanField i)
    {
        i.filedValue = EditorGUILayout.Toggle(i.fieldName, i.filedValue);
    }




}


public class StringArray_Handler : FieldHandler
{

    internal static Type SerializeType = typeof(string[]);

    public static void Set_BaseField(FieldInfo item, List<StringArrayField> serilze_fields)
    {
        var typeInfo = item.FieldType.ToString();
        var IsEnum = item.FieldType.IsEnum;
        var fieldName = item.Name;

        serilze_fields.Add(defaultBaseFidle(fieldName));


    }





    public static StringArrayField defaultBaseFidle(string p_fieldName)
    {
        return new StringArrayField() { fieldName = p_fieldName, size = 0 };
    }


    public static void Draw(StringArrayField stringArrayField)
    {

        for (int i = 0; i < stringArrayField.values.Count; i++)
        {
            stringArrayField.values[i] = EditorGUILayout.TextField(i.ToString(), stringArrayField.values[i]);
        }

    }





}

public class Int32Array_Handler : FieldHandler
{

    internal static Type SerializeType = typeof(int[]);

    public static void Set_BaseField(FieldInfo item, List<Int32ArrayField> serilze_fields)
    {
        var typeInfo = item.FieldType.ToString();
        var IsEnum = item.FieldType.IsEnum;
        var fieldName = item.Name;

        serilze_fields.Add(defaultBaseFidle(fieldName));
    }


    public static Int32ArrayField defaultBaseFidle(string p_fieldName)
    {
        return new Int32ArrayField() { fieldName = p_fieldName, size = 0 };
    }


    public static void Draw(Int32ArrayField fields)
    {

        for (int i = 0; i < fields.values.Count; i++)
        {
            fields.values[i] = EditorGUILayout.IntField(i.ToString(), fields.values[i]);
        }

    }





}


public class Enum_Handler : FieldHandler
{

    internal static Type SerializeType = typeof(Enum);

    public static void Set_BaseField(FieldInfo item, List<EnumField> serilze_fields)
    {


        var fieldName = item.Name;

        var typeStr = item.FieldType.ToString();

        serilze_fields.Add(defaultBaseFidle(fieldName, typeStr));

    }





    public static EnumField defaultBaseFidle(string p_fieldName, string p_type)
    {
        return new EnumField() { fieldName = p_fieldName, filedValue = 0, type = p_type };
    }


    public static void Draw(EnumField field)
    {
        var f = HotFixMgrEditor.instance.GetDllType(field.type);
        var fields = f.GetFields(BindingFlags.Static | BindingFlags.Public);
        string[] options = fields.Select(s => s.Name).ToArray();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(field.fieldName, GUILayout.MaxWidth(100));
        //返回用户所选选项的索引
        field.filedValue = EditorGUILayout.Popup(field.filedValue, options, GUILayout.MaxWidth(200));
        EditorGUILayout.EndHorizontal();
    }



    public string fieldtype() { return SerializeType.ToString(); }


}

public class Class_Handler : FieldHandler
{
    private static Dictionary<object, bool> foldoutMap = new Dictionary<object, bool>();

    public static void Set_Field(string fieldName, Type fieldType, List<ClassField> serilze_fields)
    {
        var className = fieldType.ToString();
        var data = defaultBaseFidle(fieldName, className);
        serilze_fields.Add(data);

        //自定类型嵌套自定类型
        //class Test{
        //     Test value1;
        //}
        SetClassFields(className, data.classData);
    }

    public static void RemoveFileds(string[] m_FieldNames, ClassData classData, Type rootClassType)
    {
        RemoveUnContainsFiled(classData, m_FieldNames.ToArray(), rootClassType);
    }

    public static void RemoveFileds(FieldInfo item, ClassData classData)
    {
        var fieldName = item.Name;

        var className = item.FieldType.ToString();

        var rootClassType = HotFixMgrEditor.instance.assembly.GetType(className);

        var m_Fields = rootClassType.GetFields(BindingFlags.Instance | BindingFlags.Public);

        var m_FieldNames = m_Fields.Select(s => s.Name);


        // foreach (var c in serilze_fields)
        {
            RemoveUnContainsFiled(classData, m_FieldNames.ToArray(), rootClassType);
        }
        //递归移除嵌套类型
        foreach (var childClass in classData.classFields)
        {
            var childType = HotFixMgrEditor.instance.assembly.GetType(childClass.classType);
            var child_Inof = childType.GetField(childClass.fieldName);
            RemoveFileds(child_Inof, childClass.classData);
        }

    }

    public static Type SetClassFields(string className, ClassData classData)
    {
        var m = HotFixMgrEditor.GetFieldClass(className);

        //假如类型被删除
        if (m == null)
        {
            classData.classFields.RemoveAll(s => s.classType == className);
            return null;
        }

        //删除不存在class[]
        classData.classArrayFields.RemoveAll(s => HotFixMgrEditor.instance.GetDllType(s.classType) == null);

        ////先移除不存在的字段
        Class_Handler.RemoveFileds(m.fieldsName, classData, m.type);
        //Debug.Log("classData.gameObjectFields " + classData.gameObjectFields.Count);

        foreach (FieldInfo item in m.fields)
        {
            var dllFieldName = item.Name;
            {

                if (GameObject_Handler.Serialize == item.FieldType)
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_GameObject(item, classData.gameObjectFields);
                }
                else if (Int32_Handler.SerializeType == item.FieldType)
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_Int32(item, classData.int32Field);
                }

                else if (Single_Handler.SerializeType == item.FieldType)
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_Float(item, classData.floatFields);
                }
                else if (Int32Array_Handler.SerializeType == item.FieldType)
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_Int32Array(item, classData.Int32ArrayField);
                }
                else if (String_Handler.SerializeType == item.FieldType)
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_String(item, classData.stringFields);
                }
                else if (Boolean_Handler.SerializeType == item.FieldType)
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_Boolean(item, classData.booleanField);
                }
                else if (Vector3_Handler.SerializeType == item.FieldType)
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_Vector3(item, classData.vector3Fields);
                }
                else if (StringArray_Handler.SerializeType == item.FieldType)
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_StringArray(item, classData.stringArrayFields);
                }
                else if (item.FieldType.IsEnum)//Enum
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_Enum(item, classData.enumFields);
                }
                else if (Boolean_Handler.SerializeType == item.FieldType)
                {
                    HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_Boolean(item, classData.booleanField);
                }
                //Class  
                else if (item.FieldType.IsClass && !item.FieldType.IsArray && !item.FieldType.IsGenericType)
                {
                    Debug.Log("class " + item.Name);
                    //只序列化带标识的对象
                    if (item.FieldType.GetCustomAttribute<SerializableAttribute>() != null)
                    {
                        HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_Class(item.Name, item.FieldType, classData);
                    }
                }
                else if (item.FieldType.IsClass && item.FieldType.IsArray)//Class[] 不支持List<T>
                {
                    Debug.Log("class isArray " + item.Name);
                    //只序列化带标识的类型

                    var typeName = item.FieldType.ToString().Replace("[]", string.Empty);

                    var type = HotFixMgrEditor.instance.GetDllType(typeName);
                    //只序列化带标识的对象
                    if (type.GetCustomAttribute<SerializableAttribute>() != null)
                    {
                        HotFixMonoBehaviourSerializeFieldEditor.SetSerialize_Class_Array(item.Name, type, classData);

                    }
                }
            }
        }


        return m.type;
    }

    //移除不存在的字段，因为热更项目类型字段是不确定的
    static void RemoveUnContainsFiled(ClassData classData, string[] m_FieldNames, Type rootClassType)
    {

        //去除不存在的unityObjectFields字段
        classData.gameObjectFields.RemoveAll(s => !m_FieldNames.Contains(s.fieldName)
        || rootClassType.GetField(s.fieldName).FieldType != typeof(UnityEngine.GameObject));

        //浮点
        classData.floatFields.RemoveAll(s =>
        !m_FieldNames.Contains(s.fieldName)
        || rootClassType.GetField(s.fieldName).FieldType != Single_Handler.SerializeType
        );

        //vector3
        classData.vector3Fields.RemoveAll(s =>
       !m_FieldNames.Contains(s.fieldName)
       || rootClassType.GetField(s.fieldName).FieldType != Vector3_Handler.SerializeType
       );

        //string[]
        classData.stringArrayFields.RemoveAll(s =>
     !m_FieldNames.Contains(s.fieldName)
     || rootClassType.GetField(s.fieldName).FieldType != StringArray_Handler.SerializeType
     );
        //int[]
        classData.Int32ArrayField.RemoveAll(s =>
!m_FieldNames.Contains(s.fieldName)
|| rootClassType.GetField(s.fieldName).FieldType != Int32Array_Handler.SerializeType
);


        //Enum[]
        classData.enumFields.RemoveAll(s =>
     !m_FieldNames.Contains(s.fieldName)
     || rootClassType.GetField(s.fieldName).FieldType.ToString() != s.type
     );

        //class
        classData.classFields.RemoveAll(s =>
 !m_FieldNames.Contains(s.fieldName)
 || rootClassType.GetField(s.fieldName).FieldType.ToString() != s.classType
 || rootClassType.GetField(s.fieldName).FieldType.GetCustomAttribute<SerializableAttribute>() == null
 );
        //int32
        classData.int32Field.RemoveAll(s =>
!m_FieldNames.Contains(s.fieldName)
|| rootClassType.GetField(s.fieldName).FieldType != Int32_Handler.SerializeType
);

        //string
        classData.stringFields.RemoveAll(s =>
!m_FieldNames.Contains(s.fieldName)
|| rootClassType.GetField(s.fieldName).FieldType != String_Handler.SerializeType
);

        //bool
        classData.booleanField.RemoveAll(s =>
!m_FieldNames.Contains(s.fieldName)
|| rootClassType.GetField(s.fieldName).FieldType != String_Handler.SerializeType
);


    }

    public static ClassField defaultBaseFidle(string p_fieldName, string className)
    {
        return new ClassField() { fieldName = p_fieldName, classType = className, classData = new ClassData() };
    }

    public static void Draw(ClassData classData)

    {

        //为了性能考虑不支持多类型嵌套
        //递归显示
        if (classData.classFields.Count > 0)
            foreach (var childClassData in classData.classFields)
            {
                GUILayout.Space(10);

                GUILayout.Label(childClassData.classType + " " + childClassData.fieldName);
                Class_Handler.Draw(childClassData.classData);

                GUILayout.Space(10);
            }




        //string 字段
        foreach (StringField item in classData.stringFields)
        {
            String_Handler.Draw(item);
        }
        //foreach (ClassField classitem in runtime_target.classFields)
        //{

        //    var fieldInfo = rootClassType.GetField(classitem.fieldName);

        //    CompileSync(fieldInfo.FieldType, classitem);


        //    GUILayout.Label(classitem.classType + "    " + classitem.fieldName);
        //    DrawHotFixClassField(classitem, fieldInfo.FieldType);
        //    GUILayout.Space(10);
        //}


        #region Unity对象 绘制

        //检查编译后的类是否存在该字段
        //runtime_target.unityObjectFields = runtime_target.unityObjectFields.Where(s => dllFields.Contains(s.fieldName)).ToList();

        //去重，字段类型改变时
        // runtime_target.unityObjectFields = runtime_target.unityObjectFields.

        //HideInInspector
        // runtime_target.unityObjectFields = runtime_target.unityObjectFields.Where(s => rootClassType.GetField(s.fieldName).GetType().GetCustomAttribute(typeof(HideInInspector), false) != null).ToList();

        //显示类字段
        foreach (GameObjectField gameField in classData.gameObjectFields)
        {

            GameObject_Handler.Draw(gameField);

            // GUILayout.Space(10);
        }

        #endregion

        //绘制float
        foreach (var item in classData.floatFields)
        {
            Single_Handler.Draw(item);
        }

        //绘制Int32
        foreach (var item in classData.int32Field)
        {
            Int32_Handler.Draw(item);
        }

        //绘制vector3
        foreach (var item in classData.vector3Fields)
        {
            Vector3_Handler.Draw(item);
        }

        //Bool
        foreach (var item in classData.booleanField)
        {
            Boolean_Handler.Draw(item);
        }

        //绘制枚举
        //绘制vector3
        foreach (var item in classData.enumFields)
        {
            Enum_Handler.Draw(item);
        }



        //string[]
        foreach (StringArrayField itemArray in classData.stringArrayFields)
        {
            //折叠数组
            bool show;
            if (!foldoutMap.TryGetValue(itemArray, out show))
            {
                foldoutMap.Add(itemArray, true);

            }

            show = EditorGUILayout.Foldout(show, itemArray.fieldName);
            foldoutMap[itemArray] = show;
            if (!show) continue;


            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(itemArray.fieldName + " Size ");
            var oldSize = itemArray.size;
            itemArray.size = EditorGUILayout.IntField(itemArray.size);

            GUILayout.EndHorizontal();

            //数组长度改变
            if (oldSize != itemArray.size)
            {
                var tem = itemArray.values.ToArray();
                Debug.Log(tem.Length);
                itemArray.values.Clear();
                for (int i = 0; i < itemArray.size; i++)
                {

                    if (tem.Length > i)
                    {
                        itemArray.values.Add(tem[i]);
                    }
                    else
                    {
                        itemArray.values.Add("");
                    }
                }
            }

            StringArray_Handler.Draw(itemArray);
        }


        //int[]
        //string 数组
        foreach (Int32ArrayField itemArray in classData.Int32ArrayField)
        {
            //折叠数组
            bool show;
            if (!foldoutMap.TryGetValue(itemArray, out show))
            {
                foldoutMap.Add(itemArray, true);

            }

            show = EditorGUILayout.Foldout(show, itemArray.fieldName);
            foldoutMap[itemArray] = show;
            if (!show) continue;


            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(itemArray.fieldName + " Size ");
            var oldSize = itemArray.size;
            itemArray.size = EditorGUILayout.IntField(itemArray.size);

            GUILayout.EndHorizontal();

            //数组长度改变
            if (oldSize != itemArray.size)
            {
                var tem = itemArray.values.ToArray();
                Debug.Log(tem.Length);
                itemArray.values.Clear();
                for (int i = 0; i < itemArray.size; i++)
                {

                    if (tem.Length > i)
                    {
                        itemArray.values.Add(tem[i]);
                    }
                    else
                    {
                        itemArray.values.Add(0);
                    }
                }
            }

            Int32Array_Handler.Draw(itemArray);
        }

        //显示自定义类型数组
        foreach (ClassArrayField itemArray in classData.classArrayFields)
        {
            //折叠数组
            bool show;
            if (!foldoutMap.TryGetValue(itemArray, out show))
            {
                foldoutMap.Add(itemArray, true);

            }

            show = EditorGUILayout.Foldout(show, itemArray.fieldName);
            foldoutMap[itemArray] = show;
            var oldSize = itemArray.size;
            if (show)
            {

                GUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(itemArray.fieldName + " Size ");

                itemArray.size = EditorGUILayout.IntField(itemArray.size);

                GUILayout.EndHorizontal();
            }
            //数组长度改变，或者热项目编译
            if (oldSize != itemArray.size)
            {
                var tem = itemArray.classDatas.ToArray();
                Debug.Log(tem.Length);
                itemArray.classDatas.Clear();
                for (int i = 0; i < itemArray.size; i++)
                {

                    if (tem.Length > i)
                    {
                        itemArray.classDatas.Add(tem[i]);
                    }
                    else
                    {
                        var d = new ClassData();

                        //ClassArray_Draw.Set_Field2(itemArray.fieldName, itemArray.classType, itemArray.classDatas);
                        itemArray.classDatas.Add(d);
                    }
                }

                //设置数组item默认值
                ClassArray_Handler.SetClassArray_Fields(itemArray.classType, itemArray);

            }

            if (HotFixMgrEditor.instance.compile)
            {
                Debug.Log("compile array");
                HotFixMgrEditor.instance.compile = false;
                ClassArray_Handler.SetClassArray_Fields(itemArray.classType, itemArray);
            }

            if (show)
            {
                ClassArray_Handler.Draw(itemArray);
            }
        }

    }
}


public class ClassArray_Handler
{
    internal static void Draw(ClassArrayField ClassArrayfield)
    {
        int i = 0;
        foreach (ClassData item in ClassArrayfield.classDatas)
        {

            //foreach (var item2 in item.classArrayFields)
            {
                GUILayout.Label(i.ToString());
                Class_Handler.Draw(item);
                i++;
            }
            GUILayout.Space(10);
        }
    }

    internal static void Set_Field(string fieldName, Type fieldType, List<ClassArrayField> serilze_fields)
    {
        var className = fieldType.ToString();
        var data = defaultBaseFidle(fieldName, className);
        serilze_fields.Add(data);

        //自定类型嵌套自定类型
        //class Test{
        //     Test value1;
        //}
        SetClassArray_Fields(className, data);
    }

    public static void SetClassArray_Fields(string className, ClassArrayField data)
    {
        foreach (ClassData item in data.classDatas)
        {
            //count =0
            // foreach (var item2 in item.classArrayFields)
            {
                Class_Handler.SetClassFields(className, item);
            }

        }
    }

    public static ClassArrayField defaultBaseFidle(string fieldName, string className)
    {
        return new ClassArrayField() { classType = className.Replace("[]", ""), fieldName = fieldName, classDatas = new List<ClassData>() };
    }


}


public class GameObject_Handler : FieldHandler
{
    internal static Type Serialize = typeof(GameObject);

    private static GameObjectField defaultBaseFidle(string p_fieldName, GameObject value)
    {
        Debug.Log("GameObjectField defaultBaseFidle");
        return new GameObjectField() { fieldName = p_fieldName, filedValue = value };
    }

    internal static void Set_Field(FieldInfo item, List<GameObjectField> serilze_fields)
    {
        var fieldName = item.Name;
        serilze_fields.Add(defaultBaseFidle(fieldName, null));
    }

    public static void Draw(GameObjectField i)
    {

        i.filedValue = (UnityEngine.GameObject)EditorGUILayout.ObjectField(i.fieldName, i.filedValue, typeof(GameObject), true);

        // i.filedValue = EditorGUILayout.ObjectField(i.fieldName, i.filedValue, assembly.GetType("UnityEngine.GameObject"), true);
    }


}

