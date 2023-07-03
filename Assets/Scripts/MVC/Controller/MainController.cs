using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    //能够在Controller中得到界面才行
    private MainView mainView;

    private static MainController controller = null;

    public static MainController Controller
    {
        get
        {
            return controller;
        }
    }

    // 1、界面的显隐
    public static void ShowMe()
    {
        if(controller == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/MainPanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象为canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);

            controller = obj.GetComponent<MainController>();
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
        if(controller != null)
        {
            controller.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //获取同样挂载在一个对象上的 view脚本
        mainView = this.GetComponent<MainView>();
        //第一次的 界面更新
        mainView.UpdateViewInfo(PlayerModel.Data);

        // 2、界面 事件的监听 处理对应的业务逻辑
        mainView.btnRole.onClick.AddListener(ClickRoleBtn);

        ///重点！！！！
        ///点击升级按钮后，其它的UI的对应数据会进行更新
        ///当更新数据的时候 PlayerModel就会把自己通过事件函数传出来
        //当Playermodel执行SaveData()函数，调用PlayerModel.UpdateInfo(),由于在这里把函数UpdateMainInfo添加（注册）到了PlayerModel的updaeEvent事件里去了
        //所以当执行事件的时候，自然的跳转到MainController来执行UpdateMainInfo,因此就可以在UpdateMainInfo里执行信息更新就行了
        PlayerModel.Data.AddEvent(UpdateMainInfo);

    }
    private void ClickRoleBtn()
    {
        Debug.Log("点击按钮显示角色面板");
        //通过controller去显示角色面板
        RoleController.ShowMe();
    }

    // 3、界面的更新
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
