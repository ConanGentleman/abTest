using HotFix_Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLoader : HotFixMonoBehaviour
{

    public override void Awake()
    {
        //JAssetBundleMgr.Instance.Instantiate_GameObject_From_Bundle("ui", "Canvas_017");
        Debug.Log("生成cube");
        //异步加载
        GameObject obj=ABMgr.GetInstance().LoadRes<GameObject>("uicube", "Cube") ;
        obj.transform.position = Vector3.zero;
    }

}
