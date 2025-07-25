using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Linq;

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
    public static TMP_Text enemysHpText1;
    public static TMP_Text enemysDefenceText1;
    public static TMP_Text enemysThoughtText1;
    public static TMP_Text enemysStatusEffectsText1;
    public static TMP_Text enemysAttackText1;
    public static TMP_Text enemysHpText2;
    public static TMP_Text enemysDefenceText2;
    public static TMP_Text enemysThoughtText2;
    public static TMP_Text enemysStatusEffectsText2;
    public static TMP_Text enemysAttackText2;
    public static TMP_Text discardText;
    public static TMP_Text graveyardText;
    public static GameObject cardPrefab;
    public static Canvas canvas;
    public static TMP_Text deckCountText;
    public static Card dazed;

    public static List<EnemysUiText> enemiesHpText = new();
    public static List<EnemysUiText> enemiesDefenceText = new();
    public static List<EnemysUiText> enemiesThoughtText = new();
    public static List<EnemysUiText> enemiesStatusEffectsText = new();
    public static List<EnemysUiText> enemiesAttackText = new();

    private static readonly SqliteConnection db = DBContext.db;
    private static readonly SqliteCommand cmd = DBContext.cmd;

    private static bool isTheFirstTime = true;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        graveyardText = GameObject.FindWithTag("Graveyard Text").GetComponent<TMP_Text>();
        discardText = GameObject.FindWithTag("Discard Text").GetComponent<TMP_Text>();
        deckCountText = GameObject.FindWithTag("Deck Text").GetComponent<TMP_Text>();

        herosDefenceText = GameObject.FindWithTag("Heros Defence").GetComponent<TMP_Text>();
        herosHpText = GameObject.FindWithTag("Heros Hp").GetComponent<TMP_Text>();
        herosStaminaText = GameObject.FindWithTag("Stamina Text").GetComponent<TMP_Text>();
        herosAttackText = GameObject.FindWithTag("Attack Text").GetComponent<TMP_Text>();
        herosStatusEffectsText = GameObject.FindWithTag("Heros Status Effects Text").GetComponent<TMP_Text>();

        enemiesHpText.AddRange(new[]
        {
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Health Text1").GetComponent<TMP_Text>(),
                name = "Enemys Health Text1"
            },
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Health Text2").GetComponent<TMP_Text>(),
                name = "Enemys Health Text2"
            }
        });

        enemiesDefenceText.AddRange(new[]
        {
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Defence Text1").GetComponent<TMP_Text>(),
                name = "Enemys Defence Text1"
            },
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Defence Text2").GetComponent<TMP_Text>(),
                name = "Enemys Defence Text2"
            }
        });

        enemiesThoughtText.AddRange(new[]
        {
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Thought Text1").GetComponent<TMP_Text>(),
                name = "Enemys Thought Text1"
            },
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Thought Text2").GetComponent<TMP_Text>(),
                name = "Enemys Thought Text2"
            }
        });

        enemiesStatusEffectsText.AddRange(new[]
        {
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Status Effects Text1").GetComponent<TMP_Text>(),
                name = "Enemys Status Effects Text1"
            },
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Status Effects Text2").GetComponent<TMP_Text>(),
                name = "Enemys Status Effects Text2"
            },
        });

        enemiesAttackText.AddRange(new[]
        {
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Attack Text1").GetComponent<TMP_Text>(),
                name = "Enemys Attack Text1"
            },
            new EnemysUiText
            {
                tmp_Text = GameObject.FindWithTag("Enemys Attack Text2").GetComponent<TMP_Text>(),
                name = "Enemys Attack Text2"
            }
        });

        enemysHpText1 = GameObject.FindWithTag("Enemys Health Text1").GetComponent<TMP_Text>();
        enemysDefenceText1 = GameObject.FindWithTag("Enemys Defence Text1").GetComponent<TMP_Text>();
        enemysThoughtText1 = GameObject.FindWithTag("Enemys Thought Text1").GetComponent<TMP_Text>();
        enemysStatusEffectsText1 = GameObject.FindWithTag("Enemys Status Effects Text1").GetComponent<TMP_Text>();
        enemysAttackText1 = GameObject.FindWithTag("Enemys Attack Text1").GetComponent<TMP_Text>();

        enemysHpText2 = GameObject.FindWithTag("Enemys Health Text2").GetComponent<TMP_Text>();
        enemysDefenceText2 = GameObject.FindWithTag("Enemys Defence Text2").GetComponent<TMP_Text>();
        enemysThoughtText2 = GameObject.FindWithTag("Enemys Thought Text2").GetComponent<TMP_Text>();
        enemysStatusEffectsText2 = GameObject.FindWithTag("Enemys Status Effects Text2").GetComponent<TMP_Text>();
        enemysAttackText2 = GameObject.FindWithTag("Enemys Attack Text2").GetComponent<TMP_Text>();

        if (isTheFirstTime)
        {
            Init(deck);
            Enemy.InitEnemies();
        }

        enemy = Enemy.enemies.FirstOrDefault(e => e.position == 1);

        deck.Clear();
        deck.AddRange(Hero.deck);

        deckCountText.text = deck.Count.ToString();
        discardText.text = discard.Count.ToString();

        Shuffle(deck);
        Deal(deck);

        enemy.PrepareMove();
        enemy.WhatWillDo(enemysThoughtText1);

        herosHpText.text = Hero.hp.ToString();
        herosStaminaText.text = Hero.stamina.ToString();
        herosDefenceText.text = Hero.defence.ToString();
        herosAttackText.text = Hero.attack.ToString();

        /*enemysHpText1.text = enemy.hp.ToString();               //print all enemies hp fix
        enemysHpText2.text = Enemy.darkImp.hp.ToString();*/
        foreach (var enemyHpText in enemiesHpText)
        {
            enemyHpText.tmp_Text.text = Enemy.enemies.FirstOrDefault(e => enemyHpText.name.Contains(e.position.ToString())).
                hp.ToString();
        }

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
            card.transform.localPosition = new Vector3(-1500 + (185 * (i + 1)), -1015, 0);
            card.transform.Find("Play Card (Button)/Title (Text)").GetComponent<TMP_Text>().text = deck[lastCard].Title;
            card.transform.Find("Play Card (Button)/Description (Text)").GetComponent<TMP_Text>().text = deck[lastCard].Description;
            card.transform.Find("Play Card (Button)/Sacrifice (Button)/Scales (Text)").GetComponent<TMP_Text>().text = 
                deck[lastCard].Sacrifice.ToString();
            card.transform.Find("Play Card (Button)/Stamina Cost (Image)/Text").GetComponent<TMP_Text>().text = deck[lastCard].StaminaCost.ToString();
            card.name = deck[lastCard].Effect;

            if (deck[lastCard].Title.Equals("Dazed") || deck[lastCard].Title.Equals("Focus"))
            {
                Destroy(card.transform.Find("Play Card (Button)/Sacrifice (Button)").gameObject);
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
                        StaminaCost = int.TryParse(reader["stamina cost"].ToString(), out int staminaCost) 
                            ? staminaCost : (int?)null
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
                    StaminaCost = int.TryParse(reader["stamina cost"].ToString(), out int staminaCost)
                            ? staminaCost : (int?)null
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
                    StaminaCost = int.TryParse(reader["stamina cost"].ToString(), out int staminaCost)
                            ? staminaCost : (int?)null
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