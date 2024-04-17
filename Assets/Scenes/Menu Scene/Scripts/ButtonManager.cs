using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
