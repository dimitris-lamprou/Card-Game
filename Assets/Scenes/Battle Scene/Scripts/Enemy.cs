using UnityEngine;
using TMPro;

public class Enemy
{
    public int hpCap;
    public int hp;
    public int defence;
    public int attack;
    public bool isStuned = false;
    public bool isEnraged = false;
    public int action;

    public int countMove1;
    public int countMove2;

    public static string move;

    //Enemies
    public static Enemy imp;

    public static void InitEnemies()
    {
        imp = new()
        {
            hpCap = 30,
            hp = 30,
            defence = 0,
            attack = 0,
            isStuned = false,
            isEnraged = false,
            action = 0
        };
    }

    public void Move(string name)
    {
        if (name.Equals("Rake"))
        {
            attack = 10;
        }
        else if (name.Equals("Bite"))
        {
            attack = 6;
        }
    }

    public void PrepareMove()
    {
        action = Random.Range(0, 100);
        if (countMove1 == 2)
        {
            countMove1 = 0;
            move = "Bite";
            Move(move);
            countMove2++;
            return;
        }
        else if (countMove2 == 2)
        {
            countMove2 = 0;
            move = "Rake";
            Move(move);
            countMove1++;
            return;
        }

        if (action <= 60)
        {
            move = "Rake";
            Move(move);
            countMove1++;
            countMove2 = 0;
        }
        else
        {
            move = "Bite";
            Move(move);
            countMove2++;
            countMove1 = 0;
        }
    }

    public void Act()
    {
        switch (move)
        {
            case "Rake":
                DealDamage();
                break;
            case "Bite":
                DealDamage();
                MakeHeroWeak();
                break;
        }
    }

    /*public void A(List<Card> discard, TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText)
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
    }*/

    /*public void B(TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText,
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
    }*/

    public void DealDamage()
    {
        if (Hero.defence > 0)
        {
            if (Hero.defence >= attack)
            {
                Hero.defence -= attack;
                Dealer.herosDefenceText.text = Hero.defence.ToString();
            }
            else
            {
                int remainingDamage = attack - Hero.defence;
                Hero.defence = 0;
                Hero.hp -= remainingDamage;
                Dealer.herosDefenceText.text = Hero.defence.ToString();
                Dealer.herosHpText.text = Hero.hp.ToString();
            }
        }
        else
        {
            Hero.hp -= attack;
            Dealer.herosHpText.text = Hero.hp.ToString();
        }
        attack = 0;
    }

    private void AddDefence(TMP_Text enemysDefenceText, int amount)
    {
        defence += amount;
        enemysDefenceText.text = defence.ToString();
    }

    public void Heal(int amount)
    {
        hp += amount;
        if (hp > hpCap)
        {
            hp = hpCap;
        }
    }

    public void WhatWillDo()
    {
        switch (move)
        {
            case "Rake":
                Dealer.enemysActionText.text = "+10 <sprite name=Attack>";
                break;
            case "Bite":
                Dealer.enemysActionText.text = "+6 <sprite name=Attack> and <sprite name=Weak>";
                break;
        }
        

        /*if (MapManager.stageIndex == 2)
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
        }*/

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

    public void MakeHeroWeak()
    {
        StatusEffects.heroWeakAmount = Random.Range(1,3);
        StatusEffects.heroWeakRounds = Random.Range(1,3);
        Hero.isWeak = true;
    }
}
