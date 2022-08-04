using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class CharacterInputManager : MonoBehaviour
{
    private bool isSinglePlayer
    {
        get
        {
            return GameObject.Find("PlayerMode").GetComponent<PlayerMode>().singlePlayer;
        }
    }
    private PlayerInput input;
    private GridMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<GridMovement>();

        //force player to pair with current keyboard
        InputUser.PerformPairingWithDevice(Keyboard.current, user: input.user, options: InputUserPairingOptions.ForcePlatformUserAccountSelection);

        if (isSinglePlayer)
        {
            if (gameObject.name.Contains("2P"))
            {
                input.SwitchCurrentActionMap("1PGridMovement");
                movement.enabled = false;
            }
        }
    }

    public void OnSwitch()
    {
        if (!isSinglePlayer) return;
        movement.enabled = !movement.enabled;
    }
}
