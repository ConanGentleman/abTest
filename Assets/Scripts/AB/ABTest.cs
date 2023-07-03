using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTest : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{
    //    // ��һ�� ����AB����ͬ����
    //    AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/"+"ui");
    //    // �ڶ��� ����AB���е�MainPanel��Դ��ͬ����
    //    // ֻ�������ּ��� ����� ͬ����ͬ������Դ �ֲ���
    //    //ab.LoadAsset("MainPanel");
    //    // ����ʹ�� ���ͼ��� ���� Typeָ������
    //    //GameObject obj = ab.LoadAsset<GameObject>("MainPanel");
    //    //�����ȸ���ͨ��lua,C#���������м���ʱ������lua��֧�ַ��ͼ��أ���˺���ʹ�����·�����һ��
    //    GameObject obj = ab.LoadAsset("MainPanel",typeof(GameObject)) as GameObject;//��ͬ�����أ�
    //    Instantiate(obj,GameObject.Find("Canvas").transform);

    //    //AssetBundle.UnloadAllAssetBundles(true);//ж������AB���Լ��䳡�����Ѿ����ص���Դ
    //    //AssetBundle.UnloadAllAssetBundles(false);//ֻж������AB�����䳡�����Ѿ����ص���Դ����Ӱ�죨���ã�
    //    //ab.Unload(true);//ж�ص���ab�����䳡�����Ѿ����ص���Դ��ͬ����
    //    //ab.Unload(false);//ֻж�ص���ab�����䳡�����Ѿ����ص���Դ����Ӱ�죨ͬ����
    //    //ab.UnloadAsync(true);//ж�ص���ab�����䳡�����Ѿ����ص���Դ���첽��
    //    //ab.UnloadAsync(false);//ֻж�ص���ab�����䳡�����Ѿ����ص���Դ����Ӱ�죨�첽)

    //    //�������Ĺؼ�֪ʶ�㡪�������� ��ȡ������Ϣ
    //    //��������
    //    AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "StandaloneWindowsAB");
    //    //���������еĹ̶��ļ�
    //    AssetBundleManifest abMainfest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    //    //�ӹ̶��ļ��� �õ�������Ϣ(�õ�uicube��������������Ϣ��
    //    string[] strs = abMainfest.GetAllDependencies("uicube");
    //    //�õ��� ������������
    //    for(int i = 0; i < strs.Length; i++)
    //    {
    //        Debug.Log(strs[i]);
    //        //�������������ּ���������
    //        AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + strs[i]);
    //    }

    //    // ͬһ��AB�����ܹ��ظ����� ���򱨴�
    //    //AssetBundle ab1 = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "ui");
    //    //������һ����ԴRolePanel
    //    GameObject obj1 = ab.LoadAsset<GameObject>("RolePanel");
    //    Instantiate(obj1, GameObject.Find("Canvas").transform);

    //    //�첽����AB��Դ->Э��
    //    StartCoroutine(LoadABRes("uicube", "Cube"));
    //}
    private void Start()
    {
        //ͬ�����ص����ַ�ʽ
        //Object obj=ABMgr.GetInstance().LoadRes("uicube", "Cube");
        //GameObject obj=ABMgr.GetInstance().LoadRes("uicube", "Cube",typeof(GameObject)) as GameObject;
        //obj.transform.position = Vector3.one;
        //GameObject obj1=ABMgr.GetInstance().LoadRes<GameObject>("uicube", "Cube");
        //obj1.transform.position =- Vector3.one;
        //Instantiate(obj);

        //�첽���ص����ַ�ʽ
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
        // ��һ�� ����AB��(�첽��
        AssetBundleCreateRequest abAsync= AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
       
        yield return abAsync;//�ȴ������꣬�ٽ���ڶ���

        // �ڶ��� ����AB��Դ���첽��
        AssetBundleRequest objAsync=abAsync.assetBundle.LoadAssetAsync(resName, typeof(GameObject));
        yield return objAsync;//�ȴ������꣬��ʵ����

        Instantiate(objAsync.asset as GameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
