using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController : MonoBehaviour
{
    // Start is called before the first frame update
    //�ܹ���Controller�еõ��������
    private RoleView roleView;

    private static RoleController controller = null;

    public static RoleController Controller
    {
        get
        {
            return controller;
        }
    }
    // 1�����������
    public static void ShowMe()
    {
        if (controller == null)
        {
            //ʵ����������
            GameObject res = Resources.Load<GameObject>("UI/RolePanel");
            GameObject obj = Instantiate(res);
            //�������ĸ�����Ϊcanvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);

            controller = obj.GetComponent<RoleController>();
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
        if (controller != null)
        {
            controller.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        roleView = this.GetComponent<RoleView>();
        //��һ�θ������
        roleView.UpdateViewInfo(PlayerModel.Data);

        roleView.btnClose.onClick.AddListener(ClickCloseBtn);
        roleView.btnUp.onClick.AddListener(ClickLevUpBtn);

        ///�ص㣡������
        ///���������ť��������UI�Ķ�Ӧ���ݻ���и���
        ///���������ݵ�ʱ�� PlayerModel�ͻ���Լ�ͨ���¼�����������
        //��Playermodelִ��SaveData()����������PlayerModel.UpdateInfo(),����������Ѻ���UpdateRoleInfo��ӣ�ע�ᣩ����PlayerModel��updaeEvent�¼���ȥ��
        //���Ե�ִ���¼���ʱ����Ȼ����ת��RoleController��ִ��UpdateRoleInfo,��˾Ϳ�����UpdateRoleInfo��ִ����Ϣ���¾�����
        PlayerModel.Data.AddEvent(UpdateRoleInfo);
    }
    private void UpdateRoleInfo(PlayerModel data)
    {
        if (roleView != null)
        {
            roleView.UpdateViewInfo(data);
        }
    }
    private void ClickCloseBtn()
    {
        HideMe();
    }
    //���������ť
    private void ClickLevUpBtn()
    {
        //ͨ������ģ�� �������� �ﵽ���ݸı�
        PlayerModel.Data.LevUp();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEvent(UpdateRoleInfo);
    }
}
