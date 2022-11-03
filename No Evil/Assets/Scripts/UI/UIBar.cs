using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMax(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetValue(float value)
    {
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (slider.value <= 0)
        {
            SceneManager.LoadScene("Terarraium");
        }
    }
    
    public void isVisible(bool visible)
    {
        if (visible)
        {
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
        else
        {
            fill.color = new Color(0f, 0f, 0f, 0f);
        }
        
    }
}