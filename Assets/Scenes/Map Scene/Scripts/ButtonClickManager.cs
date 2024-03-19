using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClickManager : MonoBehaviour
{
    private static int stageIndex = 0;

    private void Start()
    {
        if (stageIndex > 0)
        {
            GameObject.Find("Stage (Button) (" + stageIndex.ToString() + ")").GetComponent<Button>().interactable = true;
            GameObject.Find("Stage (Button)").GetComponent<Button>().interactable = false;
        }
    }

    public void GoToBattle()
    {
        stageIndex++;
        Debug.Log("Stage (Button) (" + stageIndex.ToString() + ")");
        SceneManager.LoadScene(1);
    }
}
