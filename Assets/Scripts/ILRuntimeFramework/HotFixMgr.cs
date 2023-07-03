//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using ILRuntime.Runtime.Enviorment;
//using System;
////下面这行为了取消使用WWW的警告，Unity2018以后推荐使用UnityWebRequest，处于兼容性考虑Demo依然使用WWW
//#pragma warning disable CS0618
//public class HotFixMgr : MonoBehaviour
//{
//    static HotFixMgr minstance;
//    public static HotFixMgr instance
//    {
//        get
//        {
//            if (minstance == null)
//            {
//                minstance = new GameObject("HotFixMgr").AddComponent<HotFixMgr>();
//                minstance.Init();
//                //minstance.LoadHotFixAssembly();
//            }
//            return minstance;
//        }
//    }

//    public void Init()
//    {
//        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
//    }

//    //AppDomain是ILRuntime的入口，最好是在一个单例类中保存，整个游戏全局就一个，这里为了示例方便，每个例子里面都单独做了一个
//    //大家在正式项目中请全局只创建一个AppDomain
//    //AppDomain appdomain;
//    //用来给外部使用访问ILRuntime的变量，全局只有一个
//    public ILRuntime.Runtime.Enviorment.AppDomain appdomain;

//    System.IO.MemoryStream fs;
//    System.IO.MemoryStream p;
//    void Start()
//    {
//        StartCoroutine(LoadHotFixAssembly());
//    }
//    IEnumerator LoadHotFixAssembly()
//    {
//        //首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
//        //appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
//        //正常项目中应该是自行从其他地方下载dll，或者打包在AssetBundle中读取，平时开发以及为了演示方便直接从StreammingAssets中读取，
//        //正式发布的时候需要大家自行从其他地方读取dll

//        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//        //这个DLL文件是直接编译HotFix_Project.sln生成的，已经在项目中设置好输出目录为StreamingAssets，在VS里直接编译即可生成到对应目录，无需手动拷贝
//        //工程目录在Assets\Samples\ILRuntime\1.6\Demo\HotFix_Project~
//        //以下加载写法只为演示，并没有处理在编辑器切换到Android平台的读取，需要自行修改
//#if UNITY_ANDROID
//        WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.dll");
//#else
//        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.dll");//使用www来加载dll，远程时使用url来加载
//#endif
//        while (!www.isDone)
//            yield return null;
//        if (!string.IsNullOrEmpty(www.error))
//            UnityEngine.Debug.LogError(www.error);
//        byte[] dll = www.bytes;//dll二进制文件读入变量中
//        www.Dispose();

//        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
//#if UNITY_ANDROID
//        www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.pdb");
//#else
//        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.pdb");
//#endif
//        while (!www.isDone)
//            yield return null;
//        if (!string.IsNullOrEmpty(www.error))
//            UnityEngine.Debug.LogError(www.error);
//        byte[] pdb = www.bytes;
//        fs = new MemoryStream(dll);
//        p = new MemoryStream(pdb);
//        try
//        {
//            //appdomain这个虚拟机来装载dll、pdb的二进制代码
//            appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
//        }
//        catch
//        {
//            Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln编译过热更DLL");
//        }

//        InitializeILRuntime();
//        OnHotFixLoaded();
//    }

//    void InitializeILRuntime()
//    {
//#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
//        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
//        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
//#endif
//        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的

//        //对CLR绑定信息（绑定信息在Unity中的菜单栏点击自动生成）进行初始化
//        //注册CLR绑定，避免过多的反射访问，提升性能
//        //通过CLR绑定，在热工程里访问主工程里的类型和结构体（如Vector3），就不会再以反射机制的形式
//        //，从而提高了运行效率
//        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);


//        //！！！！！重要
//        //由于ILRuntime默认情况下，只支持系统默认的委托，而UnityAction是是属于自定义委托，所以需要写一个接口来兼容Unity的委托
//        //否则会出错
//        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
//        {
//            return new UnityEngine.Events.UnityAction(() =>
//            {
//                ((Action)act).Invoke();
//            });
//        });
//    }

//    void OnHotFixLoaded()
//    {
//        //HelloWorld，第一次方法调用
//        //调用HotFix_Project~解决方案中的HotFix_Project命名空间下的InstanceClass类的StaticFunTest方法（该方法里面还调用了Unity引擎的Log，达到了一种在ILRuntime里面调用Unity引擎的效果）
//        appdomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);

//    }

//    private void OnDestroy()
//    {
//        if (fs != null)
//            fs.Close();
//        if (p != null)
//            p.Close();
//        fs = null;
//        p = null;
//    }

//    void Update()
//    {

//    }
//}
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Enviorment;
using System;
using System.IO;
using UnityEngine;

public class HotFixMgr : MonoBehaviour
{
    //AppDomain是ILRuntime的入口，最好是在一个单例类中保存
    static HotFixMgr minstance;
    public static HotFixMgr instance
    {
        get
        {
            if (minstance == null)
            {
                minstance = new GameObject("HotFixMgr").AddComponent<HotFixMgr>();
                minstance.LoadHotFixAssembly();
                DontDestroyOnLoad(minstance.gameObject);
            }

            return minstance;
        }

    }



    public ILRuntime.Runtime.Enviorment.AppDomain appdomain;

    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;

