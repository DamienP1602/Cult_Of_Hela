using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputComponent : MonoBehaviour
{
    IAA_Player action;

    public InputAction LeftClick { get; private set; }
    public InputAction FirstSpellBinding { get; private set; }

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
        FirstSpellBinding = action.Player.FirstSpellBinding;
    }

    public void DisableInputs()
    {
        LeftClick.Disable();
        FirstSpellBinding.Disable();
    }

    public void EnableInputs()
    {
        LeftClick.Enable();
        FirstSpellBinding.Enable();
    }
}
