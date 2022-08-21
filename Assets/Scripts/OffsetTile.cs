using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public abstract class OffsetTile : MonoBehaviour
{
    public bool IsAligned = false;

    [SerializeField]
    protected float VerticalOffset;
    [SerializeField]
    protected float HorizontalOffset;

    protected GameObject characterStepOn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsAligned) return;

        if (!PlayerStepOn(collision)) return;

        characterStepOn = collision.gameObject;
        OffsetOnStep();
        AfterStep();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsAligned) return;

        if (!PlayerStepOn(collision)) return;

        OffsetOnLeave();
        AfterLeave();
        characterStepOn = null;
    }

    private bool PlayerStepOn(Collider2D stepOn)
    {
        return stepOn.gameObject.GetComponent<GridMovement>() != null && stepOn.gameObject.GetComponent<GridMovement>().enabled;
    }
    public abstract void OffsetOnStep();
    public abstract void AfterStep();
    public abstract void OffsetOnLeave();
    public abstract void AfterLeave();
}
