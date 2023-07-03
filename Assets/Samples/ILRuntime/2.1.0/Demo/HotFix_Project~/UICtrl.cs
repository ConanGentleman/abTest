using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    class UICtrl: HotFixMonoBehaviour
    {
        public static UICtrl instance { get; private set; }
        static GameObject gob;
        GameObject uiCubeObj;
        private string name;
        void Awake()
        {
            Debug.Log("UIctrl初始");
            UICtrl.instance = this;
           uiCubeObj = getUICube("Cube");
        }
        public override void Start()
        {
            //Debug.Log("UIctrl加载模型");
            //uiCubeObj = getUICube("Cube");
            base.Start();
        }
        internal static void Init()
        {
            gob = new GameObject("UICtrl");
            gob.AddComponent_HotFix<UICtrl>();
            //MonoBehaviour.DontDestroyOnLoad(gob);
        }
        //public void Update()
        //{
        //    Debug.Log("UICtrlupdate");
        //}
        private GameObject getUICube(string path)
        {
            return ABMgr.GetInstance().LoadRes<GameObject>("uicube", path);
        }
    }
}
