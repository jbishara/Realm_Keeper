using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationtest : MonoBehaviour
{
    public Animator ani;

    public GameObject handpoint;
    public GameObject handIK;
    public GameObject anotherGameobject;
    public GameObject mkTar;
    public GameObject capeSecond;

    public float timer = 0f;
    public bool souldrain;

    // Start is called before the first frame update
    void Start()
    {
        //ani.SetBool("Attack", true);
        StartCoroutine("Animation");
    }

    // Update is called once per frame
    void Update()
    {


        

        //ani.SetBool("Souldraining", true);
        if (ani.GetBool("Souldrain") && true)
        {
            Debug.Log("it works");

            //handpoint
            
            //Destroy(mkTar);
        }
    }

    IEnumerator Animation()
    {
        // playing idle
        yield return new WaitForSeconds(3f);
        ani.SetBool("Idle", false);
        ani.SetBool("Walking", true);
        // starting walking
        yield return new WaitForSeconds(1f);
        ani.SetFloat("Blend", 0.33f);
        yield return new WaitForSeconds(2f);
        // running
        ani.SetFloat("Blend", 0.66f);
        yield return new WaitForSeconds(1.8f);
        // attack v1
        ani.SetBool("Walking", false);
        ani.SetBool("Attack", true);
        yield return new WaitForSeconds(1.9f);
        // attack v2
        ani.SetBool("Attack", false);
        ani.SetBool("Attackv2", true);
        yield return new WaitForSeconds(2.1f);
        // medusas kiss
        ani.SetBool("Attackv2", false);
        ani.SetBool("Medusas kiss", true);
        yield return new WaitForSeconds(2.6f);
        // deathmark
        ani.SetBool("Medusas kiss", false);
        ani.SetBool("Deathmark", true);
        yield return new WaitForSeconds(2.8f);
        yield return new WaitForSeconds(0.2f);
        // souldrain
        ani.SetBool("Deathmark", false);
        ani.SetBool("Souldrain", true);
        yield return new WaitForSeconds(2.4f);
        //coldsteel
        ani.SetBool("ColdSteel", true);
        ani.SetBool("Souldrain", false);
        yield return new WaitForSeconds(3.5f);
        yield return new WaitForSeconds(0.2f);
        //die
        ani.SetBool("ColdSteel", false);
        ani.SetBool("Die", true);
        yield return new WaitForSeconds(2f);
        Debug.Log("Animation completed");
    }
}
