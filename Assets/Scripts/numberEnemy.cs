using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class numberEnemy : MonoBehaviour
{
    //this enemy and the player
    public GameObject nEnemy;
    public GameObject player;

    //The player controller script so the enemy can compare values
    PlayerController playerController;

    //The number the enemy spawns at and the bounds for the random range
    public int enemyNumber;
    public int spawnLow;
    public int spawnHigh;

    public TextMeshPro numberText;

    //triggers for each staate
    [SerializeField]
    private bool chaseState;
    [SerializeField]
    private bool teleportState;
    [SerializeField]
    private bool runState;

    //If the player is within this many units of the muncher when teleport state is called it will teleport
    public BoxCollider teleportCheck;

    //Minimum and Maximum for the distance the muncher can teleport
    public int minTeleport;
    public int maxTeleport;

    [SerializeField]
    private bool hasTeleported;

    //Pick a random number for the muncher when its spawned
    void Start()
    {
        PickNumber();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        


    }

    void Update()
    {

        CompareNumber();

        if (chaseState == true)
        {
            ChasePlayer();
        }

        if (runState == true)
        {
            RunAway();
        }

        if (hasTeleported == true)
        {
            teleportState = false;
        }
    }

    //Pick a random number for the muncher when its spawned
    void PickNumber()
    {
        enemyNumber = Random.Range(spawnLow, spawnHigh + 1);

        numberText.text = enemyNumber.ToString();
    }

    //Compare the number of the enemy to the players
    void CompareNumber()
    {
        //if the player number is less than the enemy number, chase the player. If player number is greater than or equal to the enemy, run from the player
        if (playerController.playerNumber < enemyNumber)
        {
            chaseState = true;
            runState = false;
        }
        else if (playerController.playerNumber >= enemyNumber)
        {
            runState = true;
            chaseState = false;
        }

    }

    //Check to see if the player is colliding with the enemy when teleportstate is true
    private void OnTriggerStay(Collider other)
    {
        if (teleportState == true)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("THE PLAYER RUN!");
                transform.Translate(new Vector3(Random.Range(minTeleport, maxTeleport), Random.Range(minTeleport, maxTeleport), 0));
                teleportState = false;
            }
        }
    }

    //Check to see if the enemy has caught the player in chase state
    private void OnTriggerEnter(Collider other)
    {
        if (chaseState == true)
        {
            if (other.gameObject.tag == "Player")
            {
                KillPlayer();
            }
        }
    }

    //Track and chase the player
    void ChasePlayer()
    {
        hasTeleported = false;
        //Chase the player
    }

    //Run away from the player
    void RunAway()
    {
        if (hasTeleported == false)
        {
            Teleport();
        }


        //Run from the player
    }

    //Check if the enemy can teleport
    void Teleport()
    {
        Debug.Log("Checking");
        teleportState = true;
        StartCoroutine(TeleportTimer());

    }

    IEnumerator TeleportTimer()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Check over");
        teleportState = false;
        hasTeleported = true;
    }

    void KillPlayer()
    {
        Debug.Log("Kill the Player!");
    }
}
