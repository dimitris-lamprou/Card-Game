using UnityEngine;
using UnityEngine.SceneManagement;

public class RegroupButtonManager : MonoBehaviour
{
    public void Rest()
    {
        int heal;
        heal = (30 * Hero.hpCap) / 100;
        Hero.hp += heal;
        if (Hero.hp > Hero.hpCap)
        {
            Hero.hp = Hero.hpCap;
        }
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

    /*public void Risk()
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
    }*/
}
