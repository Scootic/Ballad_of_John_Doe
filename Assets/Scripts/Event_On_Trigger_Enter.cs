using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider))]
public class Event_On_Trigger_Enter : MonoBehaviour
{
    [SerializeField] private UnityEvent mevent;
    [SerializeField] private bool playerOnly = true;
    private Collider me;

    private void Start()
    {
        me = GetComponent<Collider>();
        me.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(playerOnly == true) { if(other.tag == "Player") { mevent.Invoke(); } } else { mevent.Invoke(); }

    }

}
