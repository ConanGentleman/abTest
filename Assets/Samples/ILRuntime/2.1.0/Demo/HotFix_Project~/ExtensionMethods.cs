using System;
using System.Collections.Generic;
using HotFix_Project;
//using Pathfinding;
using UnityEngine;



public static class ExtensionMethods
{

    //public static List<Vector3> toPos(this List<GraphNode> list)
    //{
    //    List<Vector3> i = new List<Vector3>();
    //    for (int j = 0; j < list.Count; j++)
    //    {
    //        i.Add((Vector3)list[j].position);
    //    }
    //    return i;
    //}


    //public static List<GraphNode> ToGraphNode(this List<PlayerController> list)
    //{
    //    List<GraphNode> i = new List<GraphNode>();

    //    if (list == null) return i;

    //    for (int j = 0; j < list.Count; j++)
    //    {
    //        i.Add(list[j].mapNode);
    //    }
    //    return i;
    //}

    //组件的获取通过遍历字典（由HotFixMonoBehaviourMgr管理）获取
    public static T GetComponent_HotFix<T>(this GameObject gameObject) where T : HotFixMonoBehaviour
    {


        List<HotFixMonoBehaviour> coms;
        if (!HotFixMonoBehaviourMgr.instance.hotfixTypes.TryGetValue(gameObject, out coms)) return null;

        for (int i = 0; i < coms.Count; i++)
        {
            if (coms[i].GetType() == typeof(T))
            {
                return coms[i] as T;
            }
        }
        return default(T);


    }




    /// <summary>
    ///热更工程可用，无需转换接口
    /// </summary>
    public static List<T> FindAll_HotFixSupport<T>(this List<T> list, Func<T, bool> lambda)
    {
        List<T> t = new List<T>();
        for (int i = 0; i < list.Count; i++)
        {
            if (lambda(list[i]))
            {
                t.Add(list[i]);
            }
        }
        return t;
    }

    /// <summary>
    ///组件添加的实现是利用自定义的接口，将类型转换为字符串，在接口进行对象的实例化 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T AddComponent_HotFix<T>(this GameObject go)
    {
        HotFixMonoBehaviourAdapter.constructorClass = typeof(T).ToString();
        return (T)((object)go.AddComponent<HotFixMonoBehaviourAdapter>().GetCLRInstance());
    }
}
