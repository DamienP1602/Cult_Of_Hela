using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputComponent : MonoBehaviour
{
    IAA_Player action;

    public InputAction LeftClick { get; private set; }

    private void Awake()
    {
        action = new IAA_Player();

    }

    private void OnEnable()
    {
        InitInputs();
        EnableInputs();
    }

    private void OnDisable()
    {
        DisableInputs();
    }

    void InitInputs()
    {
        LeftClick = action.Player.LeftClick;
    }

    public void DisableInputs()
    {
        LeftClick.Disable();
    }

    public void EnableInputs()
    {
        LeftClick.Enable();
    }
}
