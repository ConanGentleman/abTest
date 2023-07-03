using UnityEngine;
using System.Collections;
using System.IO;
using ILRuntime.Runtime.Enviorment;
//下面这行为了取消使用WWW的警告，Unity2018以后推荐使用UnityWebRequest，处于兼容性考虑Demo依然使用WWW
#pragma warning disable CS0618
public class HelloWorld : MonoBehaviour
{
    //AppDomain是ILRuntime的入口，最好是在一个单例类中保存，整个游戏全局就一个，这里为了示例方便，每个例子里面都单独做了一个
    //大家在正式项目中请全局只创建一个AppDomain
    AppDomain appdomain;

    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;
    void Start()
    {
        StartCoroutine(LoadHotFixAssembly());
    }

    IEnumerator LoadHotFixAssembly()
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
        WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.dll");
#else
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.dll");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();

        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
#if UNITY_ANDROID
        www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.pdb");
#else
        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.pdb");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] pdb = www.bytes;
        fs = new MemoryStream(dll);
        p = new MemoryStream(pdb);
        try
        {
            appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch
        {
            Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln编译过热更DLL");
        }

        InitializeILRuntime();
        OnHotFixLoaded();
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的
    }

    void OnHotFixLoaded()
    {
        //HelloWorld，第一次方法调用
        appdomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);

        #region appdomain 多种调用方式
        /////
        //Debug.Log("调用无参数静态方法");
        ////调用无参数静态方法，appdomain.Invoke("命名空间.类名", "方法名", 对象引用, 参数列表);
        //appdomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);
        ////调用带参数的静态方法
        //Debug.Log("调用带参数的静态方法");
        //appdomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest2", null, 123);
        //Debug.Log("通过IMethod调用方法");
        ////预先获得IMethod，可以减低每次调用查找方法耗用的时间
        //IType type = appdomain.LoadedTypes["HotFix_Project.InstanceClass"];
        ////根据方法名称和参数个数获取方法
        //IMethod method = type.GetMethod("StaticFunTest", 0);
        //appdomain.Invoke(method, null, null);
        //Debug.Log("指定参数类型来获得IMethod");
        //IType intType = appdomain.GetType(typeof(int));
        ////参数类型列表
        //List<IType> paramList = new List<ILRuntime.CLR.TypeSystem.IType>();
        //paramList.Add(intType);
        ////根据方法名称和参数类型列表获取方法
        //method = type.GetMethod("StaticFunTest2", paramList, null);
        //appdomain.Invoke(method, null, 456);
        //Debug.Log("实例化热更里的类");
        //object obj = appdomain.Instantiate("HotFix_Project.InstanceClass", new object[] { 233 });
        ////第二种方式
        //object obj2 = ((ILType)type).Instantiate();
        //Debug.Log("调用成员方法");
        //int id = (int)appdomain.Invoke("HotFix_Project.InstanceClass", "get_ID", obj, null);
        //Debug.Log("!! HotFix_Project.InstanceClass.ID = " + id);
        //id = (int)appdomain.Invoke("HotFix_Project.InstanceClass", "get_ID", obj2, null);
        //Debug.Log("!! HotFix_Project.InstanceClass.ID = " + id);
        //Debug.Log("调用泛型方法");
        //IType stringType = appdomain.GetType(typeof(string));
        //IType[] genericArguments = new IType[] { stringType };
        //appdomain.InvokeGenericMethod("HotFix_Project.InstanceClass", "GenericMethod", genericArguments, null, "TestString");
        //Debug.Log("获取泛型方法的IMethod");
        //paramList.Clear();
        //paramList.Add(intType);
        //genericArguments = new IType[] { intType };
        //method = type.GetMethod("GenericMethod", paramList, genericArguments);
        //appdomain.Invoke(method, null, 33333);
        #endregion
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

    void Update()
    {

    }
}
