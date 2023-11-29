using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    private void RangeAttack()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
                }
            }
        }
    }
}
