using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops the game in the editor
#else
            Application.Quit(); // Quits the game when not in the editor
#endif
    }
}
