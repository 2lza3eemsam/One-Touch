using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
 //using UnityEngine.SceneManagement;


public class MoveToGoal : Agent
{   
    public float xRange = 7;
    //the range behind the player
    public float newRangeMin;
    //the range infront of the player
    public float newRangePlus;
    private Animator Anim;
    public float walkspeed = 1.0f;
    public float speed = 10.0f;
    public static bool losep1 = false;
    public GameObject Opponent;
    
    public static bool CanWalkRight = true;


    void Start(){
        Anim = GetComponentInChildren<Animator>();
        newRangeMin = transform.localPosition.x - xRange;
        newRangePlus = transform.localPosition.x + xRange;
    }
    public override void OnEpisodeBegin()
    {
        

        transform.localPosition = new Vector3(newRangeMin - 1, transform.localPosition.y, transform.localPosition.z);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // To get the minimum and maximum allowed for the x-axis
        //to bring back the player to it's supposed position
      //  Instantiate(Opponent);
      targetTransform.localPosition = new Vector3(newRangePlus - 1, transform.localPosition.y, transform.localPosition.z);
        losep1 = false;
        P2ScriptedAI.lose = false;
       // Instantiate(Opponent);
    }

    [SerializeField] private Transform targetTransform;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions){
            //Write the actions that the agent will do like walking and attacking.;
        float moveX = actions.ContinuousActions[0];
        if (moveX > 0){

        if (CanWalkRight == true){
        transform.localPosition += new Vector3(moveX,0,0) * walkspeed * Time.deltaTime * speed;
        Anim.SetFloat("Forward3", moveX);
                                }
        }
        if (moveX < 0){

        
        transform.localPosition += new Vector3(moveX,0,0) * walkspeed * Time.deltaTime * speed;
        Anim.SetFloat("Forward3", moveX);
                                }
        float AttackNumber = actions.DiscreteActions[0];
        if(AttackNumber == 1)
        {
            Anim.SetTrigger("Punch");
           // AddReward(0.001f);
            
        }
        if(AttackNumber == 2)
        {
            Anim.SetTrigger("Block");
            
        }
        if(P2ScriptedAI.lose == true)
        {
            AddReward(0.1f);
          //  Destroy(Opponent);
            EndEpisode();
        }
        if(losep1 == true)
        {
            AddReward(-0.1f);
          //  Destroy(Opponent);
            EndEpisode();
        }
       // Debug.Log(moveX);
        Debug.Log(AttackNumber);
        Debug.Log("P1 Lost" + losep1);
        Debug.Log("P2 Lost" + P2ScriptedAI.lose);
         Debug.Log(GetCumulativeReward());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("P2HitBox"))
        {   
            Anim.SetTrigger("Knockout");
            losep1 = true;
            
        }

        if (other.CompareTag("Wall"))
        {
            AddReward(-0.01f);
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
