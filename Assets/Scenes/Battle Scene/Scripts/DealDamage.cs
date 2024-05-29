using UnityEngine;

public class DealDamage : MonoBehaviour
{
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
            if (Enemy.imp.defence > 0)
            {
                if (Enemy.imp.defence >= Hero.attack)
                {
                    Enemy.imp.defence -= Hero.attack;
                    Dealer.enemysDefenceText.text = Enemy.imp.defence.ToString();
                }
                else
                {
                    int remainingDamage = Hero.attack - Enemy.imp.defence;
                    Enemy.imp.defence = 0;
                    Enemy.imp.hp -= remainingDamage;
                    Dealer.enemysDefenceText.text = Enemy.imp.defence.ToString();
                    Dealer.enemysHpText.text = Enemy.imp.hp.ToString();
                }
            }
            else
            {
                Enemy.imp.hp -= Hero.attack;
                Dealer.enemysHpText.text = Enemy.imp.hp.ToString();
            }

            if (Enemy.imp.hp <= 0) //if hero destroyed enemy
            {
                //TODO MUST BE A FUNCTION OR CLASS
                //Store Exp
                Debug.Log("Enemy died");
                //Reset
                Hero.stamina = 3; // = cap for later
                Hero.attack = 0;
                Enemy.imp.defence = 0;
                Enemy.imp.hp = 4;
                if (MapManager.isFromMap)
                {
                    newCards.SetActive(true);
                    //SceneManager.LoadScene(2);
                }
            }

            Hero.attack = 0;
            Dealer.herosAttackText.text = Hero.attack.ToString();
        }
        Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Cursor"), Vector2.zero, CursorMode.Auto);
    }
}
