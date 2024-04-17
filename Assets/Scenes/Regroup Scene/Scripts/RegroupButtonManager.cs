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
}
