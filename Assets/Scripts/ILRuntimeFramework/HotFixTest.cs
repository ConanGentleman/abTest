using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotFixTest : MonoBehaviour
{
    string className = "HotFix_Project.HotFixHello";//HotFixHello������������HotFix_Project��Ŀ���Զ��������
    string functionName = "TestHelloWorld";//TestHelloWorld������������HotFix_Project��Ŀ���Զ������HotFixHello�ľ�̬����
    void Start()
    {
        Debug.Log("HotFixTestStart");
        StartCoroutine(LoadILRuntime(0.1f));
        //����ֱ�ӵ��� ����������Ϊappdomain��Э�̼��ص�
        //HotFixMgr.instance.appdomain.Invoke(className, functionName, null, null);
    }
    IEnumerator LoadILRuntime(float t)
    {
        yield return new WaitForSeconds(t);
        //�ȸ�����(0.5f��ȴ�appdomain�ȼ����꣩
       HotFixMgr.instance.appdomain.Invoke(className, functionName, null, null);
    }
}
