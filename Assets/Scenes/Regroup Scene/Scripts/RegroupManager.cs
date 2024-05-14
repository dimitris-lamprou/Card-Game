using UnityEngine;
using UnityEngine.SceneManagement;

public class RegroupManager : MonoBehaviour
{
    public void Rest()
    {
        int amount;
        amount = (30 * Hero.hpCap) / 100;
        Hero.Heal(amount);
        SceneManager.LoadScene(1);
    }

    public void Search()
    {
        int firstRandomInt = Random.Range(1, 101);
        int secondRandomInt = Random.Range(1, 101);
        Debug.Log("1st rand int = " + firstRandomInt);
        Debug.Log("2nd rand int = " + secondRandomInt);

        if (firstRandomInt <= 50)
        {
            if (secondRandomInt <= 25)
            {
                Hero.attack += 3;
                Debug.Log("Hero.attack += 3;");
            }
            else if (secondRandomInt <= 50)
            {
                Hero.defence += 3;
                Debug.Log("Hero.defence += 3;");
            }
            else if (secondRandomInt <= 75)
            {
                Hero.stamina++;
                Debug.Log("Hero.stamina++;");
            }
            else
            {
                Hero.hpCap += 5;
                Debug.Log("Hero.hpCap += 5;");
            }
        }
        else
        {
            if (secondRandomInt <= 50)
            {
                Hero.scales += 25;
                Debug.Log("Hero.scales += 25;");
            }
            else if (secondRandomInt <= 80)
            {
                Hero.scales += 50;
                Debug.Log("Hero.scales += 50;");
            }
            else
            {
                Hero.scales += 100;
                Debug.Log("Hero.scales += 100;");
            }
        }
        SceneManager.LoadScene(1);
    }

    public void Risk()
    {
        int firstRandomInt = Random.Range(1, 101);
        int secondRandomInt = Random.Range(1, 101);
        Debug.Log("1st rand int = " + firstRandomInt);
        Debug.Log("2nd rand int = " + secondRandomInt);

        if (firstRandomInt <= 50)
        {
            if (secondRandomInt <= 25)
            {
                Hero.attack += 5;
                int x = Random.Range(0, 4);
                Hero.hp -= x;
                Debug.Log("Hero.attack += 5 and hp -= " + x);
            }
            else if (secondRandomInt <= 50)
            {
                Hero.defence += 5;
                int x = Random.Range(0, 4);
                Hero.hp -= x;
                Debug.Log("Hero.defence += 5 and hp -= " + x);
            }
            else if (secondRandomInt <= 75)
            {
                Hero.stamina += 2;
                if (Hero.scales >= 25)
                {
                    Hero.scales -= 25;
                }
                Debug.Log("Hero.stamina += 2 and -25 scales");
            }
            else
            {
                int x = Random.Range(1, 6);
                StatusEffects.heroStunRounds = x;
                Hero.hpCap += 20;
                Debug.Log("Hero.hpCap += 20 and become stuned for " + x + " Rounds");
            }
        }
        else
        {
            if (secondRandomInt <= 50)
            {
                Hero.scales += 70;
                Hero.deck.Add(Dealer.dazed);
                Debug.Log("Hero.scales += 70 and 1 dazed");
            }
            else if (secondRandomInt <= 80)
            {
                Hero.scales += 140;
                Hero.deck.Add(Dealer.dazed);
                Hero.deck.Add(Dealer.dazed);
                Debug.Log("Hero.scales += 140 and 2 dazed");
            }
            else
            {
                Hero.scales += 200;
                Hero.deck.Add(Dealer.dazed);
                Hero.deck.Add(Dealer.dazed);
                Hero.deck.Add(Dealer.dazed);
                Debug.Log("Hero.scales += 200 and 3 dazed");
            }
        }
        SceneManager.LoadScene(1);
    }
}
