using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LD_healingburstParticles : MonoBehaviour
{
    public ParticleSystem healingburstParticle;

    void activateHealingBurst()
    {
        healingburstParticle.Play();
        Debug.Log("I ran");
    }
}