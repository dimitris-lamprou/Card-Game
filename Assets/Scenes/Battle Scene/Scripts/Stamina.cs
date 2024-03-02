using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private TMP_Text staminaText;

    // Update is called once per frame
    void Update()
    {
        if (Hero.stamina == 0)
        {
            Debug.Log("Stamina = 0");
            var listOfUnusedCards = GameObject.FindGameObjectsWithTag("Card");
            foreach (var card in listOfUnusedCards)
            {
                card.transform.Find("Play Card (Button)").GetComponent<Button>().interactable = false;
                card.transform.Find("Sacrifice (Button)").GetComponent<Button>().interactable = false;
            }
        }
    }
}
