using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    // Start is called before the first frame update
    //1. 找控件
    public Button btnRole;
    public Button btnSill;

    public TextMeshProUGUI txtLev;
    public TextMeshProUGUI txtName;

    //2. 提供面板更新的相关方法给外部
    public void UpdateViewInfo(PlayerModel data)
    {
        txtLev.text = "LV: " + data.Lev;
        txtName.text = data.PlayerName;
    }
}
