using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TEMP_SPEEDPOWERUP : MonoBehaviour
{
    
    private Collider myCol;
    [SerializeField] private float timeToRespawn;
    private float curTime;
   
    // Start is called before the first frame update
    void Start()
    {
        curTime = 0f;
        myCol = this.GetComponent<Collider>();
        myCol.isTrigger = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myCol.enabled == false)
        {
            curTime += Time.deltaTime;
            if(curTime >= timeToRespawn)
            {
                Debug.Log("I'm back!");
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;
                myCol.enabled = true;
                curTime = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Picked up powerup");
            //Player.GetComponent<PlayerMovement>(). RUN A SPEED UPGRADE METHOD HERE!
        }

        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        myCol.enabled = false;
    }
}
