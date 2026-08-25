using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Globals.playerTag))
        {
            animator.SetBool(Globals.doorOpenParameter, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Globals.playerTag))
        {
            animator.SetBool(Globals.doorOpenParameter, false);
        }
    }
}
