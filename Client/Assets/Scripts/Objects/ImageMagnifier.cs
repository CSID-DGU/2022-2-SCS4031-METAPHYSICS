using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMagnifier : MousePickCallbackObj
{
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public override void MouseClickCallback()
    {
        base.MouseClickCallback();

        SpriteRenderer Renderer = gameObject.GetComponent<SpriteRenderer>();
        Sprite SprData = Renderer.sprite;

        GameObject MagnifierObj = Resources.Load<GameObject>("Prefabs\\UI\\ImagePopUp\\ImageMagnifier");

        GameObject InstanceObj = Instantiate(MagnifierObj);

        ImageMagnifierUI WidgetComponent = InstanceObj.GetComponent<ImageMagnifierUI>();
        WidgetComponent.ImageWidget.sprite = SprData;
    }
}
