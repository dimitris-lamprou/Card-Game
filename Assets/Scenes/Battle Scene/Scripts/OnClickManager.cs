using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnClickManager : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private TMP_Text herosHpText;
    [SerializeField] private TMP_Text herosDefenceText;
    [SerializeField] private TMP_Text herosAttackText;
    [SerializeField] private TMP_Text herosStatusEffectsText;
    [SerializeField] private TMP_Text herosStaminaText;
    [Space]
    [Header("Enemy")]
    [SerializeField] private TMP_Text enemysHpText;
    [SerializeField] private TMP_Text enemysDefenceText;
    [SerializeField] private TMP_Text enemysAttackText;
    [SerializeField] private TMP_Text enemysStatusEffectsText;
    [Space]
    [Header("Labels")]
    [SerializeField] private TMP_Text discardText;

    private readonly List<Card> deck = Dealer.deck;
    private readonly List<Card> discard = Dealer.discard;
    private readonly List<Card> hand = Dealer.hand;

    private readonly int handLimit = Hero.handLimit;

    public void EndTurn()
    {
        //Stops end turn in case Hero has attack

        /*if (Hero.attack > 0)
        {
            Debug.Log("Hero has to Attack");
            return;
        }*/

        //Heros attack

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
            if (MapManager.isFromMap)
            {
                SceneManager.LoadScene(2);
            }
        }

        Hero.attack = 0;
        herosAttackText.text = Hero.attack.ToString();*/

        //Enemy Reset def

        /*if (Enemy.attack > 0)
        {
            Enemy.attack = 0;
            enemysAttackText.text = Enemy.attack.ToString();
        }*/
        if (Enemy.defence > 0)
        {
            Enemy.defence = 0;
            enemysDefenceText.text = Enemy.defence.ToString();
        }

        //Enemys action

        if (Enemy.isStuned)
        {
            //Debug.Log("Enemy is stuned");
        }
        else if (MapManager.stageIndex == 2)
        {
            EnemysAI.EnemyB(herosDefenceText, herosHpText, enemysDefenceText, enemysHpText);
        }
        else
        {
            EnemysAI.EnemyA(discard, herosDefenceText, herosHpText, enemysDefenceText);
        }

        if (Enemy.attack > 0)
        {
            EnemysAI.EnemyDealDamage(herosDefenceText, herosHpText);
        }

        //  FOR DEMO MAP 1 without stun card
        /*if (CollideWithEnemy.enemysName.Equals("Enemy A"))
        {
            EnemysAI.EnemyA(discard, herosDefenceText, herosHpText, enemysDefenceText);
        }
        else if (CollideWithEnemy.enemysName.Equals("Enemy B"))
        {
            EnemysAI.EnemyB(discard, herosDefenceText, herosHpText, enemysDefenceText, enemysHpText);
        }*/

        //Deal Cards
        if (hand.Count > 0)
        {
            discard.AddRange(hand);
            hand.Clear();
            var listOfUnusedCards = GameObject.FindGameObjectsWithTag("Card");
            foreach (var card in listOfUnusedCards)
            {
                Destroy(card);
            }
            // IF I WANT TO KEEP UNPLAYED CARDS IN MY HAND
            /*if (deck.Count + hand.Count < handLimit)
            {
                deck.AddRange(discard);
                discard.Clear();
                Dealer.Shuffle(deck);
            }
            hand.Reverse();
            deck.AddRange(hand);
            hand.Clear();

            var listOfUnusedCards = GameObject.FindGameObjectsWithTag("Card");
            foreach (var card in listOfUnusedCards)
            {
                Destroy(card);
            }
            Dealer.Deal(deck);*/
        }
        if (deck.Count < handLimit)     //else if for the above comment section to work
        {
            deck.AddRange(discard);
            discard.Clear();
            Dealer.Shuffle(deck);
            Dealer.Deal(deck);
        }
        else
        {
            Dealer.Deal(deck);
        }

        //Reset
        Hero.defence = 0;
        Hero.stamina = 3;
        Hero.attack = 0;
        Enemy.action = Random.Range(0, 5);
        Enemy.isStuned = false;
        Enemy.isEnraged = false;
        Enemy.attack = 0;
        //Enemy.defence = 0;
        Dealer.WhatEnemyWillDo();

        herosDefenceText.text = Hero.defence.ToString();
        herosStatusEffectsText.text = "";
        herosStaminaText.text = Hero.stamina.ToString();
        herosAttackText.text = Hero.attack.ToString();
        enemysDefenceText.text = Enemy.defence.ToString();
        enemysStatusEffectsText.text = "";
        enemysAttackText.text = Enemy.attack.ToString();
        discardText.text = discard.Count.ToString();
    }
}