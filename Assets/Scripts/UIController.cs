using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private Text livesText;
    [SerializeField] private GameObject gameOverScreen;



    private void Awake()
    {
        instance = this;

        gameOverScreen.SetActive(false);
    }

    public static void UpdateLives(float value)
    {
        instance.livesText.text = $"x{value}";
    }

    public static void ShowGameOverScreen()
    {
        instance.gameOverScreen.SetActive(true);
    }

    public void OnRetryButtonClicked()
    {
        GameController.Retry();
    }
}