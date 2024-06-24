using System.Linq;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
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
        else if (Hero.hasEnrage)
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Enrage Cursor"), Vector2.zero, CursorMode.Auto);
        }
        else if (Hero.hasDrain)
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Drain Cursor"), Vector2.zero, CursorMode.Auto);
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
        EnemysUiText enemysUiTextAttack = Dealer.enemiesAttackText.
            FirstOrDefault(e => e.name.Contains(enemy.position.ToString()));


        if (Hero.hasStun)
        {
            enemy.isStuned = true;
            enemysUiTextStatusEffects.tmp_Text.text += "<sprite name=Stun>";
            //enemysUiTextThought.tmp_Text.text = "";

            Hero.hasStun = false;
            OnMouseEnter();
        }
        else if (Hero.hasEnrage)
        {
            enemy.isEnraged = true;
            if (enemy.defence > 0)
            {
                enemy.defence-= Random.Range(1,3);
                if (enemy.defence < 0)
                {
                    enemy.defence = 0;
                }
                enemysUiTextDefence.tmp_Text.text = enemy.defence.ToString();
            }
            enemy.attack+= Random.Range(1,3);
            enemysUiTextAttack.tmp_Text.text = enemy.attack.ToString();
            enemysUiTextStatusEffects.tmp_Text.text += "<sprite name=Enrage>";

            if (Hero.hasDrain) //must be a condition that refers to card Flurry of insults
            {
                int amount = Random.Range(3, 6);

                foreach (var enemy1 in Enemy.enemies)
                {
                    if (enemy1.isEnraged)
                    {
                        DealDamage(enemy1,
                            Dealer.enemiesDefenceText.FirstOrDefault(e => e.name.Contains(enemy1.position.ToString())),
                            Dealer.enemiesHpText.FirstOrDefault(e => e.name.Contains(enemy1.position.ToString())),
                            amount);

                        Hero.Heal(amount);
                    }
                }

                Hero.hasDrain = false;
            }

            Hero.hasEnrage = false;
            OnMouseEnter();
        }
        else if (Hero.hasDrain)
        {
            int amount = Random.Range(3, 6);

            foreach (var enemy1 in Enemy.enemies)
            {
                if (enemy1.isEnraged)
                {
                    DealDamage(enemy1, 
                        Dealer.enemiesDefenceText.FirstOrDefault(e => e.name.Contains(enemy1.position.ToString())),
                        Dealer.enemiesHpText.FirstOrDefault(e => e.name.Contains(enemy1.position.ToString())),
                        amount);

                    Hero.Heal(amount);
                }
            }

            Hero.hasDrain = false;
            OnMouseEnter();
        }
        else if (Hero.attack > 0)
        {
            DealDamage(enemy, enemysUiTextDefence, enemysUiTextHp, Hero.attack);

            Hero.attack = 0;
            Dealer.herosAttackText.text = Hero.attack.ToString();
            Cursor.SetCursor(Resources.Load<Texture2D>("Icons/Cursor"), Vector2.zero, CursorMode.Auto);
        }
    }

    private void DealDamage(Enemy receiver, EnemysUiText enemysUiTextDefence, EnemysUiText enemysUiTextHp, int amount)
    {
        if (receiver.defence > 0)
        {
            if (receiver.defence >= Hero.attack)
            {
                receiver.defence -= Hero.attack;
                enemysUiTextDefence.tmp_Text.text = receiver.defence.ToString();
            }
            else
            {
                int remainingDamage = Hero.attack - receiver.defence;
                receiver.defence = 0;
                receiver.hp -= remainingDamage;
                enemysUiTextDefence.tmp_Text.text = receiver.defence.ToString();
                enemysUiTextHp.tmp_Text.text = receiver.hp.ToString();
            }
        }
        else
        {
            receiver.hp -= Hero.attack;
            enemysUiTextHp.tmp_Text.text = receiver.hp.ToString();
        }

        if (receiver.hp <= 0) //if hero destroyed enemy
        {
            //TODO MUST BE A FUNCTION OR CLASS
            //Store Exp
            Debug.Log("Enemy died");
            //Reset
            Hero.stamina = 3; // = cap for later
            Hero.attack = 0;
            receiver.defence = 0;
            receiver.hp = 4;
            if (MapManager.isFromMap)
            {
                newCards.SetActive(true);
                //SceneManager.LoadScene(2);
            }
        }
    }
}
