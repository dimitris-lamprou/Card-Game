using UnityEngine;

public class SignPost : MonoBehaviour
{
    [SerializeField] private GameObject spriteRenderer;
    [SerializeField] private GameObject signPostText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.SetActive(false);
        signPostText.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (spriteRenderer.activeSelf)
        {
            signPostText.SetActive(true);
        }
    }
}
