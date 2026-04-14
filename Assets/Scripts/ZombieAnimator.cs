using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    private Animator animator;
    private Survivalist survivalistGroup;

    private float disableDistance = 70f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        survivalistGroup = FindAnyObjectByType<Survivalist>();
    }

    private void Update()
    {
        //if (survivalistGroup == null) return;

        //float distance = Vector3.Distance(transform.position, survivalistGroup.transform.position);

        //animator.enabled = distance < disableDistance;
    }
}
