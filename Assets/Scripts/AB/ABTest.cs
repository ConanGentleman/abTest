using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTest : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{
    //    // 第一步 加载AB包（同步）
    //    AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/"+"ui");
    //    // 第二步 加载AB包中的MainPanel资源（同步）
    //    // 只是用名字加载 会出现 同命不同类型资源 分不清
    //    //ab.LoadAsset("MainPanel");
    //    // 建议使用 泛型加载 或者 Type指定类型
    //    //GameObject obj = ab.LoadAsset<GameObject>("MainPanel");
    //    //但是热更新通过lua,C#代码来进行加载时，用于lua不支持泛型加载，因此后续使用如下方法多一点
    //    GameObject obj = ab.LoadAsset("MainPanel",typeof(GameObject)) as GameObject;//（同步加载）
    //    Instantiate(obj,GameObject.Find("Canvas").transform);

    //    //AssetBundle.UnloadAllAssetBundles(true);//卸载所有AB包以及其场景上已经加载的资源
    //    //AssetBundle.UnloadAllAssetBundles(false);//只卸载所有AB包，其场景上已经加载的资源不受影响（常用）
    //    //ab.Unload(true);//卸载单个ab包及其场景上已经加载的资源（同步）
    //    //ab.Unload(false);//只卸载单个ab包，其场景上已经加载的资源不受影响（同步）
    //    //ab.UnloadAsync(true);//卸载单个ab包及其场景上已经加载的资源（异步）
    //    //ab.UnloadAsync(false);//只卸载单个ab包，其场景上已经加载的资源不受影响（异步)

    //    //依赖包的关键知识点—利用主包 获取依赖信息
    //    //加载主包
    //    AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "StandaloneWindowsAB");
    //    //加载主包中的固定文件
    //    AssetBundleManifest abMainfest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    //    //从固定文件中 得到依赖信息(得到uicube包的所有依赖信息）
    //    string[] strs = abMainfest.GetAllDependencies("uicube");
    //    //得到了 依赖包的名字
    //    for(int i = 0; i < strs.Length; i++)
    //    {
    //        Debug.Log(strs[i]);
    //        //依据依赖包名字加载依赖包
    //        AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + strs[i]);
    //    }

    //    // 同一个AB包不能够重复加载 否则报错
    //    //AssetBundle ab1 = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "ui");
    //    //加载另一个资源RolePanel
    //    GameObject obj1 = ab.LoadAsset<GameObject>("RolePanel");
    //    Instantiate(obj1, GameObject.Find("Canvas").transform);

    //    //异步加载AB资源->协程
    //    StartCoroutine(LoadABRes("uicube", "Cube"));
    //}
    private void Start()
    {
        //同步加载的三种方式
        //Object obj=ABMgr.GetInstance().LoadRes("uicube", "Cube");
        //GameObject obj=ABMgr.GetInstance().LoadRes("uicube", "Cube",typeof(GameObject)) as GameObject;
        //obj.transform.position = Vector3.one;
        //GameObject obj1=ABMgr.GetInstance().LoadRes<GameObject>("uicube", "Cube");
        //obj1.transform.position =- Vector3.one;
        //Instantiate(obj);

        //异步加载的三种方式
        ABMgr.GetInstance().LoadResAsync("uicube", "Cube", (obj) =>
        {
            (obj as GameObject).transform.position = Vector3.up;
        });
        ABMgr.GetInstance().LoadResAsync("uicube", "Cube", typeof(GameObject), (obj) =>
         {
             (obj as GameObject).transform.position = Vector3.one;
         });
        ABMgr.GetInstance().LoadResAsync<GameObject>("uicube", "Cube", (obj) =>
        {
            obj.transform.position = -Vector3.one;
        });
    }

    IEnumerator LoadABRes(string ABName,string resName)
    {
        // 第一步 加载AB包(异步）
        AssetBundleCreateRequest abAsync= AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
       
        yield return abAsync;//等待加载完，再进入第二步

        // 第二步 加载AB资源（异步）
        AssetBundleRequest objAsync=abAsync.assetBundle.LoadAssetAsync(resName, typeof(GameObject));
        yield return objAsync;//等待加载完，再实例化

        Instantiate(objAsync.asset as GameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
