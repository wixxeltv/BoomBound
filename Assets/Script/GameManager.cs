using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager Instance { set; get; }
    public TMP_Text timerText;
    public TMP_Text messageText;
    public GameObject endGameButton;
    private bool isGameOver;
    public static float timer;
    void Awake()
    {
        timer = 0;
        if (Instance && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        isGameOver = false;
    }
    
    void Update()
    {
        if (!isGameOver)
        {
            timer += Time.deltaTime;
            timerText.text = "Timer : " + Mathf.Ceil(timer).ToString("F0");
            
        }

    }

    public static void ResetTimer()
    {
        timer = 0;
    }
    public static void StopGame()
    {
        if (Instance != null && !Instance.isGameOver)
        {
            Instance.isGameOver = true;

            string message = "GG there's your final timer : " + Mathf.Ceil(timer);

            if (Instance.messageText != null)
            {
                Instance.messageText.text = message;
            }

            if (Instance.endGameButton != null)
            {
                Instance.endGameButton.SetActive(true);
            }
            Time.timeScale = 0f;
        }
    }
}