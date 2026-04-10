using UnityEngine;

public class SurvivalistAnimator : MonoBehaviour
{
    private const string IS_MOVING = "Moving";

    private Survivalist survivalist;
    private Animator animator;

    private void Awake()
    {
        survivalist = GetComponent<Survivalist>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_MOVING, survivalist.IsMoving());
    }
}
