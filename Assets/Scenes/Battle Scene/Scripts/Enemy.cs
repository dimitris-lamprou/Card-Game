using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static readonly int hpCap = 4;

    public static int hp = 4;
    public static int defence = 0;
    public static int action;

    //public static int addDefence;
    public static int attack;
    public static bool isStuned = false;
    public static bool isEnraged = false;

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
