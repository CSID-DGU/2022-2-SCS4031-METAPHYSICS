using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class MapNavUI : UI_Drag
{
    [SerializeField]
    private Dropdown m_DropDown = null;


    [SerializeField]
    private int m_SelectIndex = -1;


    // Start is called before the first frame update
    void Start()
    {
        m_DropDown.onValueChanged.AddListener(delegate { DropDownCallback(m_DropDown.value); });

    }

    // Update is called once per frame
    void Update()
    {
    }

    void DropDownCallback(int Option)
    {
        m_SelectIndex = Option;
    }

    public void GoButtonCallback()
    {
    }
}
