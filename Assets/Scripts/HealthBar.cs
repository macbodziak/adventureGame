using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    Image image;
    float value;
    float prevValue;
    [SerializeField] float updateSpeed;
    bool updating;
    [SerializeField] Color defaultColor;
    [SerializeField] Color minusColor;
    [SerializeField] Color plusColor;
    Color newColor;
    float updateFinishTime;

    void Start()
    {
        image = GetComponent<Image>();
        Debug.Assert(image != null);
        image.fillAmount = 1.0f;
        value = 1.0f;
        prevValue = 1.0f;
        updating = false;
    }

    public void SetValue(float newValue)
    {
        if (newValue == value)
        {
            return;
        }
        prevValue = value;
        value = newValue;
        updating = true;
        updateFinishTime = Time.time + updateSpeed;
        if (value < prevValue)
        {
            newColor = minusColor;
            image.color = minusColor;
        }
        else
        {
            newColor = plusColor;
        }
    }

    private void Update()
    {
        if (updating)
        {
            float interpolateFactor = 1.0f - (updateFinishTime - Time.time) / updateSpeed;
            image.fillAmount = Mathf.Lerp(prevValue, value, interpolateFactor);
            image.color = Color.Lerp(newColor, defaultColor, interpolateFactor);
            if (interpolateFactor >= 1.0f)
            {
                updating = false;
                image.color = defaultColor;
            }
        }
    }

}
