using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;


    private void Start()
    {
        
        animator = GetComponent<Animator>();

        // Get animation length dynamically
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // Destroy after animation ends
        Destroy(gameObject, animationLength);
    }

    
}
