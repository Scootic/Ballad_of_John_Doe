using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(AudioSource))]
public abstract class BASE_Pickup : MonoBehaviour
{
    private Collider myCol;
    private Animation myAnim;
    [SerializeField] private AudioSource pickupSFX;
    [SerializeField] private AudioSource ambientSFX;
  
    protected void Start()
    {
        myCol = GetComponent<Collider>();
        myAnim = GetComponent<Animation>();
        ambientSFX = GetComponent<AudioSource>();

        myCol.isTrigger = true;
        myAnim?.Play();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (myAnim != null && myAnim.isPlaying == false) //basic loop logic for the animation and ambientSFX (assuming they exist, of course)
        {
            myAnim.Play();
        }
        if (ambientSFX != null && ambientSFX.isPlaying == false && myCol.enabled == true)
        {
            ambientSFX.Play();
        }
    }

    //should change sound effect behaviors if we get an audio manager implemented.

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (pickupSFX != null)
            {
                pickupSFX.Play();
            }
            if (ambientSFX != null)
            {
                ambientSFX.Stop();
            }
            PickupBehavior(other);
            if (this.gameObject.GetComponent<MeshRenderer>() != null) {
                this.gameObject.GetComponent<MeshRenderer>().enabled = false; 
            }//hide the gameobject while still letting logic run, if necessary
            myCol.enabled = false;

            if(this.gameObject.transform.childCount > 0) //kill children?
            {
                foreach(Transform gum in this.gameObject.transform)
                {
                    gum.gameObject.SetActive(false);
                }
            }
        }
    }

    protected abstract void PickupBehavior(Collider collider); //this the mf you override to make the pickups do things
}
