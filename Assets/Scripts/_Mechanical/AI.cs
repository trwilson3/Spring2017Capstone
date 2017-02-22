using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private AudioSource pew;
    [SerializeField]
    private float skillLevel = 1; //0 = Sniper, .7 pretty dang accurate
    [SerializeField]
    private int fireRateAmnt;
    [SerializeField]
    private float heightAdjustment = 0f;
    private enum CONDITIONS{Patrolling,Attacking,Cautious};
    private CONDITIONS state;
    private float Attck_timer = 0f;
    private Vector3 pos;
    private bool canShoot = true;
    public int viewSize = 40;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        state = CONDITIONS.Patrolling;
	}
	
	// Update is called once per frame
	void Update ()
    { 

        if (state == CONDITIONS.Attacking)
        {
            Attck_timer += 1 * Time.deltaTime;
            if(Attck_timer >= 3f)
            {
                Attck_timer = 0f;
                fire(fireRateAmnt);
            }
        }
	}

    public void SetState(string s)
    {
        print("Attempting to change state to " + s);
        if (s.Equals("Patrolling"))
        {
            state = CONDITIONS.Patrolling;
        }
        if (s.Equals("Attacking"))
        {
            state = CONDITIONS.Attacking;
        }
        if (s.Equals("Cautious"))
        {
            state = CONDITIONS.Cautious;
        }
    }

    private void fire(int amnt)
    {
        StartCoroutine(FireShots(amnt));
    }

    private IEnumerator FireShots(int num)
    {
        for (int i = 0; i < num; i++)
        {
            pos = gun.transform.position;
            Instantiate(laser, pos, Quaternion.LookRotation(player.transform.position - pos + new Vector3(0+(Random.value*skillLevel), heightAdjustment, 0 + (Random.value * skillLevel))));
            //pew.Play();
            yield return new WaitForSeconds(.1f);
        }
    }
}
