using System.Collections.Generic;
using TMPro;

public static class EnemysAI
{
    public static void EnemyA(List<Card> discard, TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText)
    {
        if (Enemy.action == 0) //deal dmg
        {
            Enemy.attack += 5;
            EnemyDealDamage(herosDefenceText, herosHpText);
        }
        else if (Enemy.action == 1) //add defence
        {
            EnemyAddDefence(enemysDefenceText , 5);
        }
        else if (Enemy.action == 2) //deal dmg and add defence
        {
            Enemy.attack += 3;
            EnemyDealDamage(herosDefenceText, herosHpText);
            EnemyAddDefence(enemysDefenceText, 2);
        }
        else if (Enemy.action == 3) // add dazed to heros deck
        {
            discard.Add(Dealer.dazed);
            Dealer.Shuffle(discard);
        }
                                    // if action = 4 dont do anything || Confused
    }

    public static void EnemyB(TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText, 
        TMP_Text enemysHpText)
    {
        if (Enemy.action == 0) //deal dmg
        {
            Enemy.attack += 3;
            EnemyDealDamage(herosDefenceText, herosHpText);
        }
        else if (Enemy.action == 1) //add defence
        {
            EnemyAddDefence(enemysDefenceText, 7);
        }
        else if (Enemy.action == 2) //add defence
        {
            EnemyAddDefence(enemysDefenceText, 5);
        }
        else if (Enemy.action == 3) //heal
        {
            int amount = 3;
            Enemy.Heal(amount);
            enemysHpText.text = Enemy.hp.ToString();
        }
                                    // if action = 4 dont do anything || Confused
    }

    public static void EnemyDealDamage(TMP_Text herosDefenceText, TMP_Text herosHpText)
    {
        if (Hero.defence > 0)
        {
            if (Hero.defence >= Enemy.attack)
            {
                Hero.defence -= Enemy.attack;
                herosDefenceText.text = Hero.defence.ToString();
            }
            else
            {
                int remainingDamage = Enemy.attack - Hero.defence;
                Hero.defence = 0;
                Hero.hp -= remainingDamage;
                herosDefenceText.text = Hero.defence.ToString();
                herosHpText.text = Hero.hp.ToString();
            }
        }
        else
        {
            Hero.hp -= Enemy.attack;
            herosHpText.text = Hero.hp.ToString();
        }
        Enemy.attack = 0;
    }

    private static void EnemyAddDefence(TMP_Text enemysDefenceText, int amount)
    {
        Enemy.defence += amount;
        enemysDefenceText.text = Enemy.defence.ToString();
    }
}
