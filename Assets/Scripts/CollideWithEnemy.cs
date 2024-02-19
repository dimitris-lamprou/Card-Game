using UnityEngine;
using UnityEngine.SceneManagement;

public class CollideWithEnemy : MonoBehaviour
{
    public static string enemysName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemysName = other.name;
            SceneManager.LoadScene("Battle Scene");
        }
    }
}
