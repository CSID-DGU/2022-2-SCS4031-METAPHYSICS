using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageMagnifierUI : UI_Drag
{

    [SerializeField]
    private Image m_ImageWidget = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Image ImageWidget
    {
        get
        {
            return m_ImageWidget;
        }
    }
}
