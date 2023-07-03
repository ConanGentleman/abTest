//using System;
//using System.Collections.Generic;
//using UnityEngine;



////热更项目的事件管理器
//public class EventDispatcher
//{
//    /*
//    //热更项目的 事件管理器

//    //方法一 无须拆箱，性能有优势,但是需要手动增加新增事件
//         EventDispatcher.instance.playerAttackEvent += onplayerAttackEvent;
//         var t = new PlayerController();
//         t.job = Job.Zhuyou;
//        EventDispatcher.instance.playerAttackEvent.Invoke(t, t);

//        //方法二 代码管理简单，但是需要拆箱，影响性能
//        int playerAttackEvent = 256;
//        EventDispatcher.instance.Regist(playerAttackEvent,this.onplayerAttackEvent);
//    */
//    public System.Action clearFunc;
//    private static EventDispatcher m_instance;
//    public static EventDispatcher instance
//    {
//        get
//        {
//            if (EventDispatcher.m_instance == null)
//            {
//                EventDispatcher.m_instance = new EventDispatcher();
//                HotFixMgr.instance.appdomain.DelegateManager.RegisterMethodDelegate<object>();
//                HotFixMgr.instance.appdomain.DelegateManager.RegisterMethodDelegate<object, object>();
//            }
//            return EventDispatcher.m_instance;
//        }
//    }

//    private Dictionary<int, Action> listeners0 = new Dictionary<int, Action>();
//    private Dictionary<int, Action<object>> listeners1 = new Dictionary<int, Action<object>>();
//    private Dictionary<int, Action<object, object>> listeners2 = new Dictionary<int, Action<object, object>>();

//    public void Regist(int evt, Action act)
//    {
//        Action value;
//        if (!this.listeners0.TryGetValue(evt, out value))
//        {
//            this.listeners0.Add(evt, act);
//            return;
//        }
//        Dictionary<int, Action> dictionary = this.listeners0;
//        dictionary[evt] = (Action)Delegate.Combine(dictionary[evt], value);
//    }
//    public void Regist(int evt, Action<object> act)
//    {
//        Action<object> value;
//        if (!this.listeners1.TryGetValue(evt, out value))
//        {
//            //Debug.Log("注册" + act);
//            this.listeners1.Add(evt, act);
//            return;
//        }
//        Dictionary<int, Action<object>> dictionary = this.listeners1;
//        dictionary[evt] = (Action<object>)Delegate.Combine(dictionary[evt], value);
//    }
//    public void Regist(int evt, Action<object, object> act)
//    {
//        Action<object, object> value;
//        if (!this.listeners2.TryGetValue(evt, out value))
//        {
//            this.listeners2.Add(evt, act);
//            return;
//        }
//        Dictionary<int, Action<object, object>> dictionary = this.listeners2;
//        dictionary[evt] = (Action<object, object>)Delegate.Combine(dictionary[evt], value);
//    }

//    public Action<PlayerController, PlayerController> playerAttackEvent;

//    public void ClearEventListener()
//    {
//        this.listeners0.Clear();
//        this.listeners1.Clear();
//        this.listeners2.Clear();

//        if (clearFunc != null) clearFunc.Invoke();

//    }









//    public void DispatchEvent(int evt)
//    {
//        try
//        {
//            Action func;
//            if (this.listeners0.TryGetValue(evt, out func) && func != null)
//            {
//                func();
//            }
//        }
//        catch (Exception ex)
//        {
//            Debug.LogError(ex.Message);
//            Debug.LogError(ex.StackTrace);
//        }
//    }





//    public void DispatchEvent(int evt, object obj)
//    {
//        try
//        {
//            Action<object> func;
//            if (this.listeners1.TryGetValue(evt, out func))
//            {
//                if (func != null)
//                {
//                    func(obj);
//                }
//                else
//                {
//                    Debug.LogError("func null");
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Debug.LogError(ex.Message);
//            Debug.LogError(ex.StackTrace);
//        }
//    }





//    public void DispatchEvent<T, K>(int evt, object obj, object obj2)
//    {
//        try
//        {
//            Action<object, object> func;
//            if (this.listeners2.TryGetValue(evt, out func) && func != null)
//            {
//                func((T)((object)obj), (K)((object)obj2));
//            }
//        }
//        catch (Exception ex)
//        {
//            Debug.LogError(ex.Message);
//            Debug.LogError(ex.StackTrace);
//        }
//    }



//}
