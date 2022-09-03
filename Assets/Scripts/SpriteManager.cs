using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{   
    [SerializeField]
    private SpriteRenderer normalSprite;
    [SerializeField]
    private SpriteRenderer transitionSprite;
    [SerializeField]
    private Transform movablePivot;
    [SerializeField]
    private Transform transitionSpriteTransform;
    [SerializeField]
    private Animator transitionAnimator;
    [SerializeField]
    private Animator normalAnimator;
    [SerializeField]
    private Vector3 offsetWithTransitionSprite;

    private bool changingHeight = false;

    private float m_yFixedPos;
    private Vector3 savedPivotLocalPos;
    private Vector3 savedSpriteLocalPos;

    private void FixedUpdate()
    {
        if (changingHeight)
        {
            FixedPivotWhileMoving();
        }
    }

    private void FixedPivotWhileMoving()
    {
        movablePivot.position = new Vector3(movablePivot.position.x, m_yFixedPos, movablePivot.position.z);
        transitionSpriteTransform.position = gameObject.transform.position + offsetWithTransitionSprite;
    }

    public void OnHeightChangeStart(float yFixedPos)
    {
        Debug.Log("height change start");
        changingHeight = true;
        m_yFixedPos = yFixedPos;
        FixedPivotWhileMoving(); // place the pivot to the desired position first to avoid wrong sorting at the start
        SwitchToTransitionSprite();
        SaveLocalPosition();
    }

    public void OnHeightChangeEnd()
    {
        Debug.Log("height change end");
        SwitchToNormalSprite();
        ResumeLocalPosition();
        changingHeight = false;
    }

    private void SwitchToTransitionSprite()
    {
        transitionAnimator.Play(GetComponentInParent<GridMovement>().Facing.ToString());
        transitionSprite.enabled = true;
        GetComponentInParent<GridMovement>().SpriteRotate = transitionAnimator;
        normalSprite.enabled = false;
    }
    private void SwitchToNormalSprite()
    {
        normalAnimator.Play(GetComponentInParent<GridMovement>().Facing.ToString());
        normalSprite.enabled = true;
        GetComponentInParent<GridMovement>().SpriteRotate = normalAnimator;
        transitionSprite.enabled = false;
    }

    private void SaveLocalPosition()
    {
        savedPivotLocalPos = movablePivot.localPosition;
        savedSpriteLocalPos = transitionSpriteTransform.localPosition;
    }
    private void ResumeLocalPosition()
    {
        movablePivot.localPosition = savedPivotLocalPos;
        transitionSprite.transform.localPosition = savedSpriteLocalPos;
    }
}
