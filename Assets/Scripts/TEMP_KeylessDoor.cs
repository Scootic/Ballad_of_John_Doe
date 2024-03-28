using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Collider))]
public class TEMP_KeylessDoor : MonoBehaviour
{
    private bool canOpen;
    [SerializeField] private float timeToclose = 5f;
    private Collider mycol;
    private float curTime;
    private Animation myAnim;
    private bool isOpen;
    private bool inTrigger;
   
    void Start()
    {
        //get everything set up.
        myAnim = this.GetComponent<Animation>();
        mycol = this.GetComponent<Collider>();
        mycol.isTrigger = true;
        canOpen = false;
        isOpen = false;
        curTime = 0f;
    }

   
    void Update()
    {
        if (myAnim != null)//check if there are any animations at all, do nothing if not
        {
            if (canOpen == true && Input.GetKeyDown(KeyCode.E)) //open the door
            {
                myAnim[myAnim.clip.name].time = 0f;
                myAnim[myAnim.clip.name].speed = 1f;
                myAnim.Play();
                
                canOpen = false;
                isOpen = true;
            }
            if (isOpen == true && curTime < timeToclose && inTrigger == false) //timer logic
            {
                curTime += Time.deltaTime;
                canOpen = false;
                Debug.Log("Door is open, time spent open: " + curTime);
            }
            else if (isOpen == true && curTime >= timeToclose && inTrigger == false) //once timer is met, close the door
            {
                Debug.Log("Should be rewinding!");
                curTime = 0f;
                myAnim[myAnim.clip.name].time = myAnim.clip.length;
                myAnim[myAnim.clip.name].speed = -1f;
                myAnim.Play();
               
               
                isOpen = false;
                canOpen = false; //canOpen should only be turned true by trigger enter. may be a bit awkward.
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (isOpen == false)
            {
                canOpen = true;
            }
            inTrigger = true; //important to make sure door doesn't close on the player (unintentionally)
            curTime = 0f;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canOpen = false;
            inTrigger = false;
        }
    }
}
