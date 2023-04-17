using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2ScriptedAI : MonoBehaviour
{
   public float horizontalInput;
    public float speed = 10.0f;

    public float xRange = 7;
    //the range behind the player
    public float newRangeMin;
    //the range infront of the player
    public float newRangePlus;
    private Animator Anim;

    public GameObject Opponent;
    public GameObject Player1;

    public static bool CanWalkRight = true;
    public static bool CanWalkLeft = true;
    public static bool BLOCKED2 = false;
    public float OppDistance;
    public float walkspeed = 1;
    public float attackDistance = 1.5f;
    private bool MoveAI = true;
    public static bool AttackState = false;
    public static bool HitsAI = false;
    private int AttackNumber = 1;
    private bool Attacking = true;
    public float AttackRate = 1.0f;
    public static bool lose = false;
    

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
        OppDistance = Vector3.Distance(Opponent.transform.localPosition, Player1.transform.localPosition);
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
       // horizontalInput = 1f;
       // Anim.SetFloat("Forward2", horizontalInput);
       // transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
        if (OppDistance > attackDistance){
            if (MoveAI == true)
            {
            Anim.SetBool("Forward", true);
            Anim.SetBool("Backward", false);
            AttackState = false;
            Anim.SetBool("CanAttack", false);
           // Anim.SetFloat("Forward2", horizontalInput);
            transform.Translate(Vector3.forward * walkspeed * Time.deltaTime * speed);
            }
        } 
        
        if (OppDistance < attackDistance) {
          
                if (MoveAI == true){
                    MoveAI = false;
                
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
            StartCoroutine(ForwardFalse());
            Anim.SetBool("CanAttack", true);
          //  transform.Translate(Vector3.back * horizontalInput * Time.deltaTime * speed);
          //  transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
          //transform.Translate(Vector3.back * walkspeed * Time.deltaTime * speed);
            
                }
               
        }
        
        //punching
        if (Attacking == true)
        {
            Attacking = false;
        
        if(AttackNumber == 1)
        {
            Anim.SetTrigger("Punch");
            HitsAI = false;
        }
        if(AttackNumber == 2)
        {
            Anim.SetTrigger("Block");
            HitsAI = false;
        }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("P1Wall")){
            CanWalkLeft = false;
        }
        if (other.CompareTag("P1HitBox"))
        {
           // Anim.SetTrigger("KnockOut");
            lose = true;

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
    public void RandomAttack()
    {
        Debug.Log(AttackNumber);
       // if(AttackState == true)
       // {
            AttackNumber = Random.Range(1,3);
            StartCoroutine(SetAttacking());
       // }
    }
    IEnumerator ForwardFalse()
    {
        yield return new WaitForSeconds(0.6f);
        MoveAI = true;
    }

    IEnumerator SetAttacking()
    {
        yield return new WaitForSeconds(AttackRate);
        Attacking = true;
    }
}
