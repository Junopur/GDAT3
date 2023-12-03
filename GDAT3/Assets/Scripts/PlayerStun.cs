using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerStun : MonoBehaviour
{
    [SerializeField] private GameObject stunBlastPrefab; // The stun blast prefab
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
        PlayerInputManager.Instance.OnStunButtonPressed += OnStunButtonPressed;
    }

    private void Update()
    {
        if (CooldownTimer > 0)
            CooldownTimer -= Time.deltaTime;
    }

    private void StartCooldown()
    {
        CooldownTimer = CooldownSeconds;
    }
    
    // Runs when the inputmanager calls the stun button event
    private void OnStunButtonPressed(object sender, EventArgs e)
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
            
            // Spawn the stun blast
            var stunBlast = Instantiate(stunBlastPrefab, hit.point, Quaternion.identity);
            Destroy(stunBlast, 3f);
            
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