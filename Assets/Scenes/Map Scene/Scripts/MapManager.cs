using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static int stageIndex = 0;
    public static bool isFromMap = false;

    private void Start()
    {
        GameObject.Find("Stage (Button) (" + stageIndex.ToString() + ")").GetComponent<Button>().interactable = true;
    }

    public void GoToBattle()
    {
        isFromMap = true;
        stageIndex++;
        SceneManager.LoadScene(1);
    }
}
