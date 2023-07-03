using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotFixTest : MonoBehaviour
{
    string className = "HotFix_Project.HotFixHello";//HotFixHello是我们自行在HotFix_Project项目中自定义的类名
    string functionName = "TestHelloWorld";//TestHelloWorld是我们自行在HotFix_Project项目中自定义的类HotFixHello的静态函数
    void Start()
    {
        Debug.Log("HotFixTestStart");
        StartCoroutine(LoadILRuntime(0.1f));
        //不能直接调用 ，估计是因为appdomain是协程加载的
        //HotFixMgr.instance.appdomain.Invoke(className, functionName, null, null);
    }
    IEnumerator LoadILRuntime(float t)
    {
        yield return new WaitForSeconds(t);
        //热更加载(0.5f后等待appdomain先加载完）
       HotFixMgr.instance.appdomain.Invoke(className, functionName, null, null);
    }
}
