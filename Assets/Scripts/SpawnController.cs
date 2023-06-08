using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SpawnController : MonoBehaviour
{
    //initialization block: need an array of game objects for spawn locations

    int playerNumber;
    int enemyNumber;
    int NextSpawn;
    GameObject Player;
    public GameObject[] SpawnLocation;
    public GameObject ColorEnemy;
    public GameObject NumberEnemy;
    GameObject Location;
    Vector3 SpawnLoc;
    PlayerController playerController;
   
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        playerController = Player.GetComponent<PlayerController>();

        SpawnChooser();
        SpawnNumberEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        playerNumber = playerController.playerNumber;
        
    }
    //function: choose a random spawn location and set the spawn location to the empty object with the proper tag
    public void SpawnChooser()
    {
        NextSpawn = Random.Range(0, SpawnLocation.Length-1);

        if (NextSpawn == 0)
        {
            Location = GameObject.FindGameObjectWithTag("Spawn 0");
            SpawnLoc = Location.transform.position;
        }
        if (NextSpawn == 1)
        {
            Location = GameObject.FindGameObjectWithTag("Spawn 1");
            SpawnLoc = Location.transform.position;
        }
        if (NextSpawn == 2)
        {
            Location = GameObject.FindGameObjectWithTag("Spawn 2");
            SpawnLoc = Location.transform.position;
        }
        if (NextSpawn == 3)
        {
            Location = GameObject.FindGameObjectWithTag("Spawn 3");
            SpawnLoc = Location.transform.position;
        }
        if (NextSpawn == 4)
        {
            Location = GameObject.FindGameObjectWithTag("Spawn 4");
            SpawnLoc = Location.transform.position;
        }

    }

    //function: check if a spawn location is clear

    //function: choose whether to spawn a number or color enemy (check if none of a particular type exists and spawn that type, otherwise randomize)

    //function: spawn a number enemy with a random value near the player's number
    public void SpawnNumberEnemy()
    {
        enemyNumber = Random.Range(playerNumber - 5, playerNumber + 5);
        Instantiate(NumberEnemy, SpawnLoc, Quaternion.identity);
    }

    //function: spawn a color enemy with a random color




}
