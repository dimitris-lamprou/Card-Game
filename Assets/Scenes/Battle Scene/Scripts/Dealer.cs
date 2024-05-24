using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mono.Data.Sqlite;

public class Dealer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer enemysImage;
    [HideInInspector] public static List<Card> deck = new();
    [HideInInspector] public static List<Card> hand = new();
    [HideInInspector] public static List<Card> discard = new();
    [HideInInspector] public static List<Card> graveyard = new();

    public static TMP_Text herosHpText;
    public static TMP_Text herosStatusEffectsText;
    public static TMP_Text herosDefenceText;
    public static TMP_Text herosStaminaText;
    public static TMP_Text herosAttackText;
    public static TMP_Text enemysHpText;
    public static TMP_Text enemysDefenceText;
    public static TMP_Text enemysThoughtText;
    public static TMP_Text enemysStatusEffectsText;
    public static TMP_Text enemysAttackText;
    public static TMP_Text discardText;
    public static TMP_Text graveyardText;
    public static GameObject cardPrefab;
    public static Canvas canvas;
    public static TMP_Text deckCountText;
    public static TMP_Text enemysActionText;
    public static Card dazed;

    private static readonly SqliteConnection db = DBContext.db;
    private static readonly SqliteCommand cmd = DBContext.cmd;

    private static bool isTheFirstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        deckCountText = GameObject.FindWithTag("Deck Text").GetComponent<TMP_Text>();
        discardText = GameObject.FindWithTag("Discard Text").GetComponent<TMP_Text>();
        enemysActionText = GameObject.FindWithTag("Enemys Thought Text").GetComponent<TMP_Text>();
        enemysHpText = GameObject.FindWithTag("Enemys Health Text").GetComponent<TMP_Text>();
        enemysDefenceText = GameObject.FindWithTag("Enemys Defence").GetComponent<TMP_Text>();
        herosDefenceText = GameObject.FindWithTag("Heros Defence").GetComponent<TMP_Text>();
        herosHpText = GameObject.FindWithTag("Heros Hp").GetComponent<TMP_Text>();
        graveyardText = GameObject.FindWithTag("Graveyard Text").GetComponent<TMP_Text>();
        herosStaminaText = GameObject.FindWithTag("Stamina Text").GetComponent<TMP_Text>();
        herosAttackText = GameObject.FindWithTag("Attack Text").GetComponent<TMP_Text>();
        enemysThoughtText = GameObject.FindWithTag("Enemys Thought Text").GetComponent<TMP_Text>();
        herosStatusEffectsText = GameObject.FindWithTag("Heros Status Effects Text").GetComponent<TMP_Text>();
        enemysStatusEffectsText = GameObject.FindWithTag("Enemys Status Effects Text").GetComponent<TMP_Text>();
        enemysAttackText = GameObject.FindWithTag("Enemys Attack Text").GetComponent<TMP_Text>();

        if (isTheFirstTime)
        {
            Init(deck);
        }

        deck.Clear();
        deck.AddRange(Hero.deck);

        deckCountText.text = deck.Count.ToString();
        discardText.text = discard.Count.ToString();

        Shuffle(deck);
        Deal(deck);
        Enemy.WhatWillDo();

        herosHpText.text = Hero.hp.ToString();
        herosStaminaText.text = Hero.stamina.ToString();
        herosDefenceText.text = Hero.defence.ToString();
        herosAttackText.text = Hero.attack.ToString();

        if (MapManager.isFromMap)
        {
            enemysImage.sprite = Resources.Load<Sprite>("Enemies/" + MapManager.stageIndex.ToString());
        }

        if (StatusEffects.heroStunRounds > 0)
        {
            herosStatusEffectsText.text += StatusEffects.stunIcon;
        }
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

            GameObject card = Instantiate(cardPrefab);
            card.transform.parent = canvas.transform;
            card.transform.localPosition = new Vector3(-1505 + (180 * (i + 1)), -1030, 0);
            card.transform.Find("Play Card (Button)/Title (Text)").GetComponent<TMP_Text>().text = deck[lastCard].Title;
            card.transform.Find("Play Card (Button)/Description (Text)").GetComponent<TMP_Text>().text = deck[lastCard].Description;
            card.transform.Find("Sacrifice (Button)/Scales (Text)").GetComponent<TMP_Text>().text = 
                deck[lastCard].Sacrifice.ToString();
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
        bool deckLimit;
        int j = 5;
        db.Open();
        cmd.CommandText = "Select * from Card";
        var reader = cmd.ExecuteReader();

        if (MapManager.isFromMap)
        {
            deckLimit = j < 10;
        }
        else
        {
            deckLimit = true;
        }

        while (reader.Read() && deckLimit)  //for 10 cards i < 10 or reader.Read() for all cards
        {
            if (reader["title"].ToString().Equals("Strike") || reader["title"].ToString().Equals("Block"))
            {
                for (int i = 0; i < 4; i++)
                {
                    Card card = new()
                    {
                        Title = reader["title"].ToString(),
                        Description = reader["description"].ToString(),
                        Effect = reader["effect"].ToString(),
                        Sacrifice = int.TryParse(reader["sacrifice"].ToString(), out int sacrifice) ? sacrifice : (int?)null,
                    };
                    deck.Add(card);
                }
            }
            else if (reader["title"].ToString().Equals("Dazed"))
            {
                dazed = new()
                {
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    Effect = reader["effect"].ToString(),
                    Sacrifice = int.TryParse(reader["sacrifice"].ToString(), out int sacrifice) ? sacrifice : (int?)null,
                };
            }
            else
            {
                Card card = new()
                {
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    Effect = reader["effect"].ToString(),
                    Sacrifice = int.TryParse(reader["sacrifice"].ToString(), out int sacrifice) ? sacrifice : (int?)null,
                };
                deck.Add(card);
            }
            j++;
            if (MapManager.isFromMap)
            {
                deckLimit = j < 10;
            }
        }
        db.Close();
        reader.Close();
        Hero.deck.AddRange(deck);
        isTheFirstTime = false;
    }

    
}