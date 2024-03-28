using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_TestDoorAnimations : MonoBehaviour
{
    private bool openable;
    private Animator myAnims;
    private CapsuleCollider myCol;
    [SerializeField] private float timeToClose = 5f;
    private float curTime;
    // Start is called before the first frame update
    void Start()
    {
        curTime = 0f;
        openable = false;
        myCol = GetComponent<CapsuleCollider>();
       myCol.isTrigger = true;
        myAnims = GetComponent<Animator>();
        myAnims.SetBool("Open Door", false);
        myAnims.SetBool("Close Door", false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I was touched");
        if(other.gameObject.tag == "Player" && myAnims.GetBool("Open Door") == false)
        {
            openable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("I am alone :(");
        if(other.gameObject.tag == "Player")
        {
            openable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(openable == true && Input.GetKeyDown(KeyCode.E))
        {
            myAnims.SetBool("Open Door", true);
            myAnims.SetBool("Close Door", false);
            curTime = 0f;
        }

        if(myAnims.GetBool("Open Door") == true)
        {
            curTime += Time.deltaTime;
        }
        if(curTime >= timeToClose)
        {
            myAnims.SetBool("Open Door", false);
            myAnims.SetBool("Close Door", true);
        }
    }
}
