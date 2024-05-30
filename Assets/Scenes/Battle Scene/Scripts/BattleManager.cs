using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
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
                enemysDefenceText1.text = Enemy.defence.ToString();
            }
            else
            {
                int remainingDamage = Hero.attack - Enemy.defence;
                Enemy.defence = 0;
                Enemy.hp -= remainingDamage;
                enemysDefenceText1.text = "0";
                enemysHpText1.text = Enemy.hp.ToString();
            }
        }
        else
        {
            Enemy.hp -= Hero.attack;
            enemysHpText1.text = Enemy.hp.ToString();
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
            enemysAttackText1.text = Enemy.attack.ToString();
        }*/

        //Enemys pre Reset

        if (Enemy.imp.defence > 0)
        {
            Enemy.imp.defence = 0;
            Dealer.enemysDefenceText1.text = Enemy.imp.defence.ToString();
        }

        //Hero Reset

        Hero.defence = 0;
        Hero.stamina = Hero.staminaCap;
        Hero.attack = 0;

        Dealer.herosDefenceText.text = Hero.defence.ToString();
        Dealer.herosStatusEffectsText.text = "";
        Dealer.herosStaminaText.text = Hero.stamina.ToString();
        Dealer.herosAttackText.text = Hero.attack.ToString();
        Dealer.discardText.text = discard.Count.ToString();

        if (StatusEffects.heroStunRounds > 0)
        {
            StatusEffects.heroStunRounds--;
        }

        if (StatusEffects.heroStunRounds > 0)
        {
            Dealer.herosStatusEffectsText.text += StatusEffects.stunIcon;
        }

        //Enemys action

        /*if (Enemy.isStuned)
        {
            //dont act
        }
        else if (MapManager.stageIndex == 2)
        {
            Enemy.B(Dealer.herosDefenceText, Dealer.herosHpText, Dealer.enemysDefenceText1, Dealer.enemysHpText1);
        }
        else
        {
            Enemy.A(discard, Dealer.herosDefenceText, Dealer.herosHpText, Dealer.enemysDefenceText1);
        }*/

        //Enemys action

        if (Enemy.imp.isStuned)
        {
            //dont act
        }
        else
        {
            Enemy.imp.Act();
            if (Hero.isWeak)
            {
                Debug.Log("Weak by " + StatusEffects.heroWeakAmount + " for " + StatusEffects.heroWeakRounds + " rounds");
                StatusEffects.Weak("Hero");
            }
        }

        /*if (Enemy.attack > 0)
        {
            Enemy.DealDamage(Dealer.herosDefenceText, Dealer.herosHpText);
        }*/

        //  FOR DEMO MAP 1 without stun card
        /*if (CollideWithEnemy.enemysName.Equals("Enemy A"))
        {
            EnemysAI.A(discard, herosDefenceText, herosHpText, enemysDefenceText1);
        }
        else if (CollideWithEnemy.enemysName.Equals("Enemy B"))
        {
            EnemysAI.B(discard, herosDefenceText, herosHpText, enemysDefenceText1, enemysHpText1);
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

        //Enemy Reset
        
        Enemy.imp.action = Random.Range(0, 100);
        Enemy.imp.isStuned = false;
        Enemy.imp.isEnraged = false;
        Enemy.imp.attack = 0;
        Enemy.imp.PrepareMove();
        Enemy.imp.WhatWillDo();

        Dealer.enemysDefenceText1.text = Enemy.imp.defence.ToString();
        Dealer.enemysStatusEffectsText1.text = "";
        Dealer.enemysAttackText1.text = Enemy.imp.attack.ToString();
    }
}