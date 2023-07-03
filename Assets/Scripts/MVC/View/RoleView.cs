using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleView : MonoBehaviour
{
    // Start is called before the first frame update
    //1. 找控件
    public Button btnUp;
    public Button btnClose;

    public TextMeshProUGUI txtLev;

    //2. 提供面板更新的相关方法给外部
    public void UpdateViewInfo(PlayerModel data)
    {
        txtLev.text = "LV: " + data.Lev;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
