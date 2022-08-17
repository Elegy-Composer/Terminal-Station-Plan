using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOffsetTile_v2 : OffsetTile
{
    public GameObject sprite;

    public Transform Raised;
    public Transform Pressed;
    private Vector3 targetPosition;
    //private bool moving = false;
    [SerializeField]
    private float pressingSpeed;

    private enum ButtonType
    {
        ONETIME, REUSABLE
    }
    [SerializeField]
    private ButtonType buttonType;

    private void Awake()
    {
        targetPosition = sprite.transform.position;
    }

    public override void OffsetOnStep()
    {
        characterStepOn.GetComponent<PointFollower>()?.UpdateTargetBy(new Vector3(HorizontalOffset, VerticalOffset, 0));
    }
    public override void AfterStep() 
    {
        targetPosition = Pressed.position;
    }
    public override void OffsetOnLeave()
    {
        return;
    }
    public override void AfterLeave()
    {
        if (buttonType == ButtonType.REUSABLE)
        {
            targetPosition = Raised.position;
        }
        else//One Time
        {
            IsAligned = true;
        }
    }

    void FixedUpdate()
    {
        //if (Vector3.Distance(sprite.transform.position, targetPosition) == 0f)
        //{
        //    if (moving)
        //    {
        //        moving = false;
        //        if (characterStepOn != null)
        //        {
        //            characterStepOn.GetComponent<GridMovement>().enabled = true;
        //            characterStepOn.GetComponent<PointFollower>().enabled = true;
        //        }
        //    }
        //}
        //else
        //{
        //moving = true;
        Vector3 movement = Vector3.MoveTowards(sprite.transform.position, targetPosition, pressingSpeed) - sprite.transform.position;
        sprite.transform.position += movement;
        if (characterStepOn != null)
        {
            //characterStepOn.GetComponent<PointFollower>().enabled = false;
            //characterStepOn.GetComponent<GridMovement>().enabled = false;
            characterStepOn.transform.position += movement;
            characterStepOn.GetComponent<PointFollower>().UpdateTargetBy(movement);
        }
        //}
    }
}
