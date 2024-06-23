using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class Enemy
{
    public string name;
    public int hpCap;
    public int hp;
    public int defence;
    public int attack;
    public bool isStuned = false;
    public bool isEnraged = false;
    public int action;

    public int countMove1;
    public int countMove2;
    public int percentageOfMove;
    public string move1;
    public string move2;
    public int position;

    public static string move;
    public static List<Enemy> enemies = new();

    //Enemies
    public static Enemy imp;
    public static Enemy darkImp;

    public static void InitEnemies()
    {
        imp = new()
        {
            name = "Imp",
            hpCap = 30,
            hp = 30,
            defence = 0,
            attack = 0,
            isStuned = false,
            isEnraged = false,
            action = 0,
            percentageOfMove = 60,
            move1 = "Rake",
            move2 = "Bite",
            position = 1
        };
        darkImp = new()
        {
            name = "DarkImp",
            hpCap = 40,
            hp = 40,
            defence = 0,
            attack = 0,
            isStuned = false,
            isEnraged = false,
            action = 0,
            percentageOfMove = 60,
            move1 = "PowerRake",
            move2 = "PowerBite",
            position = 2
        };
        enemies.AddRange(new[]
        {
            new Enemy
            {
                name = "Imp",
                hpCap = 30,
                hp = 30,
                defence = 0,
                attack = 0,
                isStuned = false,
                isEnraged = false,
                action = 0,
                percentageOfMove = 60,
                move1 = "Rake",
                move2 = "Bite",
                position = 1
            },
            new Enemy
            {
                name = "DarkImp",
                hpCap = 40,
                hp = 40,
                defence = 0,
                attack = 0,
                isStuned = false,
                isEnraged = false,
                action = 0,
                percentageOfMove = 60,
                move1 = "PowerRake",
                move2 = "PowerBite",
                position = 2
            },
            // Add more enemies here
        });

    }

    public void Move(string name)
    {
        switch (name)
        {
            case "Rake":
                attack = 10;
                break;
            case "Bite":
                attack = 6;
                break;
            case "PowerRake":
                attack = 15;
                break;
            case "PowerBite":
                attack = 8;
                break;
        }
    }

    public void PrepareMove()
    {
        action = Random.Range(0, 100);
        if (countMove1 == 2)
        {
            countMove1 = 0;
            move = move2;
            Move(move);
            countMove2++;
            return;
        }
        else if (countMove2 == 2)
        {
            countMove2 = 0;
            move = move1;
            Move(move);
            countMove1++;
            return;
        }

        if (action <= percentageOfMove)
        {
            move = move1;
            Move(move);
            countMove1++;
            countMove2 = 0;
        }
        else
        {
            move = move2;
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
            case "PowerRake":
                DealDamage();
                break;
            case "PowerBite":
                DealDamage();
                MakeHeroWeak();
                break;
        }
    }

    /*public void A(List<Card> discard, TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText1)
    {
        if (action == 0) //deal dmg
        {
            attack += 5;
            DealDamage(herosDefenceText, herosHpText);
        }
        else if (action == 1) //add defence
        {
            AddDefence(enemysDefenceText1, 5);
        }
        else if (action == 2) //deal dmg and add defence
        {
            attack += 3;
            DealDamage(herosDefenceText, herosHpText);
            AddDefence(enemysDefenceText1, 2);
        }
        else if (action == 3) // add dazed to heros deck
        {
            discard.Add(Dealer.dazed);
            Dealer.Shuffle(discard);
        }
        // if action = 4 dont do anything || Confused
    }*/

    /*public void B(TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText1,
        TMP_Text enemysHpText1)
    {
        if (action == 0) //deal dmg
        {
            attack += 3;
            DealDamage(herosDefenceText, herosHpText);
        }
        else if (action == 1) //add defence
        {
            AddDefence(enemysDefenceText1, 7);
        }
        else if (action == 2) //add defence
        {
            AddDefence(enemysDefenceText1, 5);
        }
        else if (action == 3) //heal
        {
            int amount = 3;
            Heal(amount);
            enemysHpText1.text = hp.ToString();
        }
        // if action = 4 dont do anything || Confused
    }*/

    public void DealDamage()
    {
        if (this.isStuned)
        {
            return;
        }

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

    private void AddDefence(TMP_Text enemysDefenceText1, int amount)
    {
        defence += amount;
        enemysDefenceText1.text = defence.ToString();
    }

    public void Heal(int amount)
    {
        hp += amount;
        if (hp > hpCap)
        {
            hp = hpCap;
        }
    }

    public void WhatWillDo(TMP_Text enemysThought) //not good
    {
        switch (move)
        {
            case "Rake":
                enemysThought.text = "+10 <sprite name=Attack>";
                break;
            case "Bite":
                enemysThought.text = "+6 <sprite name=Attack> and <sprite name=Weak>";
                break;
            case "PowerRake":
                enemysThought.text = "+15 <sprite name=Attack>";
                break;
            case "PowerBite":
                enemysThought.text = "+8 <sprite name=Attack> and <sprite name=Weak>";
                break;
        }
        

        /*if (MapManager.stageIndex == 2)
        {
            if (Enemy.action == 0)
            {
                Dealer.enemysThoughtText1.text = "+3 <sprite name=Attack>";
            }
            else if (Enemy.action == 1)
            {
                Dealer.enemysThoughtText1.text = "+7 <sprite name=Defence>";
            }
            else if (Enemy.action == 2)
            {
                Dealer.enemysThoughtText1.text = "+5 <sprite name=Defence>";
            }
            else if (Enemy.action == 3)
            {
                Dealer.enemysThoughtText1.text = "+3 <sprite name=Heal>";
            }
            else
            {
                Dealer.enemysThoughtText1.text = "Enemy is confused and will not do anything";
            }
        }
        else
        {
            if (Enemy.action == 0)
            {
                Dealer.enemysThoughtText1.text = "+5 <sprite name=Attack>";
            }
            else if (Enemy.action == 1)
            {
                Dealer.enemysThoughtText1.text = "+5 <sprite name=Defence>";
            }
            else if (Enemy.action == 2)
            {
                Dealer.enemysThoughtText1.text = "+3 <sprite name=Attack> +2 <sprite name=Defence>";
            }
            else if (Enemy.action == 3)
            {
                Dealer.enemysThoughtText1.text = "Enemy will add Dazed to your deck";
            }
            else
            {
                Dealer.enemysThoughtText1.text = "Enemy is confused and will not do anything";
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
