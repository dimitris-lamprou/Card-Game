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
            if (Enemy.darkImp.defence > 0)
            {
                if (Enemy.darkImp.defence >= Hero.attack)
                {
                    Enemy.darkImp.defence -= Hero.attack;
                    Dealer.enemysDefenceText1.text = Enemy.darkImp.defence.ToString();
                }
                else
                {
                    int remainingDamage = Hero.attack - Enemy.darkImp.defence;
                    Enemy.darkImp.defence = 0;
                    Enemy.darkImp.hp -= remainingDamage;
                    Dealer.enemysDefenceText1.text = Enemy.darkImp.defence.ToString();
                    Dealer.enemysHpText1.text = Enemy.darkImp.hp.ToString();
                }
            }
            else
            {
                Enemy.darkImp.hp -= Hero.attack;
                Dealer.enemysHpText1.text = Enemy.darkImp.hp.ToString();
            }

            if (Enemy.darkImp.hp <= 0) //if hero destroyed enemy
            {
                //TODO MUST BE A FUNCTION OR CLASS
                //Store Exp
                Debug.Log("Enemy died");
                //Reset
                Hero.stamina = 3; // = cap for later
                Hero.attack = 0;
                Enemy.darkImp.defence = 0;
                Enemy.darkImp.hp = 4;
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
