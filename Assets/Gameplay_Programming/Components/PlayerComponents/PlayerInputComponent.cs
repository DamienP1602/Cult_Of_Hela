using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputComponent : MonoBehaviour
{
    IAA_Player action;

    public InputAction LeftClick { get; private set; }
    public InputAction Inventory { get; private set; }
    public InputAction FirstSpellBinding { get; private set; }
    public InputAction SecondSpellBinding { get; private set; }

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
        Inventory = action.Player.Inventory;
        FirstSpellBinding = action.Player.FirstSpellBinding;
        SecondSpellBinding = action.Player.SecondSpellBinding;
    }

    public void DisableInputs()
    {
        LeftClick.Disable();
        Inventory.Disable();
        FirstSpellBinding.Disable();
        SecondSpellBinding.Disable();
    }

    public void EnableInputs()
    {
        LeftClick.Enable();
        Inventory.Enable();
        FirstSpellBinding.Enable();
        SecondSpellBinding.Enable();
    }
}
