using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStun : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green, 10, false);
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<NavMeshAgent>().isStopped = true;
                    Debug.Log("Enemy Is Hit");
                }
                else
                {
                    Debug.Log("Enemy Not Hit");
                }
            }
        }
    }
}