using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    public Animator Animator;
    public AnimationClip closeAnimation;
    public TextMeshProUGUI Message;
    public GameObject Visual;

    public static Notification Instance { get; private set; }

    public delegate void NotificationClosed();
    /// <summary>
    /// The event that will invoke after moving to the destination
    /// </summary>
    public event NotificationClosed NotificationClosedEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void ShowMessage(string msg)
    {
        
        Message.text = msg;
        Visual.SetActive(true);
        Animator.Play("Notification_Pop_Out");
    }

    public void CloseNotification()
    {
        Animator.Play("Notification_Pop_Back");
        StartCoroutine(WaitForCloseAnimation());
    }
    private IEnumerator WaitForCloseAnimation()
    {
        yield return new WaitForSeconds(closeAnimation.length);
        Visual.SetActive(false);

        NotificationClosedEvent?.Invoke();
    }
}
