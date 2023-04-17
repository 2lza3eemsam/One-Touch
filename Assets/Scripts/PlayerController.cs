using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float horizontalInput;
    public float speed = 10.0f;

    public float xRange = 7;
    //the range behind the player
    public float newRangeMin;
    //the range infront of the player
    public float newRangePlus;
    private Animator Anim;

    public static bool CanWalkRight = true;
    public static bool CanWalkLeft = true;
    public static bool BLOCKED = false;
    // Start is called before the first frame update
    void Start()
    {   
         Anim = GetComponentInChildren<Animator>();

        // To get the minimum and maximum allowed for the x-axis
        newRangeMin = transform.localPosition.x - xRange;
        newRangePlus = transform.localPosition.x + xRange;
        //to bring back the player to it's supposed position
        transform.localPosition = new Vector3(newRangeMin + 1, transform.localPosition.y, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {       
        //Debug.Log(CanWalk);
        //Comparing the position of the character to the allowed range of movement
        //blocks him from exceeding the allowed range
        if (transform.localPosition.x < newRangeMin)
        {
            transform.localPosition = new Vector3(newRangeMin, transform.localPosition.y, transform.localPosition.z);
        }
        
        if (transform.localPosition.x > newRangePlus)
        {
            transform.localPosition = new Vector3(newRangePlus, transform.localPosition.y, transform.localPosition.z);
        }

        //Movement Left and right
        horizontalInput = Input.GetAxis("Horizontal");
       // Anim.SetFloat("Forward2", horizontalInput);
       // transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
        if (Input.GetAxis("Horizontal") > 0){
            
            if (CanWalkRight == true){

            Anim.SetBool("Forward", true);
           // Anim.SetFloat("Forward2", horizontalInput);
            transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
            }
        } 
        
        if (Input.GetAxis("Horizontal") < 0) {
            Anim.SetBool("Backward", true);
            transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
          //  transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }
        //punching
        if(Input.GetButtonDown("Fire1"))
        {
            Anim.SetTrigger("Punch");
        }
        if(Input.GetButtonDown("Fire2"))
        {
            Anim.SetTrigger("Block");
            BLOCKED = true;
        } 
        
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Body Block")) // check if "bash" is playing...
             {
                 BLOCKED = true; // "bash" is playing so no more code will be executed
             }
             else
             {
                 BLOCKED = false; // "bash" is NOT playing -> walk
             }
   
    Debug.Log(BLOCKED);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("P2HitBox"))
        {   
            //Add either box collider for blocking or a timer in which it switches the blocking state to TRUE and a switch to make it return to FALSE.
            if (BLOCKED == false)
            {
        Anim.SetTrigger("Knockout");
            }
        }
        if (other.CompareTag("P2Wall")){
            CanWalkRight = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
         if (other.CompareTag("P2Wall")){
            CanWalkRight = true;
        }
    }
    
}
