using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapObject.Interactable;

public class RemovalButton : MonoBehaviour, IInteractable
{
    public GameObject[] TargetsToRemove;

    public enum MethodType { DESTROY, DISAPPEAR }
    public MethodType RemoveMethod;
    private IDictionary<MethodType, Action<GameObject>> Mapping = new Dictionary<MethodType, Action<GameObject>>();



    private void Awake()
    {
        Mapping.Add(MethodType.DESTROY, DestroyTarget);
        Mapping.Add(MethodType.DISAPPEAR, MakeTargetInvisible);
    }

    public void Interact()
    {
        Debug.Log("Interacting with button");
        Action<GameObject> method = Mapping[RemoveMethod];
        foreach (GameObject target in TargetsToRemove)
        {
            method(target);
        }
        Destroy(this); // This object can only be interacted once
    }

    public bool CheckInteractionEnd()
    {
        return true;
    }


    private void DestroyTarget(GameObject target)
    {
        Destroy(target);
    }
    private void MakeTargetInvisible(GameObject target)
    {
        target.SetActive(false);
    }

}
