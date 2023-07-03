using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    // Start is called before the first frame update
    //1. �ҿؼ�
    public Button btnRole;
    public Button btnSill;

    public TextMeshProUGUI txtLev;
    public TextMeshProUGUI txtName;

    //2. �ṩ�����µ���ط������ⲿ
    public void UpdateViewInfo(PlayerModel data)
    {
        txtLev.text = "LV: " + data.Lev;
        txtName.text = data.PlayerName;
    }
}
