using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviour
{
    [Tooltip("This can only be changed before entering playmode")]
    public bool singlePlayer = false;
}
