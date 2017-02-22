using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionScript : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        print("Player seen!");
        if (other.tag == "Player")
        {
            this.gameObject.GetComponentInParent<AI>().SetState("Attacking");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            this.gameObject.GetComponentInParent<AI>().SetState("Patrolling");
        }
    }
}
