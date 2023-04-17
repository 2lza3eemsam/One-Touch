using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
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
    public static bool BLOCKED2 = false;
    // Start is called before the first frame update
    void Start()
    {   
         Anim = GetComponentInChildren<Animator>();

        // To get the minimum and maximum allowed for the x-axis
        newRangeMin = transform.localPosition.x - xRange;
        newRangePlus = transform.localPosition.x + xRange;
        //to bring back the player to it's supposed position
        transform.localPosition = new Vector3(newRangePlus - 1, transform.localPosition.y, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {       
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
        horizontalInput = Input.GetAxis("Horizontal2");
       // Anim.SetFloat("Forward2", horizontalInput);
       // transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
        if (Input.GetAxis("Horizontal2") > 0){
            
           

            Anim.SetBool("Forward", true);
           // Anim.SetFloat("Forward2", horizontalInput);
            transform.Translate(Vector3.back * horizontalInput * Time.deltaTime * speed);
            
        } 
        
        if (Input.GetAxis("Horizontal2") < 0) {
            if (CanWalkLeft == true){
            Anim.SetBool("Backward", true);
            transform.Translate(Vector3.back * horizontalInput * Time.deltaTime * speed);
          //  transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
            }
        }
        if (Input.GetAxis("Horizontal2") == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }
        //punching
        if(Input.GetButtonDown("FireP2"))
        {
            Anim.SetTrigger("Punch");
        }
        if(Input.GetButtonDown("BlockP2"))
        {
            Anim.SetTrigger("Block");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("P1Wall")){
            CanWalkLeft = false;
        }
        if (other.CompareTag("P1HitBox"))
        {
            Anim.SetTrigger("KnockOut");
        }
      /*  if (other.CompareTag("P1Block"))
        {
            PlayerController.BLOCKED = true;
        } else {
            PlayerController.BLOCKED = false;
        }
    */
    }
    private void OnTriggerExit(Collider other)
    {
         if (other.CompareTag("P1Wall")){
            CanWalkLeft = true;
        }
       /* if (other.CompareTag("P1Block"))
        {
            PlayerController.BLOCKED = false;
        }
        */
    }
}
