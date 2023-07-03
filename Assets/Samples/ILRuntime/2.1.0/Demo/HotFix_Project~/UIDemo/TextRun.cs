using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix_Project.UIDemo
{
    class TextRun
    {
        bool run = false;
        private GameObject textGob;
        void Awake()
        {
            //点击按钮时，文本开始滚动
            //由于热更工程缺少UI的dll组件，因此需要将UI.dll要引入到热更工程里
            //找到Ui.dll程序集文件（在Unity项目下Library\ScriptAssemblies里的UnityEngine.UI.dll

            //在热更工程VS里添加引用（右键项目-添加-引用）
            Debug.Log("Awake");
            var btn = GameObject.Find("Button").GetComponent<Button>();
            btn.onClick.AddListener(BtnClick);
            textGob = GameObject.Find("Canvas/Text (TMP)");//场景里Canvas下名为Text (TMP)的物体
        }
        private void BtnClick()
        {
            Debug.Log("BtnClick");
            run = true;
        }
        void Start()
        {
            Debug.Log("Start");
        }
        void Update()
        {
            // Debug.Log("Update");
            if (run)
            {
                //让文字移动
                textGob.transform.Translate(new Vector3(0.1f, 0, 0));
                //优化点： 由于在Update每帧都访问了Vector3，由于该结构体的程序集隶属于Unity3D的主工程，
                //所以现在是一个跨程序集的访问，默认情况下跨程序集都是通过反射机制来进行访问的，
                //这种访问方式的效率很低，因此我们为了提升效率，要进行CLR绑定，用以避开反射的访问
                //具体步骤：
                // 通过Unity中上方菜单栏的ILRuntime选择“通过自定分析热更DLL生成CLR绑定”
                //点击后可以在Samples/ILRuntime/Generated目录下看到生成的CLR绑定信息脚本
                //接着在HotFixMgr（热更管理类）里对CLR绑定信息进行初始化。
            }
        }
    }
}
public enum Dir
{
    left,
    right
}

//不加这个特性就无法通过U3D面板编辑
[System.Serializable]
public class MyData
{
    public GameObject textGob;
    public float moveSpeed;

    public Dir dir;
}


class TextRunHierarchy : HotFix_Project.HotFixMonoBehaviour
{
    private bool run = false;
    //public GameObject textGob;
    //public float speed = 0;

    public MyData[] myDatas;
    public override void Awake()
    {
        ////点击按钮，文本开始滚动
        var btn = GameObject.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(BtnClick);
        Debug.Log("TextRunHierarchyAwake");
        //run = true;
    }
    private void BtnClick()
    {
        Debug.Log("TextRunHierarchyBtnClick");
        run = true;
    }
    public override void Start()
    {
        Debug.Log("Start");
    }
    public override void Update()
    {
       // Debug.Log("myDatasLength:" + myDatas.Length);
       // Debug.Log("run:" + run1);
        if (run)
        {

            for (int i = 0; i < myDatas.Length; i++)
            {
                if (myDatas[i].dir == Dir.right)
                    myDatas[i].textGob.transform.Translate(new Vector3(myDatas[i].moveSpeed, 0, 0));
                else
                    myDatas[i].textGob.transform.Translate(new Vector3(-myDatas[i].moveSpeed, 0, 0));
            }
        }
    }
    public override void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}