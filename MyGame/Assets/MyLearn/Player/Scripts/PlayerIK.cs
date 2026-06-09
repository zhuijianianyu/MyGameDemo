using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    [Header("IK…Ë÷√")]
    public Animator animator;

    [Range(-1f, 1f)]
    public float weight = 0f;


    private void OnAnimatorIK(int layerIndex)
    {
         animator.SetIKPosition(AvatarIKGoal.RightFoot, Vector3.zero);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, weight);
    }
}
