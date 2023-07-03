using HotFix_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HotFixMonoBehaviourMgr
{
    static HotFixMonoBehaviourMgr _instance;
    public static HotFixMonoBehaviourMgr instance
    {
        get
        {
            if (_instance == null) _instance = new HotFixMonoBehaviourMgr();
            return _instance;
        }
    }
    public Dictionary<GameObject, List<HotFixMonoBehaviour>> hotfixTypes = new Dictionary<GameObject, List<HotFixMonoBehaviour>>();

    private static List<HotFixMonoBehaviour> allTypes = new List<HotFixMonoBehaviour>();

    internal void AddCom(GameObject gameObject, HotFixMonoBehaviour hotFixMonoBehaviour)
    {
        List<HotFixMonoBehaviour> coms;
        if (!hotfixTypes.TryGetValue(gameObject, out coms))
        {
            hotfixTypes.Add(gameObject, new List<HotFixMonoBehaviour>());
        }

        hotfixTypes[gameObject].Add(hotFixMonoBehaviour);
    }

    internal void RemoveCom(GameObject gameObject, HotFixMonoBehaviour hotFixMonoBehaviour)
    {

        List<HotFixMonoBehaviour> coms;
        if (!hotfixTypes.TryGetValue(gameObject, out coms)) return;


        coms.Remove(hotFixMonoBehaviour);

        if (coms.Count == 0)
        {

            //Debug.Log("Remove key"+ gameObject.name);
            hotfixTypes.Remove(gameObject);
        }

    }



}

