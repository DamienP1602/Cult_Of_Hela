using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputComponent : MonoBehaviour
{
    IAA_Player action;

    public InputAction LeftClick { get; private set; }
    public InputAction Inventory { get; private set; }
    public InputAction Abilities { get; private set; }
    public InputAction FirstSpellBinding { get; private set; }
    public InputAction SecondSpellBinding { get; private set; }
    public InputAction ThirdSpellBinding { get; private set; }
    public InputAction FourthSpellBinding { get; private set; }
    public InputAction Jump { get; private set; }

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
        Abilities = action.Player.Abilities;
        Jump = action.Player.Jump;
        FirstSpellBinding = action.Player.FirstSpellBinding;
        SecondSpellBinding = action.Player.SecondSpellBinding;
        ThirdSpellBinding = action.Player.ThirdSpellBinding;
        FourthSpellBinding = action.Player.FourthSpellBinding;
    }

    public void DisableInputs()
    {
        LeftClick.Disable();
        Inventory.Disable();
        Abilities.Disable();
        Jump.Disable();
        FirstSpellBinding.Disable();
        SecondSpellBinding.Disable();
        ThirdSpellBinding.Disable();
        FourthSpellBinding.Disable();
    }

    public void EnableInputs()
    {
        LeftClick.Enable();
        Inventory.Enable();
        Abilities.Enable();
        Jump.Enable();
        FirstSpellBinding.Enable();
        SecondSpellBinding.Enable();
        ThirdSpellBinding.Enable();
        FourthSpellBinding.Enable();
    }
}
