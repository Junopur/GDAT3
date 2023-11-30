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
    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
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
            
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Is Hit");
                
                var enemy = hit.transform.GetComponent<AIController>();
                enemy.DoStun(stunDuration);

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