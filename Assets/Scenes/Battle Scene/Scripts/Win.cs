using TMPro;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [Header("1st Card")]
    [SerializeField] private TMP_Text title1Text;
    [SerializeField] private TMP_Text description1Text;
    [SerializeField] private TMP_Text exp1Text;
    [Space]
    [Header("2nd Card")]
    [SerializeField] private TMP_Text title2Text;
    [SerializeField] private TMP_Text description2Text;
    [SerializeField] private TMP_Text exp2Text;
    [Space]
    [Header("3rd Card")]
    [SerializeField] private TMP_Text title3Text;
    [SerializeField] private TMP_Text description3Text;
    [SerializeField] private TMP_Text exp3Text;

    private static readonly SqliteConnection db = DBContext.db;
    private static readonly SqliteCommand cmd = DBContext.cmd;

    private readonly List<Card> tempDeck = new();
    private readonly List<Card> deck = new();

    private void OnEnable()
    {
        db.Open();
        cmd.CommandText = "Select * from Card Where Id >= 6 and Id <= 13";
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Card card = new()
            {
                Title = reader["title"].ToString(),
                Description = reader["description"].ToString(),
                Effect = reader["effect"].ToString(),
                Experience = int.TryParse(reader["experience"].ToString(), out int experience) ? experience : (int?)null,
            };
            tempDeck.Add(card);
        }
        db.Close();
        reader.Close();
        deck.AddRange(tempDeck);

        TMP_Text[] titles = { title1Text, title2Text, title3Text };
        TMP_Text[] descriptions = {description1Text, description2Text, description3Text };
        TMP_Text[] exps = {exp1Text, exp2Text, exp3Text };

        int tempRandVal = Random.Range(0, tempDeck.Count);

        for (int i = 0; i < 3; i++)
        {
            titles[i].text = tempDeck[tempRandVal].Title;
            descriptions[i].text = tempDeck[tempRandVal].Description;
            exps[i].text = tempDeck[tempRandVal].Experience.ToString();

            tempDeck.RemoveAt(tempRandVal);
            tempRandVal = Random.Range(0, tempDeck.Count);
        }
    }

    public void TakeCard()
    {
        GameObject selectedCard = EventSystem.current.currentSelectedGameObject;
        string selectedCardsTitle = selectedCard.transform.Find("Title (Text)").GetComponent<TMP_Text>().text;
        Card newCard = deck.Find(card => card.Title == selectedCardsTitle);
        Hero.deck.Add(newCard);

        Dealer.discard.Clear();
        Dealer.hand.Clear();

        SceneManager.LoadScene(1);
    }
}
