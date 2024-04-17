using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static int stageIndex = 0;
    public static bool isFromMap = false;

    private void Start()
    {
        for (int i = 0; i <= stageIndex; i++)
        {
            GameObject buttonGameObject = GameObject.Find("Stage (Button) (" + i.ToString() + ")");
            buttonGameObject.GetComponent<Image>().enabled = true;
            Button button = buttonGameObject.GetComponent<Button>();
            button.enabled = true;
            button.interactable = false;
            if (i == stageIndex)
            {
                button.interactable = true;
            }
        }
    }

    public void GoToBattle()
    {
        isFromMap = true;
        stageIndex++;
        SceneManager.LoadScene(2);
    }
}
