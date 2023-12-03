using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private AudioSource Pickup;
    private Collider Key;

    public static int totalKeys = 0;

    [SerializeField] TextMeshProUGUI Objective;

    private void Awake()
    {
        Pickup = GetComponent<AudioSource>();
        Key = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected");
        if (other.gameObject.CompareTag("Key"))
        {
            Debug.Log("Object is Key");
            totalKeys += 1;
            Destroy(other.gameObject);
            Objective.text = "Escape";
        }

        if (other.gameObject.CompareTag("Door"))
        {
            if (totalKeys > 0)
            {
                totalKeys -= 1;
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("You need a key to open this door.");
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (totalKeys > 0)
            {
                totalKeys -= 1;
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("You need a key to open this door.");
            }
        }
    }
}
