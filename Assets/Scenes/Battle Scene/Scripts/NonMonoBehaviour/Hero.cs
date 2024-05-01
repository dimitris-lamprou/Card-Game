using System.Collections.Generic;

public static class Hero
{
    public static int hp = 100;
    public static int hpCap = 100;
    public static int defence = 0;
    public static int exp = 0;
    public static int handLimit = 5;
    public static int stamina = 3;
    //public static int staminaCap = 3; For later
    public static int scales = 0;

    public static int attack;
    public static int addDefence;
    public static int addExp;
    public static int heal;

    public static List<Card> deck = new();
}
