using TMPro;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private TMP_Text enemysHpText;
    [SerializeField] private TMP_Text enemysDefenceText;
    [SerializeField] private TMP_Text herosAttackText;
    [SerializeField] private GameObject newCards;

    private void OnMouseEnter()
    {
        if (Hero.attack > 0)
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Attack Cursor"), Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Cursor"), Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseUp()
    {
        if (Hero.attack > 0)
        {
            if (Enemy.defence > 0)
            {
                if (Enemy.defence >= Hero.attack)
                {
                    Enemy.defence -= Hero.attack;
                    enemysDefenceText.text = Enemy.defence.ToString();
                }
                else
                {
                    int remainingDamage = Hero.attack - Enemy.defence;
                    Enemy.defence = 0;
                    Enemy.hp -= remainingDamage;
                    enemysDefenceText.text = Enemy.defence.ToString();
                    enemysHpText.text = Enemy.hp.ToString();
                }
            }
            else
            {
                Enemy.hp -= Hero.attack;
                enemysHpText.text = Enemy.hp.ToString();
            }

            if (Enemy.hp <= 0) //if hero destroyed enemy
            {
                //TODO MUST BE A FUNCTION OR CLASS
                //Store Exp
                Debug.Log("Enemy died");
                //Reset
                Hero.stamina = 3; // = cap for later
                Hero.attack = 0;
                Enemy.defence = 0;
                Enemy.hp = 4;
                if (MapManager.isFromMap)
                {
                    newCards.SetActive(true);
                    //SceneManager.LoadScene(2);
                }
            }

            Hero.attack = 0;
            herosAttackText.text = Hero.attack.ToString();
        }
        Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Cursor"), Vector2.zero, CursorMode.Auto);
    }
}
