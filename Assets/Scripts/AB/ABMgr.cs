using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 知识点
/// 1.AB包相关API
/// 2.单例模式
/// 3.委托（lambda表达式
/// 4.协程
/// 5.字典
/// </summary>
public class ABMgr : SingletonAutoMono<ABMgr>
{
    // AB管理器 目的是
    //让外部更方便的进行资源加载

    //主包
    private AssetBundle mainAB = null;
    //主包中的依赖包获取用的配置文件
    private AssetBundleManifest manifest = null;

    //AB包不能够重复加载 重复加载会报错
    //字典 用字典来存储 加载过的AB包
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// AB包存放路径 方便修改
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    //主包名 方便修改
    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif

        }
    }
    
    ///加载ab包
    public void LoadAB(string abName)
    {
        //加载AB包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        AssetBundle ab = null;
        //获取依赖包相关信息
        string[] strs = manifest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            //判断包是否加载过
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        // ·加载外部需要加载的AB资源包
        // 如果没有加载过 再加载
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }
    //同步加载 不指定类型
    public Object LoadRes(string abName,string resName)
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        ////为了外面方便 再加载资源时 判断一下 资源是不是GameObject 是的话就实例化再返回
        //Object obj = abDic[abName].LoadAsset(resName);
        //if (obj is GameObject)
        //    return Instantiate(obj);
        //else
        //    return obj;
        
        return abDic[abName].LoadAsset(resName);
    }
    /// <summary>
    /// Lua中使用较多的方法
    /// 同步加载 根据type指定类型 
    /// 用于Lua里面不支持泛型，因此设置到泛型的 就使用传入类型来达到泛型的目的
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object LoadRes(string abName, string resName,System.Type type)
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //为了外面方便 再加载资源时 判断一下 资源是不是GameObject 是的话就实例化再返回
        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    /// <summary>
    /// 同步加载 根据泛型指定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T LoadRes<T>(string abName, string resName) where T:Object
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //为了外面方便 再加载资源时 判断一下 资源是不是GameObject 是的话就实例化再返回
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }
    /// <summary>
    //异步加载（配合委托）
    //异步加载没有办法马上得到使用资源，使用委托可以知道在资源加载完后怎么去使用资源
    //这里的异步加载 AB包并没有使用异步加载(ABTest.cs中LoadABRes有提到AB包的异步写法，但实际场景中可能不适用）
    //只是从AB中 加载资源时 使用异步
    /// </summary>
    public void LoadResAsync(string abName, string resName,UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
    }
    /// <summary>
    /// 根据名字一异步加载
    ///LoadResAsync是提供给外部的方法，目的是启动ReallyLoadResAsync协程加载资源
    ///ReallyLoadResAsync是实际加载资源的方法
    ///这里也可以不用协程，监听AssetBundleRequest的completed回调也可以
    ///这里用委托是因为 协程不能有返回值，所以只能用委托来完成
    ///</summary>
    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //为了外面方便 再加载资源时 判断一下 资源是不是GameObject 是的话就实例化再返回
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;//等待加载结束
        
        //异步加载结束后 通过委托 传递给外部 来使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
        
    }

    /// <summary>
    //异步加载（根据）
    /// </summary>
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, type,callBack));
    }
    /// <summary>
    // 根据类型异步加载
    ///</summary>
    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //为了外面方便 再加载资源时 判断一下 资源是不是GameObject 是的话就实例化再返回
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;//等待加载结束

        //异步加载结束后 通过委托 传递给外部 来使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);

    }

    /// <summary>
    //异步加载（泛型）
    /// </summary>
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack)where T:Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack));
    }
    /// <summary>
    /// 泛型异步加载
    ///</summary>
    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack)where T:Object
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //为了外面方便 再加载资源时 判断一下 资源是不是GameObject 是的话就实例化再返回
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;//等待加载结束

        //异步加载结束后 通过委托 传递给外部 来使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset) as T);
        else
            callBack(abr.asset as T);

    }

    //单包卸载
    public void unLoad(string abName)
    {
        if (abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }
    //所有包卸载
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
