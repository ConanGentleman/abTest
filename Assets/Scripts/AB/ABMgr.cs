using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ֪ʶ��
/// 1.AB�����API
/// 2.����ģʽ
/// 3.ί�У�lambda���ʽ
/// 4.Э��
/// 5.�ֵ�
/// </summary>
public class ABMgr : SingletonAutoMono<ABMgr>
{
    // AB������ Ŀ����
    //���ⲿ������Ľ�����Դ����

    //����
    private AssetBundle mainAB = null;
    //�����е���������ȡ�õ������ļ�
    private AssetBundleManifest manifest = null;

    //AB�����ܹ��ظ����� �ظ����ػᱨ��
    //�ֵ� ���ֵ����洢 ���ع���AB��
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// AB�����·�� �����޸�
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    //������ �����޸�
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
    
    ///����ab��
    public void LoadAB(string abName)
    {
        //����AB��
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        AssetBundle ab = null;
        //��ȡ�����������Ϣ
        string[] strs = manifest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            //�жϰ��Ƿ���ع�
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        // �������ⲿ��Ҫ���ص�AB��Դ��
        // ���û�м��ع� �ټ���
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }
    //ͬ������ ��ָ������
    public Object LoadRes(string abName,string resName)
    {
        //����AB��
        LoadAB(abName);
        //������Դ
        ////Ϊ�����淽�� �ټ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject �ǵĻ���ʵ�����ٷ���
        //Object obj = abDic[abName].LoadAsset(resName);
        //if (obj is GameObject)
        //    return Instantiate(obj);
        //else
        //    return obj;
        
        return abDic[abName].LoadAsset(resName);
    }
    /// <summary>
    /// Lua��ʹ�ý϶�ķ���
    /// ͬ������ ����typeָ������ 
    /// ����Lua���治֧�ַ��ͣ�������õ����͵� ��ʹ�ô����������ﵽ���͵�Ŀ��
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object LoadRes(string abName, string resName,System.Type type)
    {
        //����AB��
        LoadAB(abName);
        //������Դ
        //Ϊ�����淽�� �ټ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject �ǵĻ���ʵ�����ٷ���
        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    /// <summary>
    /// ͬ������ ���ݷ���ָ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T LoadRes<T>(string abName, string resName) where T:Object
    {
        //����AB��
        LoadAB(abName);
        //������Դ
        //Ϊ�����淽�� �ټ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject �ǵĻ���ʵ�����ٷ���
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }
    /// <summary>
    //�첽���أ����ί�У�
    //�첽����û�а취���ϵõ�ʹ����Դ��ʹ��ί�п���֪������Դ���������ôȥʹ����Դ
    //������첽���� AB����û��ʹ���첽����(ABTest.cs��LoadABRes���ᵽAB�����첽д������ʵ�ʳ����п��ܲ����ã�
    //ֻ�Ǵ�AB�� ������Դʱ ʹ���첽
    /// </summary>
    public void LoadResAsync(string abName, string resName,UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
    }
    /// <summary>
    /// ��������һ�첽����
    ///LoadResAsync���ṩ���ⲿ�ķ�����Ŀ��������ReallyLoadResAsyncЭ�̼�����Դ
    ///ReallyLoadResAsync��ʵ�ʼ�����Դ�ķ���
    ///����Ҳ���Բ���Э�̣�����AssetBundleRequest��completed�ص�Ҳ����
    ///������ί������Ϊ Э�̲����з���ֵ������ֻ����ί�������
    ///</summary>
    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        //����AB��
        LoadAB(abName);
        //������Դ
        //Ϊ�����淽�� �ټ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject �ǵĻ���ʵ�����ٷ���
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;//�ȴ����ؽ���
        
        //�첽���ؽ����� ͨ��ί�� ���ݸ��ⲿ ��ʹ��
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
        
    }

    /// <summary>
    //�첽���أ����ݣ�
    /// </summary>
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, type,callBack));
    }
    /// <summary>
    // ���������첽����
    ///</summary>
    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        //����AB��
        LoadAB(abName);
        //������Դ
        //Ϊ�����淽�� �ټ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject �ǵĻ���ʵ�����ٷ���
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;//�ȴ����ؽ���

        //�첽���ؽ����� ͨ��ί�� ���ݸ��ⲿ ��ʹ��
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);

    }

    /// <summary>
    //�첽���أ����ͣ�
    /// </summary>
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack)where T:Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack));
    }
    /// <summary>
    /// �����첽����
    ///</summary>
    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack)where T:Object
    {
        //����AB��
        LoadAB(abName);
        //������Դ
        //Ϊ�����淽�� �ټ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject �ǵĻ���ʵ�����ٷ���
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;//�ȴ����ؽ���

        //�첽���ؽ����� ͨ��ί�� ���ݸ��ⲿ ��ʹ��
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset) as T);
        else
            callBack(abr.asset as T);

    }

    //����ж��
    public void unLoad(string abName)
    {
        if (abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }
    //���а�ж��
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
