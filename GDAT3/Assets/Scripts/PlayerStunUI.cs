using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStunUI : MonoBehaviour
{
    
    [SerializeField] private PlayerStun _playerStun;
    [SerializeField] private Image _cooldownImage;

    private void Update()
    {
        _cooldownImage.fillAmount = 1 - (_playerStun.CooldownTimer / PlayerStun.CooldownSeconds);
    }

}
