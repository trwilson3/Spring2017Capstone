using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharManager : MonoBehaviour {

    public static int fuelCells;
    //[FOR GUN USAGE]
    [SerializeField]
    private GameObject shootFX;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private AudioSource laserShot;
    [SerializeField]
    Image crosshair;
    private bool _canShoot = true;
    [SerializeField]
    Camera mc;
    Vector3 center = Vector3.zero;
    int min = -40, max = 40;
    float rotateSpeed = 70f;

    void Start()
    {
        mc.transform.localEulerAngles = center;
    }

    public static void printFuelCells()
    {
        print("Total fuel cells: " + fuelCells);
    }

    void Update()
    {
        center.y += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        center.x -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        center.x = Mathf.Clamp(center.x, min, max);
        center.y = Mathf.Clamp(center.y, min, max);
        mc.transform.localEulerAngles = center;
       // crosshair.transform.position = Input.mousePosition;
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {

            /*  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              RaycastHit hit;
              if (Physics.Raycast(ray, out hit))
              {
                  Vector3 pos = shootFX.transform.position;
                  Vector3 dir = hit.point - pos;
                  Instantiate(laserPrefab, pos, Quaternion.LookRotation(dir));
                 // laserShot.Play();
              }
          */
            Vector3 pos = shootFX.transform.position;
            Instantiate(laserPrefab, pos, Quaternion.LookRotation(mc.transform.forward));
        }
    }
}
