using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private PlayerBehavior player;



    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<PlayerBehavior>();

        Time.timeScale = 1;

        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(5f);

        EnemyController.SpawnNextWave();
    }

    public static PlayerBehavior GetPlayer()
    {   
        if (instance.player == null) instance.player = FindObjectOfType<PlayerBehavior>();
        return instance.player;
    }

    public static void GameOver()
    {
        Time.timeScale = 0;

        UIController.ShowGameOverScreen();
    }

    public static void Retry()
    {
        SceneManager.LoadScene(0);
    }
}