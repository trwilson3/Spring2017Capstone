using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public float speed = 1.0f;
    Vector3 posVec;
    private float deathTime = 4f;
    private float count = 0f;

    // Use this for initialization
    void Start () {
        posVec = Input.mousePosition;
        posVec.z = transform.position.z - Camera.main.transform.position.z;
        posVec = Camera.main.ScreenToWorldPoint(posVec);
    }
	
	// Update is called once per frame
	    void Update ()
        {
        count += 1 * Time.deltaTime;
        if(count >= deathTime)
        {
            Destroy(this.gameObject);
        }
            transform.position += transform.forward * speed * Time.deltaTime;
            //transform.position += posVec * speed * Time.deltaTime;
        }
}
