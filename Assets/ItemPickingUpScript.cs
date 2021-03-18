﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickingUpScript : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip _audioClip;
    public GameObject _particle;
    private JB_PlayerStats stats;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        stats = GetComponent<JB_PlayerStats>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameObject.GetComponent(JB_PlayerStats.)
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
                r.enabled = false;

            _audioSource.PlayOneShot(_audioClip);
            // change this to deactiveing and move them to under the scene
            Destroy(gameObject, _audioClip.length);
        }
    }
}
