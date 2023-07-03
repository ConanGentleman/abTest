using HotFix_Project;
using System;
using System.Collections;
using System.Collections.Generic;
//using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//using static GameDefineHotFix;

public class GameCtrl : HotFixMonoBehaviour
{
    //static GameObject gob;
    //public static GameCtrl instance { get; private set; }

    internal static void Awake()
    {
        UICtrl.Init();
    }

}
