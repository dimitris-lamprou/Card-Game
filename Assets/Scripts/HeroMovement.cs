using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private Rigidbody2D rb;

    public static float horizontal;
    public static float vertical;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(horizontal, vertical, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);
    }
}
