using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeButton : MonoBehaviour
{

    // private Color32 selectedColor;

    public void ChangeCarColor()
    {
        // selectedColor = this.GetComponent<Image>().color;
        CarPaintController.instance.PaintCar(this.GetComponent<Image>().color);
    }

    public void ChangeRimColor()
    {
        // selectedColor = 
        CarPaintController.instance.PaintRim(this.GetComponent<Image>().color);
    }
}
