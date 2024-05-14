using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject card;
    [SerializeField] private TMP_Text title;
    [Space]
    [Header("For Hover")]
    [SerializeField] private int hoverAmount = 50;
    
    private Vector3 initialPosition;

    private string effect;
    private int amount;

    void Start()
    {
        // Save the initial position of the button
        initialPosition = transform.position;
        effect = card.name;
    }

    public void PlayTheCard()
    {
        Card playedCard = Dealer.hand.Find(card => card.Title == title.text);

        if (title.text.Equals("Dazed"))
        {
            Dealer.hand.Remove(playedCard);
            Destroy(card);

            //IF I WANT DAZED TO DRAIN 1 STAMINA

            Hero.stamina--;
            Dealer.herosStaminaText.text = Hero.stamina.ToString();
            if (Hero.stamina == 0)
            {
                var listOfUnusedCards = GameObject.FindGameObjectsWithTag("Card");
                foreach (var card in listOfUnusedCards)
                {
                    card.transform.Find("Play Card (Button)").GetComponent<Button>().interactable = false;
                    if (card.transform.Find("Sacrifice (Button)") != null)
                    {
                        card.transform.Find("Sacrifice (Button)").GetComponent<Button>().interactable = false;
                    }
                }
            }

            return;
        }

        string[] effectSplited = effect.Split(',');

        if (effect.Contains("Attack") && StatusEffects.heroStunRounds == 0)
        {
            for (int i = 0; i < effectSplited.Length; i++)
            {
                if (effectSplited[i].Equals("Attack"))
                {
                    Hero.attack += int.Parse(effectSplited[i - 1]);
                    Dealer.herosAttackText.text = Hero.attack.ToString();
                    break;
                }
            }

            // FOR INSTANT ATTACK

            /*if (Enemy.defence > 0)
            {
                if (Enemy.defence >= Hero.attack)
                {
                    Enemy.defence -= Hero.attack;
                    enemysDefenceText.text = Enemy.defence.ToString();
                }
                else
                {
                    int remainingDamage = Hero.attack - Enemy.defence;
                    Enemy.defence = 0;
                    Enemy.hp -= remainingDamage;
                    enemysDefenceText.text = "0";
                    enemysHpText.text = Enemy.hp.ToString();
                }
            }
            else
            {
                Enemy.hp -= Hero.attack;
                enemysHpText.text = Enemy.hp.ToString();
            }

            if (Enemy.hp <= 0) //if hero destroyed enemy
            {
                //TODO MUST BE A FUNCTION OR CLASS
                //Store Exp
                Debug.Log("Enemy died");
            }*/
        }
        else if (effect.Contains("Experience")) //works if card gives xp only
        {
            for (int i = 0; i < effectSplited.Length; i++)
            {
                if (effectSplited[i].Equals("Experience"))
                {
                    amount = int.Parse(effectSplited[i - 1]);
                    break;
                }
            }

            Hero.AddExp(amount);
            Debug.Log("Heros xp = " + Hero.exp);
        }
        if (effect.Contains("Defence"))
        {
            for (int i = 0; i < effectSplited.Length; i++)
            {
                if (effectSplited[i].Equals("Defence"))
                {
                    amount = int.Parse(effectSplited[i - 1]);
                    break;
                }
            }

            Hero.AddDefence(amount);
            Dealer.herosDefenceText.text = Hero.defence.ToString();
        }
        if (effect.Contains("Heal"))
        {
            int amount = 0;
            for (int i = 0; i < effectSplited.Length; i++)
            {
                if (effectSplited[i].Equals("Heal"))
                {
                    amount = int.Parse(effectSplited[i - 1]);
                    break;
                }
            }

            Hero.Heal(amount);

            Dealer.herosHpText.text = Hero.hp.ToString();
        }
        if (effect.Contains("Stun"))
        {
            Enemy.isStuned = true;
            Dealer.enemysStatusEffectsText.text += "<sprite name=Stun>";
            Dealer.enemysThoughtText.text = "";
        }
        if (effect.Contains("Reckless"))
        {
            if (Hero.defence > 0)
            {
                Hero.defence--;
                Dealer.herosDefenceText.text = Hero.defence.ToString();
            }
            Dealer.herosStatusEffectsText.text += "<sprite name=Reckless>";
        }
        if (effect.Contains("Enrage"))
        {
            Enemy.isEnraged = true;
            if (Enemy.defence > 0)
            {
                Enemy.defence--;
                Dealer.enemysDefenceText.text = Enemy.defence.ToString();
            }
            Enemy.attack++;
            Dealer.enemysAttackText.text = Enemy.attack.ToString();
            Dealer.enemysStatusEffectsText.text += "<sprite name=Enrage>";
        }
        if (effect.Contains("Drain")) //only for test reasons
        {
            if (Enemy.isEnraged)
            {
                Enemy.hp--;
                if (Hero.hp == Hero.hpCap)
                {
                    //dont do anything
                }
                else
                {
                    Hero.hp++;
                }
                Dealer.enemysHpText.text = Enemy.hp.ToString();
                Dealer.herosHpText.text = Hero.hp.ToString();
            }
        }

        Dealer.discard.Add(playedCard);
        Dealer.hand.Remove(playedCard);

        Hero.stamina--;

        Dealer.herosStaminaText.text = Hero.stamina.ToString();
        Dealer.discardText.text = Dealer.discard.Count.ToString();

        Destroy(card);

        if (Hero.stamina == 0)
        {
            var listOfUnusedCards = GameObject.FindGameObjectsWithTag("Card");
            foreach (var card in listOfUnusedCards)
            {
                card.transform.Find("Play Card (Button)").GetComponent<Button>().interactable = false;
                if (card.transform.Find("Sacrifice (Button)") != null)
                {
                    card.transform.Find("Sacrifice (Button)").GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void Sacrifice()
    {
        Card playedCard = Dealer.hand.Find(card => card.Title == title.text);

        if (Hero.handLimit == 1)
        {
            Debug.Log("You cant sacrifice another card");
            return;
        }

        Hero.exp += int.Parse( //works for one char xp
            card.transform.Find("Sacrifice (Button)/Experience (Text)").GetComponent<TMP_Text>().text[0].ToString());
        Debug.Log("Heros xp = " + Hero.exp);

        Hero.handLimit--;
        Hero.hp--;
        Dealer.herosHpText.text = Hero.hp.ToString();

        Dealer.graveyard.Add(playedCard);
        Dealer.hand.Remove(playedCard);
        Dealer.graveyardText.text = Dealer.graveyard.Count.ToString();
        Destroy(card);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Move the button up when the mouse enters
        transform.position = initialPosition + new Vector3(0, hoverAmount, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return the button to its initial position when the mouse exits
        transform.position = initialPosition;
    }
}
