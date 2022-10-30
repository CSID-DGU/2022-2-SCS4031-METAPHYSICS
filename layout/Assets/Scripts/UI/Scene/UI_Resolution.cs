using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Resolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject CamObj = GameObject.Find("Main Camera");

        Camera Cam = CamObj.GetComponent<Camera>();
        Rect Rec = Cam.rect;

        Image Image = gameObject.GetComponent<Image>();
        Image.transform.localScale = new Vector3(Rec.width, Rec.height, 1.0f);
        Rect ImageREct = Image.rectTransform.rect;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