    void LoadHotFixAssembly()
    {
        //首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        //正常项目中应该是自行从其他地方下载dll，或者打包在AssetBundle中读取，平时开发以及为了演示方便直接从StreammingAssets中读取，
        //正式发布的时候需要大家自行从其他地方读取dll

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //这个DLL文件是直接编译HotFix_Project.sln生成的，已经在项目中设置好输出目录为StreamingAssets，在VS里直接编译即可生成到对应目录，无需手动拷贝
        //工程目录在Assets\Samples\ILRuntime\1.6\Demo\HotFix_Project~
        //以下加载写法只为演示，并没有处理在编辑器切换到Android平台的读取，需要自行修改
#if UNITY_ANDROID

        //通过资源管理框架加载程序集文件
        //因为文件更新之后的路径是由资源管理框架决定的
        //WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.dll");
        var dll = JAssetBundleMgr.Instance.LoadBytesFromFile("HotFix_Project.dll");
#else
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.dll");
#endif

        while (!www.isDone)
        {
            System.Threading.Thread.Sleep(100);
        }
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();

        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
#if UNITY_ANDROID

        //WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.pdb");
        //var pdb = JAssetBundleMgr.Instance.LoadBytesFromFile("HotFix_Project.pdb");
#else
        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.pdb");
#endif
        //while (!www.isDone)
        //{
        //    System.Threading.Thread.Sleep(100);
        //}
        //if (!string.IsNullOrEmpty(www.error))
        //    UnityEngine.Debug.LogError(www.error);
        // byte[] pdb = www.bytes;
        fs = new MemoryStream(dll);
        //  p = new MemoryStream(pdb);


        //发布模式
        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件
        //不过由于会额外耗用内存，正式发布时请将PDB去掉
        // appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        appdomain.LoadAssembly(fs, null, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());


        InitializeILRuntime();
        OnHotFixLoaded();
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif

        //注册CLR绑定，避免过多的反射访问，提升性能
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
        //热更项目支持携程语法
        appdomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        //注册LitJson类库
        LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);

        appdomain.DebugService.StartDebugService(56000);

        //注册事件管理器的委托
        appdomain.DelegateManager.RegisterMethodDelegate<Vector3>();
        #region UGUI注册委托和委托转换
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
        {
            return new UnityEngine.Events.UnityAction(() =>
            {
                ((Action)act).Invoke();
            });
        });
        appdomain.DelegateManager.RegisterMethodDelegate<bool>();
        //UI Toggle
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<bool>>((act) =>
        {
            return new UnityEngine.Events.UnityAction<bool>((value) =>
            {
                ((Action<bool>)act).Invoke(value);
            });
        });
        #endregion


 //       #region A星插注册委托和委托转换
 //       //A星插件内部使用了 自定义委托 
 //       //默认情况框架只下支持Action Func委托，所以需要注册
 //       //用到的委托类型注册
 //       appdomain.DelegateManager.RegisterMethodDelegate<Pathfinding.Path>();
 //       //委托转换器 OnPathDelegate 转换成 Action<Path>
 //       appdomain.DelegateManager.RegisterDelegateConvertor<OnPathDelegate>((act) =>
 //       {
 //           return new OnPathDelegate((path) =>
 //           {
 //               ((Action<Pathfinding.Path>)act).Invoke(path);
 //           });
 //       });


 //       //支持 List<GraphNode>().FindAll();
 //       //或者 热更项目中使用  List<GraphNode>().FindAll_HotFixSupport 而无需编写委托转换
 //       HotFixMgr.instance.appdomain.DelegateManager.RegisterMethodDelegate<Pathfinding.Path>();
 //       HotFixMgr.instance.appdomain.DelegateManager.RegisterFunctionDelegate<Pathfinding.GraphNode, System.Boolean>();

 //       appdomain.DelegateManager.RegisterFunctionDelegate<Pathfinding.GraphNode, bool>();
 //       appdomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<Pathfinding.GraphNode>
 //>((action) =>
 //{
 //    return new System.Predicate<Pathfinding.GraphNode>((obj) =>
 //    {
 //        return ((Func<Pathfinding.GraphNode, bool>)action).Invoke(obj);
 //    });
 //});

 //       #endregion




        #region 让热更工程List<T>.FindAll() 可用

        appdomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, bool>();

        appdomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<ILTypeInstance>
>((action) =>
{
    return new System.Predicate<ILTypeInstance>((obj) =>
    {
        return ((Func<ILTypeInstance, bool>)action).Invoke(obj);
    });


});
        appdomain.DelegateManager.RegisterFunctionDelegate<int, bool>();
        appdomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<int>
>((action) =>
{
    return new System.Predicate<int>((a) =>
    {
        return ((Func<int, bool>)action).Invoke(a);
    });




});
        #endregion


        #region 让热更工程List<T>.Sort() 可用

        appdomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, ILTypeInstance, int>();

        appdomain.DelegateManager.RegisterDelegateConvertor<Comparison<ILTypeInstance>>((act) =>
        {
            return new Comparison<ILTypeInstance>((a, b) =>
            {
                return ((Func<ILTypeInstance, ILTypeInstance, int>)act).Invoke(a, b);
                // ((Action<bool>)act).Invoke(value);
            });
        });

        #endregion


        //事件管理器
        HotFixMgr.instance.appdomain.DelegateManager.RegisterFunctionDelegate<System.Delegate, System.Delegate>();

    }

    void OnHotFixLoaded()
    {

    }

    private void OnDestroy()
    {
        if (fs != null)
            fs.Close();
        if (p != null)
            p.Close();
        fs = null;
        p = null;
    }


}
