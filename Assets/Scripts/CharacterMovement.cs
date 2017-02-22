using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    //Variables
    public GameObject shootFX;
    public GameObject laserPrefab;
    public AudioSource laserShot;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float _health = 100;
    public float _armor = 100;
    private int rotationAdjuster = 25;
    private bool _dead = false;
    private bool _using = false;
    private bool _canShoot = true;
    public bool _canDive = true;
    public Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    //Turn on autorun anim for sprint

        /*
        WASD + Arrow keys = Movement
        Spacebar = Dive
        Left Control = Sprint
        Right Mouse = Aim
        Return = Pickup
        T = Action Key
        --Ask Erica about custom inputs for dynamic keys
        */

    void Update()
    {
        //Release Commands
        if (_health > 0.0f)
        {
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                anim.SetBool("F_W_A", false);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                this.gameObject.transform.Rotate(0, -rotationAdjuster * Time.deltaTime, 0);
            }
            if(Input.GetKey(KeyCode.E))
            {
                this.gameObject.transform.Rotate(0, rotationAdjuster * Time.deltaTime, 0);
            }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                anim.SetBool("R_W_A", false);
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                anim.SetBool("L_W_A", false);
            }
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                anim.SetBool("B_W_A", false);
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                anim.SetBool("Sprint", false);
            }
            //Use Key
            if(Input.GetKeyDown(KeyCode.T))
            {
                if (!_using)
                {
                    _using = true;
                    anim.SetTrigger("Use");
                    StartCoroutine(useDelay(3));
                }
            }
            //Pickup
            if(Input.GetKeyDown(KeyCode.Return))
            {
                //Prox check maybe?
                anim.SetTrigger("Pickup");
            }
            //Left Mouse Button Down

            //Right Mouse Button Down
            if (Input.GetMouseButtonDown(1))
            {
                anim.SetBool("Aiming", true);
            }
            if (Input.GetMouseButtonUp(1))
            {
                anim.SetBool("Aiming", false);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                anim.SetBool("Sprint", true);
            }
            if (Input.GetKey(KeyCode.Space) && _canDive)
            {
                _canDive = false;
                _canShoot = false;
                StartCoroutine(diveDelay(1f)); //Prevent duplicate jumps
                anim.SetTrigger("Roll");
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))// && anim.GetBool("Aiming")
            {
                anim.SetBool("F_W_A", true);
            }
            //Right Commands
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("R_W_A", true);
            }
            //Left Command
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("L_W_A", true);
            }
            //Down Command
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("B_W_A", true);
            }
        }
        else if (_health < 0.1f)
        {
            onDeath();
        }
    }

    private void shoot()
    {
        print("pew pew");
    }

    private IEnumerator useDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        _using = false;
        anim.SetTrigger("StopUse");
    }

    private IEnumerator diveDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canDive = true;
        _canShoot = true;
    }

    private void onDeath()
    {
        if(!_dead)
        {
            _dead = true;
            anim.SetBool("Dead", true);
        }
    }
}
