using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    public Slider Slider;
    public Color low;
    public Color high;
    public Vector3 offset;

   

    public void SetHealth (float barHealth, float maxHealth)
    {
        Slider.gameObject.SetActive(barHealth < maxHealth);
        Slider.value = barHealth;
        Slider.maxValue= maxHealth;

        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, Slider.normalizedValue);
    }
    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
