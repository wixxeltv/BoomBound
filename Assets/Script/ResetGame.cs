using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void Reset()
    {
        Time.timeScale = 1f;
        GameManager.ResetTimer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}