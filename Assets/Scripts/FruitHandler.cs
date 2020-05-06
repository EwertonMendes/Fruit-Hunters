using System.Linq;
using UnityEngine;

public class FruitHandler : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {

        if (collider2D.gameObject.CompareTag("Player"))
        {
            if(animator)
                animator.SetBool("Collected", true);

            Destroy(this.gameObject, 0.25f);
        }

        if (collider2D.gameObject.CompareTag("Grounds") || collider2D.gameObject.CompareTag("Walls&Ceil"))
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Grounds") || collider2D.gameObject.CompareTag("Walls&Ceil"))
        {
            Destroy(this.gameObject);
        }
    }
}
