using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{   
    //[Todo] the transitionSprite will accelarate when entering the height change collider 
    [SerializeField]
    private SpriteRenderer normalSprite;
    [SerializeField]
    private SpriteRenderer transitionSprite;
    [SerializeField]
    private Transform movablePivot;
    [SerializeField]
    private Transform transitionSpriteTransform;
    private bool changingHeight = false;
    //private float m_velocity;
    [SerializeField]
    private Vector3 offsetWithTransitionSprite;
    private float m_yFixedPos;

    private Vector3 savedPivotLocalPos;
    private Vector3 savedSpriteLocalPos;
    private void Start()
    {
        Debug.Log(transitionSprite.transform.localPosition);
    }
    private void FixedUpdate()
    {
        if (changingHeight)
        {
            movablePivot.position = new Vector3(movablePivot.position.x, m_yFixedPos, movablePivot.position.z);
            //float prev_y = transitionSpriteTransform.localPosition.y;
            transitionSpriteTransform.position = gameObject.transform.position + offsetWithTransitionSprite;
            //transitionSpriteTransform.localPosition = Vector3.MoveTowards(transitionSpriteTransform.localPosition, transitionSpriteTransform.localPosition + Vector3.up, m_velocity);
            //float pos_y = transitionSpriteTransform.localPosition.y;
            //Debug.Log("Sprite on character move " + (pos_y - prev_y).ToString() + " at one frame");
        }
    }


    public void OnHeightChangeStart(float yFixedPos)
    {
        SwitchToTransitionSprite();
        SaveLocalPosition();
        changingHeight = true;
        //m_velocity = velocity;
        m_yFixedPos = yFixedPos;
    }


    public void OnHeightChangeEnd()
    {
        SwitchToNormalSprite();
        ResumeLocalPosition();
        changingHeight = false;
        //m_velocity = 0;
    }



    private void SwitchToTransitionSprite()
    {
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
