using UnityEngine;


public static class Extension //정적 한정자 안쪽에
{
    //정적 메서드로 만든다.
    //매개변수 첫번째가 중요함, 
    public static void SetIKTransformAndWeight(this Animator animator, AvatarIKGoal goal, Transform goalTransform, float weight = 1f)
    {
        animator.SetIKPositionWeight(goal, weight);
        animator.SetIKPosition(goal, goalTransform.position);
        animator.SetIKRotationWeight(goal, weight);
        animator.SetIKRotation(goal, goalTransform.rotation);
    }




}