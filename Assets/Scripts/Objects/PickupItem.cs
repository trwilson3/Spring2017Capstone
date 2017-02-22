using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) //Might need to be changed if using 2D physics
    {
        print("Entered");
        if(other.CompareTag("Player"))
        {
            if(this.gameObject.tag == "FuelCells")
            {
                print("Fuel cells before change: " + CharManager.fuelCells);
                CharManager.fuelCells += 10;
                CharManager.printFuelCells();
                Destroy(this.gameObject);
            }
        }
    }

}
