using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefabOnClick : MonoBehaviour
{
    [SerializeField] private GameObject card;
    [SerializeField] private TMP_Text title;

    private TMP_Text herosHpText;
    private TMP_Text herosStatusEffectsText;
    private TMP_Text herosDefenceText;
    private TMP_Text herosStaminaText;
    private TMP_Text herosAttackText;
    private TMP_Text enemysHpText;
    private TMP_Text enemysDefenceText;
    private TMP_Text enemysThoughtText;
    private TMP_Text enemysStatusEffectsText;
    private TMP_Text enemysAttackText;
    private TMP_Text discardText;
    private TMP_Text graveyardText;

    private string effect;

    private void Start()
    {
        enemysHpText = GameObject.FindWithTag("Enemys Health Text").GetComponent<TMP_Text>();
        enemysDefenceText = GameObject.FindWithTag("Enemys Defence").GetComponent<TMP_Text>();
        herosDefenceText = GameObject.FindWithTag("Heros Defence").GetComponent<TMP_Text>();
        herosHpText = GameObject.FindWithTag("Heros Hp").GetComponent<TMP_Text>();
        discardText = GameObject.FindWithTag("Discard Text").GetComponent<TMP_Text>();
        graveyardText = GameObject.FindWithTag("Graveyard Text").GetComponent<TMP_Text>();
        herosStaminaText = GameObject.FindWithTag("Stamina Text").GetComponent<TMP_Text>();
        herosAttackText = GameObject.FindWithTag("Attack Text").GetComponent<TMP_Text>();
        enemysThoughtText = GameObject.FindWithTag("Enemys Thought Text").GetComponent<TMP_Text>();
        herosStatusEffectsText = GameObject.FindWithTag("Heros Status Effects Text").GetComponent<TMP_Text>();
        enemysStatusEffectsText = GameObject.FindWithTag("Enemys Status Effects Text").GetComponent<TMP_Text>();
        enemysAttackText = GameObject.FindWithTag("Enemys Attack Text").GetComponent<TMP_Text>();

        effect = card.name;
    }

    public void DoCardEffect()
    {
        Card playedCard = Dealer.hand.Find(card => card.Title == title.text);

        if (title.text.Equals("Dazed"))
        {
            Dealer.hand.Remove(playedCard);
            Destroy(card);

            //IF I WANT DAZED TO DRAIN 1 STAMINA

            Hero.stamina--;
            herosStaminaText.text = Hero.stamina.ToString();
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
                    herosAttackText.text = Hero.attack.ToString();
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
                    Hero.addExp = int.Parse(effectSplited[i - 1]);
                    break;
                }
            }

            Hero.exp += Hero.addExp;
            Debug.Log("Heros xp = " + Hero.exp);
        }
        if (effect.Contains("Defence"))
        {
            for (int i = 0; i < effectSplited.Length; i++)
            {
                if (effectSplited[i].Equals("Defence"))
                {
                    Hero.addDefence = int.Parse(effectSplited[i - 1]);
                    break;
                }
            }

            Hero.defence += Hero.addDefence;
            herosDefenceText.text = Hero.defence.ToString();
        }
        if (effect.Contains("Heal"))
        {
            for (int i = 0; i < effectSplited.Length; i++)
            {
                if (effectSplited[i].Equals("Heal"))
                {
                    Hero.heal = int.Parse(effectSplited[i - 1]);
                    break;
                }
            }

            for (int i = 1; i <= Hero.heal; i++) //dont overheal
            {
                if (Hero.hp == Hero.hpCap)
                {
                    break;
                }
                Hero.hp++;
            }

            herosHpText.text = Hero.hp.ToString();
        }
        if (effect.Contains("Stun"))
        {
            Enemy.isStuned = true;
            enemysStatusEffectsText.text += "<sprite name=Stun>";
            enemysThoughtText.text = "";
        }
        if (effect.Contains("Reckless"))
        {
            if (Hero.defence > 0)
            {
                Hero.defence--;
                herosDefenceText.text = Hero.defence.ToString();
            }
            herosStatusEffectsText.text += "<sprite name=Reckless>";
        }
        if (effect.Contains("Enrage"))
        {
            Enemy.isEnraged = true;
            if (Enemy.defence > 0)
            {
                Enemy.defence--;
                enemysDefenceText.text = Enemy.defence.ToString();
            }
            Enemy.attack++;
            enemysAttackText.text = Enemy.attack.ToString();
            enemysStatusEffectsText.text += "<sprite name=Enrage>";
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
                enemysHpText.text = Enemy.hp.ToString();
                herosHpText.text = Hero.hp.ToString();
            }
        }

        Dealer.discard.Add(playedCard);
        Dealer.hand.Remove(playedCard);

        Hero.stamina--;

        herosStaminaText.text = Hero.stamina.ToString();
        discardText.text = Dealer.discard.Count.ToString();

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
        herosHpText.text = Hero.hp.ToString();

        Dealer.graveyard.Add(playedCard);
        Dealer.hand.Remove(playedCard);
        graveyardText.text = Dealer.graveyard.Count.ToString();
        Destroy(card);
    }
}
