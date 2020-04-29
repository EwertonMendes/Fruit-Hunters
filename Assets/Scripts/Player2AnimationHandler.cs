using Assets.Scripts.Enums;
using UnityEngine;

public class Player2AnimationHandler : MonoBehaviour
{
    private Animator animator;
    private string playerState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerState = GetComponent<Player2MovementHandler>().playerState.ToString();

        if (playerState == "Walking")
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Jumping", false);
        } else if (playerState == "Jumping")
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Jumping", true);
        } else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Jumping", false);
        }
    }
}
