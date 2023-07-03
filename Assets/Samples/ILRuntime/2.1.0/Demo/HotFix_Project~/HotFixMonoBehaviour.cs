using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project
{
    public class HotFixMonoBehaviour
    {
        [HideInInspector]
        public Transform transform;
        [HideInInspector]
        public GameObject gameObject;

        private HotFixMonoBehaviourAdapter adapter;
            //被Unity主程序中调用
            public virtual void Awake()
            {
                Debug.Log("Awake");
            }
            public virtual void Start()
            {
                Debug.Log("Start");
            }
            public virtual void Update()
            {
              //  Debug.Log("Update");
            }
            public virtual void OnDestroy()
            {
                Debug.Log("OnDestroy");
            }

            public void BindAdapter(HotFixMonoBehaviourAdapter adapter)
        {
            this.adapter = adapter;
            this.gameObject = adapter.gameObject;
            this.transform = adapter.transform;
            HotFixMonoBehaviourMgr.instance.AddCom(gameObject, this);

        }


        private void OnAdapterRemove()
        {
            HotFixMonoBehaviourMgr.instance.RemoveCom(gameObject, this);

        }


        public Coroutine StartCoroutine(IEnumerator i)
        {
            return this.adapter.StartCoroutine(i);
        }


        public void StopCoroutine(Coroutine i)
        {
            this.adapter.StopCoroutine(i);
        }




        public T GetComponent_HotFix<T>() where T : HotFixMonoBehaviour
        {
            List<HotFixMonoBehaviour> coms;
            if (!HotFixMonoBehaviourMgr.instance.hotfixTypes.TryGetValue(this.gameObject, out coms))
            {
                return null;
            }


            for (int i = 0; i < coms.Count; i++)
            {
                if (coms[i].GetType() == typeof(T))
                {
                    return coms[i] as T;
                }
            }
            return default(T);
        }



        public static List<T> FindObjectsOfType_HotFix<T>() where T : HotFixMonoBehaviour
        {

            var iter = HotFixMonoBehaviourMgr.instance.hotfixTypes.Values.GetEnumerator();
            var list = new List<T>();
            while (iter.MoveNext())
            {
                var values = iter.Current;

                for (int i = 0; i < values.Count; i++)
                {

                    if (values[i].GetType() == typeof(T))
                    {
                        list.Add((T)values[i]);
                    }
                }

            }

            return list;


        }


        public static T FindObjectOfType_HotFix<T>() where T : HotFixMonoBehaviour
        {
            var iter = HotFixMonoBehaviourMgr.instance.hotfixTypes.Values.GetEnumerator();

            while (iter.MoveNext())
            {
                var values = iter.Current;

                for (int i = 0; i < values.Count; i++)
                {

                    if (values[i].GetType() == typeof(T))
                    {
                        return ((T)values[i]);
                    }
                }

            }

            return null;
        }

        //主程调用反序列化方法
        private void MainProjectCall_Deserialize(HotFixMonoBehaviourSerializeField com)
        {
            m_SetField(this, com.classData);
        }


        private void m_SetField(object obj, ClassData classData)
        {

            //string
            for (int i = 0; i < classData.stringFields.Count; i++)
            {
                var fieldData = classData.stringFields[i];
                SetValue(obj, fieldData.fieldName, fieldData.filedValue);
            }

            //stringArray
            for (int i = 0; i < classData.stringArrayFields.Count; i++)
            {
                var fieldData = classData.stringArrayFields[i];

                Array arrayInstance = Array.CreateInstance(typeof(string), fieldData.values.Count);
                //string[] stringArray=new int[Count];

                for (int j = 0; j < fieldData.values.Count; j++)
                {
                    //stringArray[j]=fieldData.values[j]
                    arrayInstance.SetValue(fieldData.values[j], j);
                }

                //new TestClass().strings=stringArray;
                SetValue(obj, fieldData.fieldName, arrayInstance);
            }



            //int
            for (int i = 0; i < classData.int32Field.Count; i++)
            {
                var fieldData = classData.int32Field[i];

                SetValue(obj, fieldData.fieldName, fieldData.filedValue);
            }

            //int[]
            for (int i = 0; i < classData.Int32ArrayField.Count; i++)
            {
                var fieldData = classData.Int32ArrayField[i];

                Array arrayInstance = Array.CreateInstance(typeof(int), fieldData.values.Count);
                //int[] intArray=new int[Count];

                for (int j = 0; j < fieldData.values.Count; j++)
                {
                    //intArray [j]=fieldData.values[j]
                    arrayInstance.SetValue(fieldData.values[j], j);
                }

                //TestClass.intArray=stringArray;
                SetValue(obj, fieldData.fieldName, arrayInstance);
            }

            // float
            for (int i = 0; i < classData.floatFields.Count; i++)
            {
                var fieldData = classData.floatFields[i];
                SetValue(obj, fieldData.fieldName, fieldData.filedValue);
            }

            //enum
            for (int i = 0; i < classData.enumFields.Count; i++)
            {
                var fieldData = classData.enumFields[i];
                SetValue(obj, fieldData.fieldName, fieldData.filedValue);
            }

            //GameoObject
            for (int i = 0; i < classData.gameObjectFields.Count; i++)
            {
                var fieldData = classData.gameObjectFields[i];
                SetValue(obj, fieldData.fieldName, fieldData.filedValue);
            }

            //Vector3
            for (int i = 0; i < classData.vector3Fields.Count; i++)
            {
                var fieldData = classData.vector3Fields[i];
                SetValue(obj, fieldData.fieldName, fieldData.filedValue);
            }

            //class
            for (int i = 0; i < classData.classFields.Count; i++)
            {
                ClassField classField = classData.classFields[i];
                Type ItemType = Type.GetType(classField.classType);

                var classInstance = Activator.CreateInstance(ItemType);
                m_SetField(classInstance, classField.classData);
                SetValue(obj, classField.fieldName, classInstance);
            }

            //class[]
            for (int i = 0; i < classData.classArrayFields.Count; i++)
            {
                ClassArrayField classArrayField = classData.classArrayFields[i];
                //T
                Type classType = Type.GetType(classArrayField.classType);
                if (classType == null) Debug.LogError("类型不存在，重新编译数据" + classArrayField.classType);
                //T[] testArray
                Array arrayInstance = Array.CreateInstance(classType, classArrayField.classDatas.Count);
                for (int j = 0; j < classArrayField.classDatas.Count; j++)
                {
                    //T test
                    var classInstance = Activator.CreateInstance(classType);

                    m_SetField(classInstance, classArrayField.classDatas[j]);
                    arrayInstance.SetValue(classInstance, j);
                }


                //T.test[]=testArray;
                SetValue(obj, classArrayField.fieldName, arrayInstance);

            }


            /*
            //反序列化字段 自定义类
            for (int i = 0; i < classData..Count; i++)
          {
              ClassField classField = data.classFields[i];

              for (int j = 0; j < classField.hotFixBaseField.Count; j++)
              {
                  var p_hotFixclassFieldData = classField.hotFixBaseField[j];
                  //自定义类实例化
                  var className = classField.classType;

                  Type ItemType = Type.GetType(className);
                  Debug.Log("className "+ className);
                  var classInstance = Activator.CreateInstance(ItemType);
                  Debug.Log(classInstance!=null);
                  //为类型赋值
                  for (int k = 0; k < p_hotFixclassFieldData.fileds.Count; k++)
                  {
                      var item= p_hotFixclassFieldData.fileds[k];

                      string fieldName = item.fieldName;
                      string filedValue = item.filedValue;
                      if (item.typeNum == FieldType.System_String)
                          SetValue(classInstance, fieldName, filedValue);

                      if (item.typeNum == FieldType.System_Boolean)
                          SetValue(classInstance, fieldName, Convert.ToBoolean(filedValue));

                      if (item.typeNum == FieldType.System_Int32)
                          SetValue(classInstance, fieldName, Convert.ToInt32(filedValue));

                      if (item.typeNum == FieldType.System_Enum)
                      {
                          Debug.Log(classInstance+"   " +fieldName + "   " + filedValue);
                          SetValue(classInstance, fieldName, Convert.ToInt32(filedValue));
                      }
                  }


                  SetValue(this, classField.fieldName, classInstance);
              }


          }


          //反序列化数组字段
          for (int i = 0; i < data.classArrayFields.Count; i++)
          {
              var ClassName = data.classArrayFields[i].classType;
              Type ItemType = Type.GetType(ClassName);
              var arrayInstance = Array.CreateInstance(ItemType, data.classArrayFields[i].size);

              for (int j = 0; j < data.classArrayFields[i].size; j++)
              {

                  var arryItems = data.classArrayFields[i].hotfixArry[j].fileds;

                  //数组类型实例
                  object item_instance = Activator.CreateInstance(ItemType);
                  for (int k = 0; k < arryItems.Count; k++)
                  {
                      BaseField baseField = arryItems[k];

                      string fieldName = baseField.fieldName;
                      string filedValue = baseField.filedValue;



                      if (baseField.typeNum == FieldType.System_String)
                          SetValue(item_instance, fieldName, filedValue);

                      if (baseField.typeNum == FieldType.System_Boolean)
                          SetValue(item_instance, fieldName, Convert.ToBoolean(filedValue));

                      if (baseField.typeNum == FieldType.System_Int32)
                          SetValue(item_instance, fieldName, Convert.ToInt32(filedValue));

                      if (baseField.typeNum == FieldType.System_Enum)
                          SetValue(item_instance, fieldName, Convert.ToInt32(filedValue));

                  }

                  arrayInstance.SetValue(item_instance,j);

                  //给字段赋值


              }

              SetValue(this, data.classArrayFields[i].fieldName, arrayInstance);


          }

          //反序列化基础类型数组
          for (int i = 0; i < data.baseArryFields.Count; i++)
          {
              Array arrayInstance=null;
              int typeNum = data.baseArryFields[i].typeNum;
              if (data.baseArryFields[i].typeNum == FieldType.System_Int32)
              {
                  arrayInstance = Array.CreateInstance(typeof(int), data.baseArryFields[i].size);
              }
              else if (data.baseArryFields[i].typeNum == FieldType.System_String)
              {
                  arrayInstance = Array.CreateInstance(typeof(string), data.baseArryFields[i].size);
              }
              else if (data.baseArryFields[i].typeNum == FieldType.System_Boolean)
              {
                  arrayInstance = Array.CreateInstance(typeof(bool), data.baseArryFields[i].size);
              }


              for (int j = 0; j < data.baseArryFields[i].values.Count; j++)
              {
                  {
                      // BaseField baseField = arryItems[k];

                      // string fieldName = baseField.fieldName;
                      string filedValue = data.baseArryFields[i].values[j];



                      if (typeNum == FieldType.System_String)
                          arrayInstance.SetValue(filedValue, j);

                      if (typeNum == FieldType.System_Boolean)
                          arrayInstance.SetValue(Convert.ToBoolean(filedValue),j);

                      if (typeNum == FieldType.System_Int32)
                          arrayInstance.SetValue(Convert.ToInt32(filedValue),j);

                      if (typeNum == FieldType.System_Enum)
                          arrayInstance.SetValue(Convert.ToInt32(filedValue),j);

                  }



                  //给字段赋值


              }

              SetValue(this, data.baseArryFields[i].fieldName, arrayInstance);


          }

          */
        }


        void SetValue(object entity, string fieldName, object fieldValue)
        {
            entity.GetType().GetField(fieldName).SetValue(entity, fieldValue);
        }
    }
}
