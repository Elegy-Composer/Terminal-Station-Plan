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
    private Vector3 offsetWithTransitionSprite;

    private bool changingHeight = false;

    private float m_yFixedPos;
    private Vector3 savedPivotLocalPos;
    private Vector3 savedSpriteLocalPos;

    private void FixedUpdate()
    {
        if (changingHeight)
        {
            movablePivot.position = new Vector3(movablePivot.position.x, m_yFixedPos, movablePivot.position.z);
            transitionSpriteTransform.position = gameObject.transform.position + offsetWithTransitionSprite;
        }
    }

    public void OnHeightChangeStart(float yFixedPos)
    {
        Debug.Log("height change start");
        SwitchToTransitionSprite();
        SaveLocalPosition();
        changingHeight = true;
        m_yFixedPos = yFixedPos;
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
        transitionSprite.sprite = normalSprite.sprite;
        transitionSprite.enabled = true;
        normalSprite.enabled = false;
    }
    private void SwitchToNormalSprite()
    {
        normalSprite.enabled = true;
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
