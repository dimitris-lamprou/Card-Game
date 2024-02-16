using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private float h;
    private float v;

    // Update is called once per frame
    void Update()
    {
        h = HeroMovement.horizontal;
        v = HeroMovement.vertical;

        if (h > 0)
        {
            animator.SetBool("WalkRIGHT", true);

            animator.SetBool("WalkLEFT", false);
            animator.SetBool("WalkUP", false);
            animator.SetBool("WalkDOWN", false);
        }
        else if (h < 0)
        {
            animator.SetBool("WalkLEFT", true);

            animator.SetBool("WalkRIGHT", false);
            animator.SetBool("WalkUP", false);
            animator.SetBool("WalkDOWN", false);
        }
        else if (v > 0)
        {
            animator.SetBool("WalkUP", true);

            animator.SetBool("WalkRIGHT", false);
            animator.SetBool("WalkLEFT", false);
            animator.SetBool("WalkDOWN", false);
        }
        else if (v < 0)
        {
            animator.SetBool("WalkDOWN", true);

            animator.SetBool("WalkRIGHT", false);
            animator.SetBool("WalkLEFT", false);
            animator.SetBool("WalkUP", false);
        }
        else
        {
            animator.SetBool("WalkDOWN", false);
            animator.SetBool("WalkRIGHT", false);
            animator.SetBool("WalkLEFT", false);
            animator.SetBool("WalkUP", false);
        }
    }
}
