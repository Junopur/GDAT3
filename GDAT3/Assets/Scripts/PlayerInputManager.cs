using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    private PlayerInputs _playerInputs;

    public event EventHandler OnFlashlightButtonPressed;
    public event EventHandler OnStunButtonPressed;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"Multiple instances of PlayerInputManager found. Deleting this one ({transform.name}).");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        _playerInputs = new PlayerInputs();
        _playerInputs.Gameplay.Enable();
        
        _playerInputs.Gameplay.StunButton.performed += StunButtonPressed;
        _playerInputs.Gameplay.ToggleFlashlight.performed += ToggleFlashlightOnPerformed;
    }
    private void ToggleFlashlightOnPerformed(InputAction.CallbackContext obj) => OnFlashlightButtonPressed?.Invoke(this, EventArgs.Empty);
    private void StunButtonPressed(InputAction.CallbackContext obj) => OnStunButtonPressed?.Invoke(this, EventArgs.Empty);
    

    public Vector2 GetMovementInput()
    {
        return _playerInputs.Gameplay.Movement.ReadValue<Vector2>();
    }
}
