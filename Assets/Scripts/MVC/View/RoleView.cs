using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleView : MonoBehaviour
{
    // Start is called before the first frame update
    //1. �ҿؼ�
    public Button btnUp;
    public Button btnClose;

    public TextMeshProUGUI txtLev;

    //2. �ṩ�����µ���ط������ⲿ
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
