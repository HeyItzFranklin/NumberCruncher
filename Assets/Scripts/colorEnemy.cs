using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class colorEnemy : colorEnums
{
    //Red = 0, Yellow = 1, Blue = 2, Green = 3, comparing ints are easier than strings and enum is a good way to keep track of the colors
    public colors enemyColor;

    //this enemy and the player
    public GameObject cEnemy;
    public GameObject player;

    //The player controller script so the enemy can compare values
    PlayerController playerController;

    //triggers for each state
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

    //Materials for the muncher color and shape
    public Material Red;
    public Material Yellow;
    public Material Blue;
    public Material Green;

    public Mesh Sphere;
    public Mesh Capsule;
    public Mesh Cube;

    //Pick a random color for the muncher when its spawned and find the player object
    void Start()
    {
        PickColor();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        ChangeColor();
        CompareColor();


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

    //Pick a random color for the muncher when its spawned
    void PickColor()
    {
        enemyColor = (colors)Random.Range(0, 4);
    }

    //Compare the color of the player to the color of the enemy
    void CompareColor()
    {
        //If the enemy is a different color than the player, chase the player and if the enemy is the same color as the player, run from the player.
        if ((int)enemyColor != (int)playerController.playerColor)
        {
            chaseState = true;
            runState = false;
        }
        else if ((int)enemyColor == (int)playerController.playerColor)
        {
            runState = true;
            chaseState = false;
        }

    }

    //Sets the correct color and shape for the enemy
    void ChangeColor()
    {
        if (enemyColor == colors.Red)
        {
            cEnemy.name = "Red Enemy";
            cEnemy.GetComponent<MeshRenderer>().material = Red;
            cEnemy.GetComponent<MeshFilter>().mesh = Sphere;
            cEnemy.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (enemyColor == colors.Yellow)
        {
            cEnemy.name = "Yellow Enemy";
            cEnemy.GetComponent<MeshRenderer>().material = Yellow;
            cEnemy.GetComponent<MeshFilter>().mesh = Capsule;
            cEnemy.transform.rotation = Quaternion.Euler(90, 0, 0);

        }
        else if (enemyColor == colors.Blue)
        {
            cEnemy.name = "Blue Enemy";
            cEnemy.GetComponent<MeshRenderer>().material = Blue;
            cEnemy.GetComponent<MeshFilter>().mesh = Cube;
            cEnemy.transform.rotation = Quaternion.Euler(0, 45, 0);

        }
        else if (enemyColor == colors.Green)
        {
            cEnemy.name = "Green Enemy";
            cEnemy.GetComponent<MeshRenderer>().material = Green;
            cEnemy.GetComponent<MeshFilter>().mesh = Cube;
            cEnemy.transform.rotation = Quaternion.Euler(0, 0, 0);

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
                transform.Translate(new Vector3(Random.Range(minTeleport, maxTeleport), 0, Random.Range(minTeleport, maxTeleport)));
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
