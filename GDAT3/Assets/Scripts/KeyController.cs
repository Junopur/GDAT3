using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    AudioSource Pickup;
    Collider Key;

    public static int totalKeys = 0;

    private void Awake()
    {
        Pickup = GetComponent<AudioSource>();
        Key = GetComponent<Collider>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected");
        if (other.gameObject.CompareTag("Key"))
        {
            Debug.Log("Object is Key");
            totalKeys += 1;
            Destroy(other.gameObject);
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
    void OnCollisionEnter(Collision other)
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
