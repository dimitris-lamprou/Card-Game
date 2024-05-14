using System.Collections.Generic;

public static class Hero
{
    public static int hp = 100;
    public static int hpCap = 100;
    public static int defence = 0;
    public static int exp = 0;
    public static int handLimit = 5;
    public static int stamina = 3;
    public static int staminaCap = 3;
    public static int scales = 0;
    public static int level = 1;
    public static int inventorySlots = 1;
    public static int attack = 0;

    public static List<Card> deck = new();

    public static void Heal(int amount)
    {
        hp += amount;
        if (hp > hpCap)
        {
            hp = hpCap;
        }
    }

    public static void AddDefence(int amount)
    {
        defence += amount;
    }

    public static void AddExp(int amount)
    {
        exp += amount;
    }
}
