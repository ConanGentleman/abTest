using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    //�ܹ���Controller�еõ��������
    private MainView mainView;

    private static MainController controller = null;

    public static MainController Controller
    {
        get
        {
            return controller;
        }
    }

    // 1�����������
    public static void ShowMe()
    {
        if(controller == null)
        {
            //ʵ����������
            GameObject res = Resources.Load<GameObject>("UI/MainPanel");
            GameObject obj = Instantiate(res);
            //�������ĸ�����Ϊcanvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);

            controller = obj.GetComponent<MainController>();
        }
        else
        {
            controller.gameObject.SetActive(true);
        }
        ////��������ص���ʽhide ������Ҫ��ʾ
        //mainView.gameObject.SetActive(true);
        ////��ʾ����� ����������Ϣ
        //mainView.updateInfo();
    }
    public static void HideMe()
    {
        if(controller != null)
        {
            controller.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //��ȡͬ��������һ�������ϵ� view�ű�
        mainView = this.GetComponent<MainView>();
        //��һ�ε� �������
        mainView.UpdateViewInfo(PlayerModel.Data);

        // 2������ �¼��ļ��� �����Ӧ��ҵ���߼�
        mainView.btnRole.onClick.AddListener(ClickRoleBtn);

        ///�ص㣡������
        ///���������ť��������UI�Ķ�Ӧ���ݻ���и���
        ///���������ݵ�ʱ�� PlayerModel�ͻ���Լ�ͨ���¼�����������
        //��Playermodelִ��SaveData()����������PlayerModel.UpdateInfo(),����������Ѻ���UpdateMainInfo��ӣ�ע�ᣩ����PlayerModel��updaeEvent�¼���ȥ��
        //���Ե�ִ���¼���ʱ����Ȼ����ת��MainController��ִ��UpdateMainInfo,��˾Ϳ�����UpdateMainInfo��ִ����Ϣ���¾�����
        PlayerModel.Data.AddEvent(UpdateMainInfo);

    }
    private void ClickRoleBtn()
    {
        Debug.Log("�����ť��ʾ��ɫ���");
        //ͨ��controllerȥ��ʾ��ɫ���
        RoleController.ShowMe();
    }

    // 3������ĸ���
    private void UpdateMainInfo(PlayerModel data)
    {
        if(mainView!=null)
            mainView.UpdateViewInfo(data);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEvent(UpdateMainInfo);
    }
}
