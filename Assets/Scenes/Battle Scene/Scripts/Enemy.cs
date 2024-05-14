using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static readonly int hpCap = 4;

    public static int hp = 4;
    public static int defence = 0;
    public static int action;

    public static int attack;
    public static bool isStuned = false;
    public static bool isEnraged = false;

    public static void A(List<Card> discard, TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText)
    {
        if (action == 0) //deal dmg
        {
            attack += 5;
            DealDamage(herosDefenceText, herosHpText);
        }
        else if (action == 1) //add defence
        {
            AddDefence(enemysDefenceText, 5);
        }
        else if (action == 2) //deal dmg and add defence
        {
            attack += 3;
            DealDamage(herosDefenceText, herosHpText);
            AddDefence(enemysDefenceText, 2);
        }
        else if (action == 3) // add dazed to heros deck
        {
            discard.Add(Dealer.dazed);
            Dealer.Shuffle(discard);
        }
        // if action = 4 dont do anything || Confused
    }

    public static void B(TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText,
        TMP_Text enemysHpText)
    {
        if (action == 0) //deal dmg
        {
            attack += 3;
            DealDamage(herosDefenceText, herosHpText);
        }
        else if (action == 1) //add defence
        {
            AddDefence(enemysDefenceText, 7);
        }
        else if (action == 2) //add defence
        {
            AddDefence(enemysDefenceText, 5);
        }
        else if (action == 3) //heal
        {
            int amount = 3;
            Heal(amount);
            enemysHpText.text = hp.ToString();
        }
        // if action = 4 dont do anything || Confused
    }

    public static void DealDamage(TMP_Text herosDefenceText, TMP_Text herosHpText)
    {
        if (Hero.defence > 0)
        {
            if (Hero.defence >= attack)
            {
                Hero.defence -= attack;
                herosDefenceText.text = Hero.defence.ToString();
            }
            else
            {
                int remainingDamage = attack - Hero.defence;
                Hero.defence = 0;
                Hero.hp -= remainingDamage;
                herosDefenceText.text = Hero.defence.ToString();
                herosHpText.text = Hero.hp.ToString();
            }
        }
        else
        {
            Hero.hp -= attack;
            herosHpText.text = Hero.hp.ToString();
        }
        attack = 0;
    }

    private static void AddDefence(TMP_Text enemysDefenceText, int amount)
    {
        defence += amount;
        enemysDefenceText.text = defence.ToString();
    }

    public static void Heal(int amount)
    {
        hp += amount;
        if (hp > hpCap)
        {
            hp = hpCap;
        }
    }

    public static void WhatWillDo()
    {
        if (MapManager.stageIndex == 2)
        {
            if (Enemy.action == 0)
            {
                Dealer.enemysActionText.text = "+3 <sprite name=Attack>";
            }
            else if (Enemy.action == 1)
            {
                Dealer.enemysActionText.text = "+7 <sprite name=Defence>";
            }
            else if (Enemy.action == 2)
            {
                Dealer.enemysActionText.text = "+5 <sprite name=Defence>";
            }
            else if (Enemy.action == 3)
            {
                Dealer.enemysActionText.text = "+3 <sprite name=Heal>";
            }
            else
            {
                Dealer.enemysActionText.text = "Enemy is confused and will not do anything";
            }
        }
        else
        {
            if (Enemy.action == 0)
            {
                Dealer.enemysActionText.text = "+5 <sprite name=Attack>";
            }
            else if (Enemy.action == 1)
            {
                Dealer.enemysActionText.text = "+5 <sprite name=Defence>";
            }
            else if (Enemy.action == 2)
            {
                Dealer.enemysActionText.text = "+3 <sprite name=Attack> +2 <sprite name=Defence>";
            }
            else if (Enemy.action == 3)
            {
                Dealer.enemysActionText.text = "Enemy will add Dazed to your deck";
            }
            else
            {
                Dealer.enemysActionText.text = "Enemy is confused and will not do anything";
            }
        }

        //  FOR DEMO MAP 1
        /*if (CollideWithEnemy.enemysName.Equals("Enemy A"))
        {
            if (Enemy.action == 0)
            {
                Debug.Log("Enemy will deal 5 dmg");
            }
            else if (Enemy.action == 1)
            {
                Debug.Log("Enemy will add 5 defence");
            }
            else if (Enemy.action == 2)
            {
                Debug.Log("Enemy will deal 3 dmg and add 2 defence");
            }
            else if (Enemy.action == 3)
            {
                Debug.Log("Enemy will add Dazed to your deck");
            }
            else
            {
                Debug.Log("Enemy is confused and will not do anything");
            }
        }
        else if (CollideWithEnemy.enemysName.Equals("Enemy B"))
        {
            if (Enemy.action == 0)
            {
                Debug.Log("Enemy will deal 3 dmg");
            }
            else if (Enemy.action == 1)
            {
                Debug.Log("Enemy will add 7 defence");
            }
            else if (Enemy.action == 2)
            {
                Debug.Log("Enemy will add 5 defence");
            }
            else if (Enemy.action == 3)
            {
                Debug.Log("Enemy will heal by 3");
            }
            else
            {
                Debug.Log("Enemy is confused and will not do anything");
            }
        }*/
    }
}
