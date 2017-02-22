using UnityEngine;
using System.Collections;

public class HitDetection : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "laser")
        {
            Destroy(other.gameObject);
            print("Laser hit");
        }
    }
}
