using System;
using TMPro;
using UnityEngine;

public class CardPrefabOnClick : MonoBehaviour
{
    [SerializeField] private GameObject card;
    [SerializeField] private TMP_Text title;

    private TMP_Text enemysHpText;
    private TMP_Text enemysBlockText;
    private TMP_Text herosHpText;
    private TMP_Text herosBlockText;
    private TMP_Text discardText;
    private TMP_Text graveyardText;

    private string effect;

    private void Start()
    {
        enemysHpText = GameObject.FindWithTag("Enemys Health Text").GetComponent<TMP_Text>();
        enemysBlockText = GameObject.FindWithTag("Enemys Block Text").GetComponent<TMP_Text>();
        herosBlockText = GameObject.FindWithTag("Heros Block Text").GetComponent<TMP_Text>();
        herosHpText = GameObject.FindWithTag("Heros Hp").GetComponent<TMP_Text>();
        discardText = GameObject.FindWithTag("Discard Text").GetComponent<TMP_Text>();
        graveyardText = GameObject.FindWithTag("Graveyard Text").GetComponent<TMP_Text>();

        effect = card.name;
    }

    public void DoCardEffect()
    {
        Card playedCard = Dealer.hand.Find(card => card.Title == title.text);

        if (title.text.Equals("Dazed"))
        {
            Dealer.hand.Remove(playedCard);
            Destroy(card);
            return;
        }

        /*Hero.attack = int.Parse(effect[0].ToString());
        Hero.addBlock = int.Parse(effect[0].ToString());
        Hero.addExp = int.Parse(effect[0].ToString());*/
        string[] effectSplited = effect.Split(',');

        if (effect.Contains("Attack"))
        {
            for (int i = 0; i < effectSplited.Length; i++)
            {
                if (effectSplited[i].Equals("Attack"))
                {
                    Hero.attack = int.Parse(effectSplited[i - 1]);
                    break;
                }
            }

            if (Enemy.block > 0)
            {
                if (Enemy.block >= Hero.attack)
                {
                    Enemy.block -= Hero.attack;
                    enemysBlockText.text = Enemy.block.ToString();
                }
                else
                {
                    int remainingDamage = Hero.attack - Enemy.block;
                    Enemy.block = 0;
                    Enemy.hp -= remainingDamage;
                    enemysBlockText.text = "0";
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
            }
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
                    Hero.addBlock = int.Parse(effectSplited[i - 1]);
                    break;
                }
            }

            Hero.block += Hero.addBlock;
            herosBlockText.text = Hero.block.ToString();
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

            for (int i = 1; i <= Hero.heal; i++)
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
        }
        if (effect.Contains("Reckless"))
        {
            if (Hero.block > 0)
            {
                Hero.block--;
                herosBlockText.text = Hero.block.ToString();
            }
        }

        Dealer.discard.Add(playedCard);
        Dealer.hand.Remove(playedCard);
        discardText.text = Dealer.discard.Count.ToString();
        Destroy(card);
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
