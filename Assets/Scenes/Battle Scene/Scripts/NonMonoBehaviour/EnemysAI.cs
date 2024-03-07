using System.Collections.Generic;
using TMPro;

public static class EnemysAI
{
    public static void EnemyA(List<Card> discard, TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText)
    {
        if (Enemy.action == 0) //deal dmg
        {
            Enemy.attack = 5;
            if (Enemy.isEnraged)
            {
                Enemy.attack++;
            }
            EnemyDealDamage(herosDefenceText, herosHpText);
        }
        else if (Enemy.action == 1) //add defence
        {
            Enemy.addDefence = 5;
            EnemyAddDefence(enemysDefenceText);
        }
        else if (Enemy.action == 2) //deal dmg and add defence
        {
            Enemy.attack = 3;
            if (Enemy.isEnraged)
            {
                Enemy.attack++;
            }
            Enemy.addDefence = 2;
            EnemyDealDamage(herosDefenceText, herosHpText);
            EnemyAddDefence(enemysDefenceText);
        }
        else if (Enemy.action == 3) // add dazed to heros deck
        {
            discard.Add(Enemy.dazed);
            Dealer.Shuffle(discard);
        }
                                    // if action = 4 dont do anything || Confused
    }

    public static void EnemyB(List<Card> discard, TMP_Text herosDefenceText, TMP_Text herosHpText, TMP_Text enemysDefenceText, 
        TMP_Text enemysHpText)
    {
        if (Enemy.action == 0) //deal dmg
        {
            Enemy.attack = 3;
            EnemyDealDamage(herosDefenceText, herosHpText);
        }
        else if (Enemy.action == 1) //add defence
        {
            Enemy.addDefence = 7;
            EnemyAddDefence(enemysDefenceText);
        }
        else if (Enemy.action == 2) //add defence
        {
            Enemy.addDefence = 5;
            EnemyAddDefence(enemysDefenceText);
        }
        else if (Enemy.action == 3) //heal
        {
            int heal = 3;
            for (int i = 0; i < heal; i++)
            {
                if (Enemy.hp == Enemy.hpCap)
                {
                    break;
                }
                Enemy.hp++;
            }
            enemysHpText.text = Enemy.hp.ToString();
        }
                                    // if action = 4 dont do anything || Confused
    }

    private static void EnemyDealDamage(TMP_Text herosDefenceText, TMP_Text herosHpText)
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
                herosDefenceText.text = "0";
                herosHpText.text = Hero.hp.ToString();
            }
        }
        else
        {
            Hero.hp -= Enemy.attack;
            herosHpText.text = Hero.hp.ToString();
        }
    }

    private static void EnemyAddDefence(TMP_Text enemysDefenceText)
    {
        Enemy.defence += Enemy.addDefence;
        enemysDefenceText.text = Enemy.defence.ToString();
    }
}
