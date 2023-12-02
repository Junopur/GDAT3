using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerStun : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask; // Layers to fire the ray on
    [SerializeField] private float stunDuration = 5f; // Time to stun for.
    private Camera _camera; // Initialized in awake because Camera.main is expensive
    public const float CooldownSeconds = 10f;
    
    public float CooldownTimer { get; private set; } = 0f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        CooldownTimer = 10f;
    }

    private void Update()
    {
        if (CooldownTimer > 0)
            CooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CooldownTimer > 0)
            {
                Debug.Log("Stun is on cooldown");
            }
            else
            {
                StunBlast();   
            }
        }
    }

    private void StartCooldown()
    {
        CooldownTimer = CooldownSeconds;
    }
    
    /// <summary>
    /// Perform the Stun Blast
    /// </summary>
    private void StunBlast()
    {
        const int RAY_DISTANCE = 10;
        
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * RAY_DISTANCE, Color.red, 10, false);
        
        if (Physics.Raycast(ray, out RaycastHit hit, RAY_DISTANCE, enemyLayerMask, QueryTriggerInteraction.Collide))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green, 10, false);
            Debug.Log($"Hit: {hit.transform.name}");
            
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Is Hit");
                
                var enemy = hit.transform.GetComponent<AIController>();
                enemy.DoStun(stunDuration);
                
                StartCooldown(); // start stun cooldown
            }
            else
            {
                Debug.Log("Something was hit, but not the enemy");
            }
        }
        else
        {
            Debug.Log("Nothing was hit.");
        }
    }
}