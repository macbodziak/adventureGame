using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    // [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject hudScreen;
    [SerializeField] Text HealthText;
    [SerializeField] Text DebugText;
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
            Debug.Assert(gameOverScreen != null);
        }
    }

    private void Start()
    {
        currentScreen = hudScreen;
    }

    public void UpdateHealth(float value)
    {
        HealthText.text = value.ToString() + " / 1.00";
        healthBar.SetValue(value);
    }

    public void UpdateDebug(string value)
    {
        DebugText.text = value;
    }

    public void OnGameOver()
    {
        if (currentScreen != gameOverScreen)
        {
            StartCoroutine("FadeBetweenScreens", gameOverScreen);
        }
    }

    IEnumerator FadeBetweenScreens(GameObject newScreen)
    {
        CanvasGroup cg;
        float iv;

        if (currentScreen != null)
        {
            cg = currentScreen.GetComponent<CanvasGroup>();
            cg.interactable = false;
            iv = 0.0f;

            while (iv <= 1.0f)
            {
                iv += Time.deltaTime * fadeSpeed;
                cg.alpha = Mathf.Lerp(1.0f, 0.0f, iv);
                yield return null;
            }
            currentScreen.SetActive(false);
        }

        currentScreen = newScreen;
        newScreen.SetActive(true);
        cg = newScreen.GetComponent<CanvasGroup>();
        cg.interactable = false;
        iv = 0.0f;

        while (iv <= 1.0f)
        {
            iv += Time.deltaTime * fadeSpeed;
            cg.alpha = Mathf.Lerp(0.0f, 1.0f, iv);
            yield return null;
        }
        cg.interactable = true;
        yield return null;
    }
}
