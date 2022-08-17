using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOffsetTile_v2 : OffsetTile
{


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
        characterStepOn.GetComponent<PointFollower>()?.UpdateTargetBy(new Vector3(-HorizontalOffset, -VerticalOffset, 0));
    }
    public override void OffsetOnLeave()
    {
        if (buttonType == ButtonType.REUSABLE)
        {
        }
        else//One Time
        {
            IsAligned = true;
        }
    }
}
