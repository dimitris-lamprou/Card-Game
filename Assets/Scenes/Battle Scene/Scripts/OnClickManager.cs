using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnClickManager : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private TMP_Text herosHpText;
    [SerializeField] private TMP_Text herosBlockText;
    [SerializeField] private TMP_Text staminaText;
    [SerializeField] private TMP_Text attackText;
    [Space]
    [Header("Enemy")]
    [SerializeField] private TMP_Text enemysBlockText;
    [SerializeField] private TMP_Text enemysHpText;
    [Space]
    [Header("Labels")]
    [SerializeField] private TMP_Text discardText;

    private readonly List<Card> deck = Dealer.deck;
    private readonly List<Card> discard = Dealer.discard;
    private readonly List<Card> hand = Dealer.hand;

    private readonly int handLimit = Hero.handLimit;

    public void EndTurn()
    {
        //Heros attack

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

        Hero.attack = 0;
        attackText.text = Hero.attack.ToString();

        //Enemys action

        if (Enemy.isStuned)
        {
            Debug.Log("Enemy is stuned");
        }
        else
        {
            EnemysAI.EnemyA(discard, herosBlockText, herosHpText, enemysBlockText);
        }

        //  FOR DEMO MAP 1 without stun card
        /*if (CollideWithEnemy.enemysName.Equals("Enemy A"))
        {
            EnemysAI.EnemyA(discard, herosBlockText, herosHpText, enemysBlockText);
        }
        else if (CollideWithEnemy.enemysName.Equals("Enemy B"))
        {
            EnemysAI.EnemyB(discard, herosBlockText, herosHpText, enemysBlockText, enemysHpText);
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

        Hero.block = 0;
        Hero.stamina = 3;
        Enemy.action = Random.Range(0, 5);
        Enemy.isStuned = false;
        Enemy.isEnraged = false;
        Enemy.block = 0;
        Dealer.WhatEnemyWillDo();

        herosBlockText.text = Hero.block.ToString();
        staminaText.text = Hero.stamina.ToString();
        enemysBlockText.text = "0";
        discardText.text = discard.Count.ToString();
    }
}