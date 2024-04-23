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

    private void Update()
    {
        //SynchronizeFingerRotation();
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
    }

    private void SynchronizeFingerRotation()
    {
        rightThumbBones[0].localRotation = rightHandFingers[0].localRotation;
        rightThumbBones[1].localRotation = rightHandFingers[0].GetChild(0).localRotation;
        rightThumbBones[2].localRotation = rightHandFingers[0].GetChild(0).GetChild(0).localRotation;

        rightIndexBones[0].localRotation = rightHandFingers[1].localRotation;
        rightIndexBones[1].localRotation = rightHandFingers[1].GetChild(0).localRotation;
        rightIndexBones[2].localRotation = rightHandFingers[1].GetChild(0).GetChild(0).localRotation;

        rightFingersBones[0].localRotation = rightHandFingers[2].localRotation;
        rightFingersBones[1].localRotation = rightHandFingers[2].GetChild(0).localRotation;
        rightFingersBones[2].localRotation = rightHandFingers[2].GetChild(0).GetChild(0).localRotation;

        leftThumbBones[0].localRotation = leftHandFingers[0].localRotation;
        leftThumbBones[1].localRotation = leftHandFingers[0].GetChild(0).localRotation;
        leftThumbBones[2].localRotation = leftHandFingers[0].GetChild(0).GetChild(0).localRotation;

        leftIndexBones[0].localRotation = leftHandFingers[1].localRotation;
        leftIndexBones[1].localRotation = leftHandFingers[1].GetChild(0).localRotation;
        leftIndexBones[2].localRotation = leftHandFingers[1].GetChild(0).GetChild(0).localRotation;

        leftFingersBones[0].localRotation = leftHandFingers[2].localRotation;
        leftFingersBones[1].localRotation = leftHandFingers[2].GetChild(0).localRotation;
        leftFingersBones[2].localRotation = leftHandFingers[2].GetChild(0).GetChild(0).localRotation;
    }
}
