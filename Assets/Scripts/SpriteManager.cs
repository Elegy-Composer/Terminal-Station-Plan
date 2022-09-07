using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
    public int SortingPriority;

    [Header("Not necessary")]
    [SerializeField]
    private Animator transitionAnimator;
    [SerializeField]
    private Animator normalAnimator;
    
    private bool changingHeight = false;
    private GameObject currentDetector;

    private Vector3 offsetWithTransitionSprite;
    private string m_sortingLayerName;
    private float m_yFixedPos;
    private Vector3 savedPivotLocalPos;
    private Vector3 savedSpriteLocalPos;

    private void Awake()
    {
        savedPivotLocalPos = movablePivot.localPosition;
        savedSpriteLocalPos = transitionSpriteTransform.localPosition;
        offsetWithTransitionSprite = transitionSpriteTransform.position - gameObject.transform.position;
    }

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

    public void OnHeightChangeStart(float yFixedPos, string sortingLayerName, GameObject detector)
    {
        if (changingHeight)
        {
            OnHeightChangeEnd(currentDetector); // start the next height changing process as soon as enter next detector
        }
        Debug.Log("height change start");
        changingHeight = true;
        currentDetector = detector;
        m_sortingLayerName = sortingLayerName;
        m_yFixedPos = yFixedPos;
        FixedPivotWhileMoving(); // place the pivot to the desired position first to avoid wrong sorting at the start
        SwitchToTransitionSprite();
    }

    public void OnHeightChangeEnd(GameObject detector)
    {
        if (detector != currentDetector) return;
        Debug.Log("height change end");
        SwitchToNormalSprite();
        ResumeLocalPosition();
        changingHeight = false;
    }

    private void SwitchToTransitionSprite()
    {
        if (GetComponentInParent<GridMovement>() != null)
        {
            transitionAnimator.Play(GetComponentInParent<GridMovement>().Facing.ToString());
            GetComponentInParent<GridMovement>().SpriteRotate = transitionAnimator;
        }
        movablePivot.GetComponent<SortingGroup>().sortingLayerID = SortingLayer.NameToID(m_sortingLayerName);
        transitionSprite.enabled = true;
        normalSprite.enabled = false;
    }
    private void SwitchToNormalSprite()
    {
        if (GetComponentInParent<GridMovement>() != null)
        {
            normalAnimator.Play(GetComponentInParent<GridMovement>().Facing.ToString());
            GetComponentInParent<GridMovement>().SpriteRotate = normalAnimator;
        }
        normalSprite.enabled = true;
        transitionSprite.enabled = false;
    }

    private void ResumeLocalPosition()
    {
        movablePivot.localPosition = savedPivotLocalPos;
        transitionSpriteTransform.localPosition = savedSpriteLocalPos;
    }
}
