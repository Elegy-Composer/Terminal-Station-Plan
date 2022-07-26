using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCircle : MonoBehaviour
{
    private Animator[] animators;
    [Header("please ensure that only one active gameobject between Collider1 and Collider2")]
    public GameObject Collider1;
    private bool collider1Active;
    public GameObject Collider2;
    private bool collider2Active;
    private int _characterWithin = 0;
    private int characterWithIn 
    {
        get
        {
            return _characterWithin;
        }
        set
        {
            if (value == 0) Hide();
            else Show();
            _characterWithin = value;
        }
    }

    private void Awake()
    {
        animators = GetComponentsInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<GridMovement>() != null)
        {
            characterWithIn += 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<GridMovement>() != null)
        {
            characterWithIn -= 1;
        }
    }

    private void Show()
    {
        foreach (Animator animator in animators)
        {
            animator.SetBool("LightOn", true);
        }
    }

    private void Hide()
    {
        foreach (Animator animator in animators)
        {
            animator.SetBool("LightOn", false);
        }
    }

    //below is not tested, since it need to integrate with move platform

    /// <summary>
    /// before platform starts moving, call this method to close collision detection
    /// , so no light circles will appear during platform moving
    /// </summary>
    public void DisableLightCircle() 
    {
        collider1Active = Collider1.activeInHierarchy;
        Collider1.SetActive(false);
        collider2Active = Collider2.activeInHierarchy;
        Collider2.SetActive(false);
    }
    /// <summary>
    /// after platform move to its goal, call this method to let the light circles can appear again.
    /// </summary>
    public void EnableLightCircle()
    {
        Collider1.SetActive(collider1Active);
        Collider2.SetActive(collider2Active);
    }
    /// <summary>
    /// call this method whenever platform finish its route, e.g.A->B, B->A
    /// </summary>
    public void SwitchCollider()
    {
        Collider1.SetActive(!Collider1.activeInHierarchy);
        Collider2.SetActive(!Collider2.activeInHierarchy);
    }

}
