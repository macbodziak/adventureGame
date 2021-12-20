using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    // [SerializeField] GameObject mainMenuScreen;
    // [SerializeField] GameObject gameOverScreen;
    // [SerializeField] GameObject gameScreen;
    [SerializeField]  Text HealthText;
    [SerializeField]  Text DebugText;
    [SerializeField] float fadeSpeed;
    [SerializeField] HealthBar healthBar;
    private static UIMenuManager _instance;
    GameObject currentScreen;

    public static UIMenuManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void UpdateHealth(float value)
    {
        HealthText.text = value.ToString() + " / 1.00";
        healthBar.SetValue(value);
    }
}
