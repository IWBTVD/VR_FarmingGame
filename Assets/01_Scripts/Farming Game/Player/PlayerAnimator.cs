using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using Oculus.Interaction;

public class PlayerAnimator : MonoBehaviour
{
    [Header("손 포지션 동기화")]
    [SerializeField] private Transform rightHandOffset;

    [SerializeField] private Transform leftHandOffset;

    [Space]
    [Header("손가락 동기화")]
    [SerializeField] private List<Transform> rightHandFingers = new();
    [SerializeField] private List<Transform> leftHandFingers = new();

    [SerializeField] private Transform[] rightThumbBones;
    [SerializeField] private Transform[] rightIndexBones;
    [SerializeField] private Transform[] rightFingersBones;
    [Space]
    [SerializeField] private Transform[] leftThumbBones;
    [SerializeField] private Transform[] leftIndexBones;
    [SerializeField] private Transform[] leftFingersBones;

    private Animator animator;

    private Transform[][] rightHand;
    private Transform[][] leftHand;

    private void Start()
    {
        animator = GetComponent<Animator>();


    }

    private void OnAnimatorIK(int layerIndex)
    {
        float rightHandReach = animator.GetFloat("RightHand");
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandOffset.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandOffset.rotation);
        
        float leftHandReach = animator.GetFloat("LeftHand");
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandOffset.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandOffset.rotation);
        //animator.SetIKRotation();
    }

    public void OnPressLeftPinch()
    {
        animator.SetFloat("Left Pinch", 1f);
    }

    public void OnReleaseLeftPinch()
    {
        animator.SetFloat("Left Pinch", 0f);
    }

    public void OnPressLeftGrab()
    {
        animator.SetFloat("Left Grab", 1f);
    }

    public void OnReleaseLeftGrab()
    {
        animator.SetFloat("Left Grab", 0f);
    }

    public void OnPressRightPinch()
    {
        animator.SetFloat("Right Pinch", 1f);
    }

    public void OnReleaseRightPinch()
    {
        animator.SetFloat("Right Pinch", 0f);
    }

    public void OnPressRightGrab()
    {
        animator.SetFloat("Right Grab", 1f);
    }

    public void OnReleaseRightGrab()
    {
        animator.SetFloat("Right Grab", 0f);
    }
}
