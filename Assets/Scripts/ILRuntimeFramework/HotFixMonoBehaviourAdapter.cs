using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotFixMonoBehaviourAdapter : MonoBehaviour
{
    string className = "HotFix_Project.HotFixHello";//HotFixHello是我们自行在HotFix_Project项目中自定义的类名
    string functionName = "TestHelloWorld";//TestHelloWorld是我们自行在HotFix_Project项目中自定义的类HotFixHello的静态函数
    public static string constructorClass = "";

    public string bindClass = "";
    public IType classType;
    public ILTypeInstance instance;
    protected IMethod awake_method;
    protected IMethod update_method;
    protected IMethod start_method;



    protected IMethod OnDestroy_method;
    private IMethod LateUpdate_method;
    private IMethod AdapterRemove_method;
    private IMethod beforeStart_method;

    public object GetCLRInstance()
    {
        if (instance == null)
        {
            CreateInstantiate();
        }
        return instance.CLRInstance;
    }

    private void Awake()
    {
        Init(1f);
        //StartCoroutine(Init(1f));
        //需要创建的类型
        //HotFixMgr.instance.Init();
        //classType = HotFixMgr.instance.appdomain.LoadedTypes[bindClass];
        ////创建实例
        //instance = (classType as ILType).Instantiate();

        //IMethod awake_method = classType.GetMethod("Awake", 0);
        //update_method = classType.GetMethod("Update", 0);
        //start_method = classType.GetMethod("Start", 0);
        //OnDestory_method = classType.GetMethod("OnDestroy", 0);

        //if (awake_method != null)
        //    HotFixMgr.instance.appdomain.Invoke(awake_method, instance);
    }
    void Init(float t)
    {
        //yield return new WaitForSeconds(t);
        ////HotFixMgr.instance.appdomain.Invoke(className, functionName, null, null);
        ////需要创建的类型
        //classType = HotFixMgr.instance.appdomain.LoadedTypes[bindClass];//用于读取热更程序集指定的类型
        ////创建实例
        //instance = (classType as ILType).Instantiate();//再读取指定类型的热更程序集后，将其进行实例化

        ////通过反射去获得Awake、Update、Start、OnDestroy的方法
        //awake_method = classType.GetMethod("Awake", 0);
        //update_method = classType.GetMethod("Update", 0);
        //start_method = classType.GetMethod("Start", 0);
        //OnDestroy_method = classType.GetMethod("OnDestroy", 0);
        //LateUpdate_method = classType.GetMethod("LateUpdate", 0);
        //AdapterRemove_method = classType.GetMethod("OnAdapterRemove", 0);

        //beforeStart_method = classType.GetMethod("beforeStart", 0);
        ////然后在对应的生命周期，调用对应的方法
        //if (awake_method != null)
        //    HotFixMgr.instance.appdomain.Invoke(awake_method, instance);
        CreateInstantiate();

        var BindAdapter = classType.GetMethod("BindAdapter", 1);

        if (BindAdapter != null)
            HotFixMgr.instance.appdomain.Invoke(BindAdapter, instance, this);

        GetMethod();

        CallHotFixProject_awake();

    }
    protected virtual void CallHotFixProject_awake()
    {
        if (awake_method != null)
            HotFixMgr.instance.appdomain.Invoke(awake_method, instance);
    }
    protected void GetMethod()
    {
        awake_method = classType.GetMethod("Awake", 0);
        update_method = classType.GetMethod("Update", 0);
        start_method = classType.GetMethod("Start", 0);
        OnDestroy_method = classType.GetMethod("OnDestroy", 0);
        LateUpdate_method = classType.GetMethod("LateUpdate", 0);
        AdapterRemove_method = classType.GetMethod("OnAdapterRemove", 0);

        beforeStart_method = classType.GetMethod("beforeStart", 0);
    }

    protected void CreateInstantiate()
    {
        if (instance == null)
        {
            //try
            // {
            //通过代码创建
            if (string.IsNullOrEmpty(bindClass))
            {
                classType = HotFixMgr.instance.appdomain.LoadedTypes[constructorClass];
                bindClass = constructorClass;
                constructorClass = "";
                classType = HotFixMgr.instance.appdomain.LoadedTypes[bindClass];
            }
            else
            {

                HotFixMgr.instance.appdomain.LoadedTypes.TryGetValue(bindClass, out classType);
                //通过脚本编辑器创建
                if (classType == null)
                {
                    Debug.LogError(bindClass + "热更项找不到该类型");
                }
                // classType = HotFixMgr.instance.appdomain.LoadedTypes[bindClass];
            }




            // 创建实例
            instance = (classType as ILType).Instantiate();
            // }
            // catch (System.Exception)
            // {

            //     throw new System.Exception(this.gameObject.name + " 绑定类错误" + bindClass);
            // }

        }
    }

    public Coroutine DoCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void DoStopCoroutine(UnityEngine.Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }



    protected void Start()
    {
        if (beforeStart_method != null)
        {
            HotFixMgr.instance.appdomain.Invoke(beforeStart_method, instance);
        }

        if (start_method != null)
        {
            //try
            //{
            HotFixMgr.instance.appdomain.Invoke(start_method, instance);
            //}
            //catch (Exception e)
            //{

            //    Debug.LogError(bindClass + "运行时编译错误");
            //    throw e;

            //}

        }
    }

    protected void Update()
    {
        //Debug.Log(this.bindClass +" u3d proJect Update");
        if (update_method != null)
            HotFixMgr.instance.appdomain.Invoke(update_method, instance);
    }

    private void LateUpdate()
    {
        if (LateUpdate_method != null)
            HotFixMgr.instance.appdomain.Invoke(LateUpdate_method, instance);
    }


    protected void OnDestroy()
    {
        if (AdapterRemove_method != null)
            HotFixMgr.instance.appdomain.Invoke(AdapterRemove_method, instance);
        if (OnDestroy_method != null)
            HotFixMgr.instance.appdomain.Invoke(OnDestroy_method, instance);

        //Debug.Log("OnDestroy"+this.bindClass);
        instance = null;
    }
}
