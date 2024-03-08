using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mono.Data.Sqlite;

public class Dealer : MonoBehaviour
{
    private static TMP_Text enemysActionText;

    public static GameObject cardPrefab;
    public static Canvas canvas;
    public static TMP_Text deckCountText;
    public static TMP_Text discardText;

    [HideInInspector] public static List<Card> deck = new();
    [HideInInspector] public static List<Card> hand = new();
    [HideInInspector] public static List<Card> discard = new();
    [HideInInspector] public static List<Card> graveyard = new();

    private static readonly SqliteConnection db = DBContext.db;
    private static readonly SqliteCommand cmd = DBContext.cmd;

    // Start is called before the first frame update
    void Start()
    {
        cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        deckCountText = GameObject.FindWithTag("Deck Text").GetComponent<TMP_Text>();
        discardText = GameObject.FindWithTag("Discard Text").GetComponent<TMP_Text>();
        enemysActionText = GameObject.FindWithTag("Action Text").GetComponent<TMP_Text>();

        Init(deck);
        Shuffle(deck);
        Deal(deck);
        WhatEnemyWillDo();
    }

    public static void Deal(List<Card> deck)
    {
        int lastCard;

        int limit;
        if (deck.Count < Hero.handLimit)
        {
            limit = deck.Count;
        }
        else
        {
            limit = Hero.handLimit;
        }

        for (int i = 0; i < limit; i++)
        {
            lastCard = deck.Count - 1;
            deck[lastCard].Description = deck[lastCard].Description.Replace("Attack", "<sprite name=Attack>");
            deck[lastCard].Description = deck[lastCard].Description.Replace("Defence", "<sprite name=Defence>");
            deck[lastCard].Description = deck[lastCard].Description.Replace("Heal", "<sprite name=Heal>");
            deck[lastCard].Description = deck[lastCard].Description.Replace("Enrage", "<sprite name=Enrage>");
            deck[lastCard].Description = deck[lastCard].Description.Replace("Stun", "<sprite name=Stun>");
            deck[lastCard].Description = deck[lastCard].Description.Replace("Reckless", "<sprite name=Reckless>");

            GameObject card = Instantiate(cardPrefab);
            card.transform.parent = canvas.transform;
            card.transform.localPosition = new Vector3(-1602 + (216 * (i + 1)), -1030, 0);

            card.transform.Find("Play Card (Button)/Title (Text)").GetComponent<TMP_Text>().text = deck[lastCard].Title;
            card.transform.Find("Play Card (Button)/Description (Text)").GetComponent<TMP_Text>().text = deck[lastCard].Description;
            card.transform.Find("Sacrifice (Button)/Experience (Text)").GetComponent<TMP_Text>().text = 
                deck[lastCard].Experience.ToString();
            card.name = deck[lastCard].Effect;

            if (deck[lastCard].Title.Equals("Dazed") || deck[lastCard].Title.Equals("Focus"))
            {
                Destroy(card.transform.Find("Sacrifice (Button)").gameObject);
            }

            hand.Add(deck[lastCard]);
            deck.RemoveAt(lastCard);
        }
        deckCountText.text = deck.Count.ToString();
    }

    public static void Shuffle(List<Card> deck)
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (deck[n], deck[k]) = (deck[k], deck[n]);
        }
    }

    public static void Init(List<Card> deck)
    {
        db.Open();
        cmd.CommandText = "Select * from Card";
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader["title"].ToString().Equals("Strike") || reader["title"].ToString().Equals("Defence"))
            {
                for (int i = 0; i < 4; i++)
                {
                    Card card = new()
                    {
                        Id = int.TryParse(reader["id"].ToString(), out int id) ? id : (int?)null,
                        Title = reader["title"].ToString(),
                        Description = reader["description"].ToString(),
                        Effect = reader["effect"].ToString(),
                        Experience = int.TryParse(reader["experience"].ToString(), out int experience) ? experience : (int?)null,
                    };
                    deck.Add(card);
                }
            }
            else if (reader["title"].ToString().Equals("Dazed"))
            {
                //pass this card
            }
            else
            {
                Card card = new()
                {
                    Id = int.TryParse(reader["id"].ToString(), out int id) ? id : (int?)null,
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    Effect = reader["effect"].ToString(),
                    Experience = int.TryParse(reader["experience"].ToString(), out int experience) ? experience : (int?)null,
                };
                deck.Add(card);
            }
        }
        db.Close();
        reader.Close();
    }

    public static void WhatEnemyWillDo()
    {
        if (Enemy.action == 0)
        {
            enemysActionText.text = "Enemy will deal 5 dmg";
        }
        else if (Enemy.action == 1)
        {
            enemysActionText.text = "Enemy will add 5 defence";
        }
        else if (Enemy.action == 2)
        {
            enemysActionText.text = "Enemy will deal 3 dmg and add 2 defence";
        }
        else if (Enemy.action == 3)
        {
            enemysActionText.text = "Enemy will add Dazed to your deck";
        }
        else
        {
            enemysActionText.text = "Enemy is confused and will not do anything";
        }

        //  FOR DEMO MAP 1
        /*if (CollideWithEnemy.enemysName.Equals("Enemy A"))
        {
            if (Enemy.action == 0)
            {
                Debug.Log("Enemy will deal 5 dmg");
            }
            else if (Enemy.action == 1)
            {
                Debug.Log("Enemy will add 5 defence");
            }
            else if (Enemy.action == 2)
            {
                Debug.Log("Enemy will deal 3 dmg and add 2 defence");
            }
            else if (Enemy.action == 3)
            {
                Debug.Log("Enemy will add Dazed to your deck");
            }
            else
            {
                Debug.Log("Enemy is confused and will not do anything");
            }
        }
        else if (CollideWithEnemy.enemysName.Equals("Enemy B"))
        {
            if (Enemy.action == 0)
            {
                Debug.Log("Enemy will deal 3 dmg");
            }
            else if (Enemy.action == 1)
            {
                Debug.Log("Enemy will add 7 defence");
            }
            else if (Enemy.action == 2)
            {
                Debug.Log("Enemy will add 5 defence");
            }
            else if (Enemy.action == 3)
            {
                Debug.Log("Enemy will heal by 3");
            }
            else
            {
                Debug.Log("Enemy is confused and will not do anything");
            }
        }*/
    }
}