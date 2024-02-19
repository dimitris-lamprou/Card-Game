using System.Collections.Generic;
using TMPro;

public static class EnemysAI
{
    public static void EnemyA(List<Card> discard, TMP_Text herosBlockText, TMP_Text herosHpText, TMP_Text enemysBlockText)
    {
        if (Enemy.action == 0) //deal dmg
        {
            Enemy.attack = 5;
            EnemyDealDamage(herosBlockText, herosHpText);
        }
        else if (Enemy.action == 1) //add block
        {
            Enemy.addBlock = 5;
            EnemyAddBlock(enemysBlockText);
        }
        else if (Enemy.action == 2) //deal dmg and add block
        {
            Enemy.attack = 3;
            Enemy.addBlock = 2;
            EnemyDealDamage(herosBlockText, herosHpText);
            EnemyAddBlock(enemysBlockText);
        }
        else if (Enemy.action == 3) // add dazed to heros deck
        {
            discard.Add(Enemy.dazed);
            Dealer.Shuffle(discard);
        }
                                    // if action = 4 dont do anything || Confused
    }

    public static void EnemyB(List<Card> discard, TMP_Text herosBlockText, TMP_Text herosHpText, TMP_Text enemysBlockText, 
        TMP_Text enemysHpText)
    {
        if (Enemy.action == 0) //deal dmg
        {
            Enemy.attack = 3;
            EnemyDealDamage(herosBlockText, herosHpText);
        }
        else if (Enemy.action == 1) //add block
        {
            Enemy.addBlock = 7;
            EnemyAddBlock(enemysBlockText);
        }
        else if (Enemy.action == 2) //add block
        {
            Enemy.addBlock = 5;
            EnemyAddBlock(enemysBlockText);
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

    private static void EnemyDealDamage(TMP_Text herosBlockText, TMP_Text herosHpText)
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

    private static void EnemyAddBlock(TMP_Text enemysBlockText)
    {
        Enemy.block += Enemy.addBlock;
        enemysBlockText.text = Enemy.block.ToString();
    }
}
