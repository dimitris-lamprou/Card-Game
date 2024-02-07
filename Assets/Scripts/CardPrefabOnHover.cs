using UnityEngine;
using UnityEngine.EventSystems;

public class CardPrefabOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int hoverAmount = 50;
    private Vector3 initialPosition;

    void Start()
    {
        // Save the initial position of the button
        initialPosition = transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Move the button up when the mouse enters
        transform.position = initialPosition + new Vector3(0, hoverAmount, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return the button to its initial position when the mouse exits
        transform.position = initialPosition;
    }
}
