//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using ILRuntime.Runtime.Enviorment;
//using System;
////��������Ϊ��ȡ��ʹ��WWW�ľ��棬Unity2018�Ժ��Ƽ�ʹ��UnityWebRequest�����ڼ����Կ���Demo��Ȼʹ��WWW
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

//    //AppDomain��ILRuntime����ڣ��������һ���������б��棬������Ϸȫ�־�һ��������Ϊ��ʾ�����㣬ÿ���������涼��������һ��
//    //�������ʽ��Ŀ����ȫ��ֻ����һ��AppDomain
//    //AppDomain appdomain;
//    //�������ⲿʹ�÷���ILRuntime�ı�����ȫ��ֻ��һ��
//    public ILRuntime.Runtime.Enviorment.AppDomain appdomain;

//    System.IO.MemoryStream fs;
//    System.IO.MemoryStream p;
//    void Start()
//    {
//        StartCoroutine(LoadHotFixAssembly());
//    }
//    IEnumerator LoadHotFixAssembly()
//    {
//        //����ʵ����ILRuntime��AppDomain��AppDomain��һ��Ӧ�ó�����ÿ��AppDomain����һ��������ɳ��
//        //appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
//        //������Ŀ��Ӧ�������д������ط�����dll�����ߴ����AssetBundle�ж�ȡ��ƽʱ�����Լ�Ϊ����ʾ����ֱ�Ӵ�StreammingAssets�ж�ȡ��
//        //��ʽ������ʱ����Ҫ������д������ط���ȡdll

//        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//        //���DLL�ļ���ֱ�ӱ���HotFix_Project.sln���ɵģ��Ѿ�����Ŀ�����ú����Ŀ¼ΪStreamingAssets����VS��ֱ�ӱ��뼴�����ɵ���ӦĿ¼�������ֶ�����
//        //����Ŀ¼��Assets\Samples\ILRuntime\1.6\Demo\HotFix_Project~
//        //���¼���д��ֻΪ��ʾ����û�д����ڱ༭���л���Androidƽ̨�Ķ�ȡ����Ҫ�����޸�
//#if UNITY_ANDROID
//        WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.dll");
//#else
//        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.dll");//ʹ��www������dll��Զ��ʱʹ��url������
//#endif
//        while (!www.isDone)
//            yield return null;
//        if (!string.IsNullOrEmpty(www.error))
//            UnityEngine.Debug.LogError(www.error);
//        byte[] dll = www.bytes;//dll�������ļ����������
//        www.Dispose();

//        //PDB�ļ��ǵ������ݿ⣬����Ҫ����־����ʾ������кţ�������ṩPDB�ļ����������ڻ��������ڴ棬��ʽ����ʱ�뽫PDBȥ��������LoadAssembly��ʱ��pdb��null����
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
//            //appdomain����������װ��dll��pdb�Ķ����ƴ���
//            appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
//        }
//        catch
//        {
//            Debug.LogError("�����ȸ�DLLʧ�ܣ���ȷ���Ѿ�ͨ��VS��Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln������ȸ�DLL");
//        }

//        InitializeILRuntime();
//        OnHotFixLoaded();
//    }

//    void InitializeILRuntime()
//    {
//#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
//        //����Unity��Profiler�ӿ�ֻ���������߳�ʹ�ã�Ϊ�˱�����쳣����Ҫ����ILRuntime���̵߳��߳�ID������ȷ���������к�ʱ�����Profiler
//        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
//#endif
//        //������һЩILRuntime��ע�ᣬHelloWorldʾ����ʱû����Ҫע���

//        //��CLR����Ϣ������Ϣ��Unity�еĲ˵�������Զ����ɣ����г�ʼ��
//        //ע��CLR�󶨣��������ķ�����ʣ���������
//        //ͨ��CLR�󶨣����ȹ��������������������ͺͽṹ�壨��Vector3�����Ͳ������Է�����Ƶ���ʽ
//        //���Ӷ����������Ч��
//        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);


