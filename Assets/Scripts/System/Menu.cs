using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadSceneRaw(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void EditTime(int num)
    {
        Time.timeScale = num;
    }
}
