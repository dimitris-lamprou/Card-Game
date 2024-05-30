using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private readonly List<Card> deck = Dealer.deck;
    private readonly List<Card> discard = Dealer.discard;
    private readonly List<Card> hand = Dealer.hand;

    private readonly int handLimit = Hero.handLimit;

    private int attackingEnemyPosition = 1;
    private Enemy enemy;
    private Enemy nextEnemy;
    private EnemysUiText enemysUiTextDefence;
    private EnemysUiText enemysUiTextStatusEffects;
    private EnemysUiText enemysUiTextAttack;
    private EnemysUiText nextEnemysUiTextThought;
    private EnemysUiText enemysUiTextThought;

    public void EndTurn()
    {
        enemy = Enemy.enemies.FirstOrDefault(e => e.position == attackingEnemyPosition);
        nextEnemy = Enemy.enemies.FirstOrDefault(e => e.position == 
            ((attackingEnemyPosition + 1 >= 3) ? 1 : attackingEnemyPosition + 1));
        enemysUiTextDefence = Dealer.enemiesDefenceText.FirstOrDefault(e => e.name.Contains(enemy.position.ToString()));
        enemysUiTextStatusEffects = Dealer.enemiesStatusEffectsText.FirstOrDefault(e => e.name.Contains(enemy.position.ToString()));
        enemysUiTextAttack = Dealer.enemiesAttackText.FirstOrDefault(e => e.name.Contains(enemy.position.ToString()));
        nextEnemysUiTextThought = Dealer.enemiesThoughtText.
            FirstOrDefault(e => e.name.Contains(((enemy.position + 1 >= 3) ? 1 : enemy.position + 1).ToString())); //not good
        enemysUiTextThought = Dealer.enemiesThoughtText.FirstOrDefault(e => e.name.Contains(enemy.position.ToString()));

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

        //Enemys pre Reset              //Maybe here reset all enemies

        if (enemy.defence > 0)
        {
            enemy.defence = 0;
            enemysUiTextDefence.tmp_Text.text = enemy.defence.ToString();
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

        if (enemy.isStuned)
        {
            //dont act
        }
        else
        {
            enemy.Act();
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

        //Enemy Reset                           //Maybe reset all enemies
        
        enemy.action = Random.Range(0, 100);
        enemy.isStuned = false;
        enemy.isEnraged = false;
        enemy.attack = 0;

        nextEnemy.PrepareMove();
        enemy.WhatWillDo(nextEnemysUiTextThought.tmp_Text); //not good

        enemysUiTextDefence.tmp_Text.text = enemy.defence.ToString();
        enemysUiTextStatusEffects.tmp_Text.text = "";
        enemysUiTextAttack.tmp_Text.text = enemy.attack.ToString();
        enemysUiTextThought.tmp_Text.text = string.Empty; 

        attackingEnemyPosition++;
        if (attackingEnemyPosition == 3) // later 3 will be 7 cause we will have 6 enemies i think
        {
            attackingEnemyPosition = 1;
        }
    }
}