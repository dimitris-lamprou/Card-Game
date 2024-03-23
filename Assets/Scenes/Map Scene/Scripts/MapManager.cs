using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static int stageIndex = 0;
    public static bool isFromMap = false;

    private void Start()
    {
        GameObject buttonGameObject = GameObject.Find("Stage (Button) (" + stageIndex.ToString() + ")");
        buttonGameObject.GetComponent<Image>().enabled = true;
        Button button = buttonGameObject.GetComponent<Button>();
        button.enabled = true;
        if (buttonGameObject.name.Contains("5"))
        {
            button.interactable = true;
        }
    }

    public void GoToBattle()
    {
        isFromMap = true;
        stageIndex++;
        SceneManager.LoadScene(1);
    }
}
