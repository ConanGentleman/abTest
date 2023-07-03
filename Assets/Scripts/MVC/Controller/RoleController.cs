using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController : MonoBehaviour
{
    // Start is called before the first frame update
    //能够在Controller中得到界面才行
    private RoleView roleView;

    private static RoleController controller = null;

    public static RoleController Controller
    {
        get
        {
            return controller;
        }
    }
    // 1、界面的显隐
    public static void ShowMe()
    {
        if (controller == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/RolePanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象为canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);

            controller = obj.GetComponent<RoleController>();
        }
        else
        {
            controller.gameObject.SetActive(true);
        }
        ////如果是隐藏的形式hide 在这里要显示
        //mainView.gameObject.SetActive(true);
        ////显示完面板 更新面板的信息
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
        //第一次更新面板
        roleView.UpdateViewInfo(PlayerModel.Data);

        roleView.btnClose.onClick.AddListener(ClickCloseBtn);
        roleView.btnUp.onClick.AddListener(ClickLevUpBtn);

        ///重点！！！！
        ///点击升级按钮后，其它的UI的对应数据会进行更新
        ///当更新数据的时候 PlayerModel就会把自己通过事件函数传出来
        //当Playermodel执行SaveData()函数，调用PlayerModel.UpdateInfo(),由于在这里把函数UpdateRoleInfo添加（注册）到了PlayerModel的updaeEvent事件里去了
        //所以当执行事件的时候，自然的跳转到RoleController来执行UpdateRoleInfo,因此就可以在UpdateRoleInfo里执行信息更新就行了
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
    //点击升级按钮
    private void ClickLevUpBtn()
    {
        //通过数据模块 进行升级 达到数据改变
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
