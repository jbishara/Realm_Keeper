using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickingUpScript : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip _audioClip;
    public GameObject _particle;
    private JB_PlayerStats stats;
    private GameObject parentObj;
    public string itemName;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        stats = GetComponent<JB_PlayerStats>();
        //_particle.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<JB_PlayerStats>().SendMessage(itemName);
            parentObj = transform.parent.gameObject;
            //gameObject.GetComponent<JB_PlayerStats>().critChance += 0.10f;
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
                r.enabled = false;

            _audioSource.PlayOneShot(_audioClip);
            Destroy(parentObj);
            
            // change this to deactiveing and move them to under the scene
            //Destroy(gameObject, _audioClip.length);
        }
    }
}
