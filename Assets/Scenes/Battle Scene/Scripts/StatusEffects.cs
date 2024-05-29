public static class StatusEffects
{
    public static int heroStunRounds;
    public static int heroWeakRounds;
    public static int heroWeakAmount;

    public const string stunIcon = "<sprite name=Stun>";
    public const string weakIcon = "<sprite name=Weak>";

    public static void Weak(string who)
    {
        if (who.Equals("Hero"))
        {
            if (heroWeakRounds == 0)
            {
                Hero.isWeak = false;
                Dealer.herosStatusEffectsText.text = Dealer.herosStatusEffectsText.text.Replace(weakIcon, "");
                return;
            }
            Hero.attack -= heroWeakAmount;
            Dealer.herosStatusEffectsText.text += weakIcon;
            Dealer.herosAttackText.text = Hero.attack.ToString();
            heroWeakRounds--;
        }
        else
        {
                
        }
    }
}
