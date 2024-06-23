using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private GameObject newCards;

    private string enemysName;

    private void Start()
    {
        enemysName = gameObject.name;
    }

    private void OnMouseEnter()
    {
        if (Hero.hasStun)
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Stun Cursor"), Vector2.zero, CursorMode.Auto);
        }
        else if (Hero.attack > 0)
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Attack Cursor"), Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Cursor"), Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Cursor"), Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseUp()
    {
        Enemy enemy = Enemy.enemies.FirstOrDefault(e => e.name == enemysName);

        EnemysUiText enemysUiTextDefence = Dealer.enemiesDefenceText.
            FirstOrDefault(e => e.name.Contains(enemy.position.ToString()));
        EnemysUiText enemysUiTextHp = Dealer.enemiesHpText.
            FirstOrDefault(e => e.name.Contains(enemy.position.ToString()));
        EnemysUiText enemysUiTextStatusEffects = Dealer.enemiesStatusEffectsText.
            FirstOrDefault(e => e.name.Contains(enemy.position.ToString()));
        EnemysUiText enemysUiTextThought = Dealer.enemiesThoughtText.
            FirstOrDefault(e => e.name.Contains(enemy.position.ToString()));


        if (Hero.hasStun)
        {
            enemy.isStuned = true;
            enemysUiTextStatusEffects.tmp_Text.text += "<sprite name=Stun>";
            //enemysUiTextThought.tmp_Text.text = "";

            Hero.hasStun = false;
            OnMouseEnter();
        }
        else if (Hero.attack > 0)
        {     
            if (enemy.defence > 0)
            {
                if (enemy.defence >= Hero.attack)
                {
                    enemy.defence -= Hero.attack;
                   enemysUiTextDefence.tmp_Text.text = enemy.defence.ToString();
                }
                else
                {
                    int remainingDamage = Hero.attack - enemy.defence;
                    enemy.defence = 0;
                    enemy.hp -= remainingDamage;
                   enemysUiTextDefence.tmp_Text.text = enemy.defence.ToString();
                    enemysUiTextHp.tmp_Text.text = enemy.hp.ToString();
                }
            }
            else
            {
                enemy.hp -= Hero.attack;
                enemysUiTextHp.tmp_Text.text = enemy.hp.ToString();
            }

            if (enemy.hp <= 0) //if hero destroyed enemy
            {
                //TODO MUST BE A FUNCTION OR CLASS
                //Store Exp
                Debug.Log("Enemy died");
                //Reset
                Hero.stamina = 3; // = cap for later
                Hero.attack = 0;
                enemy.defence = 0;
                enemy.hp = 4;
                if (MapManager.isFromMap)
                {
                    newCards.SetActive(true);
                    //SceneManager.LoadScene(2);
                }
            }

            Hero.attack = 0;
            Dealer.herosAttackText.text = Hero.attack.ToString();
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Cursor"), Vector2.zero, CursorMode.Auto);
        }
    }
}
