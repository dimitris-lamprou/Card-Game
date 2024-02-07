using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnClickManager : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private TMP_Text herosHpText;
    [SerializeField] private TMP_Text herosBlockText;
    [Space]
    [Header("Enemy")]
    [SerializeField] private TMP_Text enemysBlockText;
    [Space]
    [Header("Labels")]
    [SerializeField] private TMP_Text discardText;
    [Space]
    [Header("For test")]
    [SerializeField] private TMP_Text graveyardText;

    private readonly List<Card> deck = Dealer.deck;
    private readonly List<Card> discard = Dealer.discard;
    private readonly List<Card> hand = Dealer.hand;

    private readonly int handLimit = Hero.handLimit;

    public void EndTurn()
    {
        Enemy.block = 0;
        enemysBlockText.text = "0";

        //Enemys action

        if (Enemy.action == 0) //deal dmg
        {
            Enemy.attack = 5;
            EnemyDealDamage();
        }
        else if (Enemy.action == 1) //add 1 block
        {
            Enemy.addBlock = 5;
            EnemyAddBlock();
        }
        else if (Enemy.action == 2)
        {
            Enemy.attack = 3;
            Enemy.addBlock = 2;
            EnemyDealDamage();
            EnemyAddBlock();
        }
        else if (Enemy.action == 3)
        {
            discard.Add(Enemy.dazed);
            Dealer.Shuffle(discard);
        }

        //Deal Cards
        if (hand.Count > 0)
        {
            if (deck.Count + hand.Count < handLimit)
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
            Dealer.Deal(deck);
        }
        else if (deck.Count < handLimit)
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

        herosBlockText.text = "0";
        Hero.block = 0;
        Enemy.action = Random.Range(0, 5);
        Dealer.WhatEnemyWillDo();
        discardText.text = discard.Count.ToString();
    }

    private void EnemyDealDamage()
    {
        if (Hero.block > 0)
        {
            if (Hero.block >= Enemy.attack)
            {
                Hero.block -= Enemy.attack;
                herosBlockText.text = Hero.block.ToString();
            }
            else
            {
                int remainingDamage = Enemy.attack - Hero.block;
                Hero.block = 0;
                Hero.hp -= remainingDamage;
                herosBlockText.text = "0";
                herosHpText.text = Hero.hp.ToString();
            }
        }
        else
        {
            Hero.hp -= Enemy.attack;
            herosHpText.text = Hero.hp.ToString();
        }
    }

    private void EnemyAddBlock()
    {
        Enemy.block += Enemy.addBlock;
        enemysBlockText.text = Enemy.block.ToString();
    }
}