//        //������������Ҫ
//        //����ILRuntimeĬ������£�ֻ֧��ϵͳĬ�ϵ�ί�У���UnityAction���������Զ���ί�У�������Ҫдһ���ӿ�������Unity��ί��
//        //��������
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
//        //HelloWorld����һ�η�������
//        //����HotFix_Project~��������е�HotFix_Project�����ռ��µ�InstanceClass���StaticFunTest�������÷������滹������Unity�����Log���ﵽ��һ����ILRuntime�������Unity�����Ч����
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
    //AppDomain��ILRuntime����ڣ��������һ���������б���
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
        //����ʵ����ILRuntime��AppDomain��AppDomain��һ��Ӧ�ó�����ÿ��AppDomain����һ��������ɳ��
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        //������Ŀ��Ӧ�������д������ط�����dll�����ߴ����AssetBundle�ж�ȡ��ƽʱ�����Լ�Ϊ����ʾ����ֱ�Ӵ�StreammingAssets�ж�ȡ��
        //��ʽ������ʱ����Ҫ������д������ط���ȡdll

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //���DLL�ļ���ֱ�ӱ���HotFix_Project.sln���ɵģ��Ѿ�����Ŀ�����ú����Ŀ¼ΪStreamingAssets����VS��ֱ�ӱ��뼴�����ɵ���ӦĿ¼�������ֶ�����
        //����Ŀ¼��Assets\Samples\ILRuntime\1.6\Demo\HotFix_Project~
        //���¼���д��ֻΪ��ʾ����û�д����ڱ༭���л���Androidƽ̨�Ķ�ȡ����Ҫ�����޸�
#if UNITY_ANDROID

        //ͨ����Դ�����ܼ��س����ļ�
        //��Ϊ�ļ�����֮���·��������Դ�����ܾ�����
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

        //PDB�ļ��ǵ������ݿ⣬����Ҫ����־����ʾ������кţ�������ṩPDB�ļ����������ڻ��������ڴ棬��ʽ����ʱ�뽫PDBȥ��������LoadAssembly��ʱ��pdb��null����
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


        //����ģʽ
        //PDB�ļ��ǵ������ݿ⣬����Ҫ����־����ʾ������кţ�������ṩPDB�ļ�
        //�������ڻ��������ڴ棬��ʽ����ʱ�뽫PDBȥ��
        // appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        appdomain.LoadAssembly(fs, null, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());


        InitializeILRuntime();
        OnHotFixLoaded();
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //����Unity��Profiler�ӿ�ֻ���������߳�ʹ�ã�Ϊ�˱�����쳣����Ҫ����ILRuntime���̵߳��߳�ID������ȷ���������к�ʱ�����Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif

        //ע��CLR�󶨣��������ķ�����ʣ���������
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
        //�ȸ���Ŀ֧��Я���﷨
        appdomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        //ע��LitJson���
        LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);

        appdomain.DebugService.StartDebugService(56000);

        //ע���¼���������ί��
        appdomain.DelegateManager.RegisterMethodDelegate<Vector3>();
        #region UGUIע��ί�к�ί��ת��
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


 //       #region A�ǲ�ע��ί�к�ί��ת��
 //       //A�ǲ���ڲ�ʹ���� �Զ���ί�� 
 //       //Ĭ��������ֻ��֧��Action Funcί�У�������Ҫע��
 //       //�õ���ί������ע��
 //       appdomain.DelegateManager.RegisterMethodDelegate<Pathfinding.Path>();
 //       //ί��ת���� OnPathDelegate ת���� Action<Path>
 //       appdomain.DelegateManager.RegisterDelegateConvertor<OnPathDelegate>((act) =>
 //       {
 //           return new OnPathDelegate((path) =>
 //           {
 //               ((Action<Pathfinding.Path>)act).Invoke(path);
 //           });
 //       });


 //       //֧�� List<GraphNode>().FindAll();
 //       //���� �ȸ���Ŀ��ʹ��  List<GraphNode>().FindAll_HotFixSupport �������дί��ת��
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




        #region ���ȸ�����List<T>.FindAll() ����

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


        #region ���ȸ�����List<T>.Sort() ����

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


        //�¼�������
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
