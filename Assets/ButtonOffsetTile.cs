using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOffsetTile : OffsetTile 
{
    public SpriteRenderer NormalRenderer;
    public SpriteRenderer StepOnRenderer;

    private enum ButtonType
    {
        ONETIME, REUSABLE
    }
    [SerializeField]
    private ButtonType buttonType;

    public override void OffsetOnStep()
    {
        characterStepOn.GetComponent<PointFollower>()?.UpdateTargetBy(new Vector3(HorizontalOffset, VerticalOffset, 0));
        Debug.Log("Step on");
    }
    public override void AfterStep()//since there's no button sprite for being step on, just for representation
    {
        NormalRenderer.enabled = false;
        StepOnRenderer.enabled = true;
        characterStepOn.GetComponent<PointFollower>()?.UpdateTargetBy(new Vector3(-HorizontalOffset, -VerticalOffset, 0));
    }
    public override void OffsetOnLeave()
    {
        if (buttonType == ButtonType.REUSABLE)
        {
            StepOnRenderer.enabled = false;
            NormalRenderer.enabled = true;
        }
        else//One Time
        {
            IsAligned = true;
        }
    }
}
