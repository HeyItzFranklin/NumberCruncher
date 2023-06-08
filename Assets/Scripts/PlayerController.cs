using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class PlayerController : colorEnums
{

    public float playerSpeed = 5f;

    public GameObject playerObject;

    public Rigidbody rigidB;

    private Vector3 PlayerMovementInput;

    public int playerNumber = 30;

    public colors playerColor;

    public GameObject cubeGreen;

    public GameObject sphereRed;

    public GameObject capsuleYellow;

    public GameObject diamondBlue;


    public TextMeshPro playerNumberTMP;

    private numberEnemy NumberEnemy;

    private colorEnemy ColorEnemy;



    // Start is called before the first frame update
    void Start()
    {

        playerObject = GameObject.Find("Player");

        DisableColorP();
        PlayerAssignNumber();
        StartCoroutine(PlayerNumberLowerer());

        PlayerAssignColor();
        ChangePlayerColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNumber > 100)
        {
            playerNumber = 100;
        }
        if (playerNumber < 0)
        {
            playerNumber = 0;
        }
    }

    void FixedUpdate()
    {

        //records input for main movement keys. Horizontal looks for "A" and "D". Vertical looks for "W" and "S".
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();

        playerNumberTMP.text = playerNumber.ToString();

        /*
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rigidB.velocity = transform.forward * playerSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rigidB.velocity = -transform.right * playerSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rigidB.velocity = transform.right * playerSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rigidB.velocity = -transform.forward * playerSpeed;
        }
        */
    }

    private void DisableColorP()
    {
        cubeGreen.SetActive(false);
        sphereRed.SetActive(false);
        capsuleYellow.SetActive(false);
        diamondBlue.SetActive(false);


    }

    private void MovePlayer()
    {
        Vector3 VectorMove = transform.TransformDirection(PlayerMovementInput) * playerSpeed;

        rigidB.velocity = new Vector3(VectorMove.x, rigidB.velocity.y, VectorMove.z);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "ColorEnemy")
        {
            Debug.Log("we out here bois");
            GameObject theEnemy = collision.gameObject;

            if (theEnemy.GetComponent<colorEnemy>().enemyColor == playerColor)
            {
                Destroy(collision.gameObject);
                Debug.Log("Color Destroyed");
            }
        }

        if (collision.gameObject.tag == "NumberEnemy")
        {
            Debug.Log("we out here bois");
            GameObject theEnemy = collision.gameObject;

            if (theEnemy.GetComponent<numberEnemy>().enemyNumber < playerNumber)
            {
                Destroy(collision.gameObject);
                Debug.Log("Number Destroyed");
            }
        }

    }

    public void PlayerAssignNumber()
    {
        playerNumber = Random.Range(30, 61);
        Debug.Log(playerNumber);
    }

    public IEnumerator PlayerNumberLowerer()
    {
        while (playerNumber > 0)
        {
            playerNumber--;
            Debug.Log(playerNumber);
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }

    public void PlayerAssignColor()
    {
        playerColor = (colors)Random.Range(0, 4);
    }

    public void ChangePlayerColor()
    {
        if (playerColor == colors.Red)
        {
            //cEnemy.GetComponent<MeshRenderer>().material = Red;
            //colorText.text = "Red";
            DisableColorP();
            sphereRed.SetActive(true);
        }
        else if (playerColor == colors.Yellow)
        {
            //cEnemy.GetComponent<MeshRenderer>().material = Yellow;
            //colorText.text = "Yellow";
            DisableColorP();
            capsuleYellow.SetActive(true);
        }
        else if (playerColor == colors.Blue)
        {
            //cEnemy.GetComponent<MeshRenderer>().material = Blue;
            //colorText.text = "Blue";
            DisableColorP();
            diamondBlue.SetActive(true);
        }
        else if (playerColor == colors.Green)
        {
            //cEnemy.GetComponent<MeshRenderer>().material = Green;
            //colorText.text = "Green";
            DisableColorP();
            cubeGreen.SetActive(true);
        }
    }
}




//tutorial credit: https://www.youtube.com/watch?v=b1uoLBp2I1w 
//How to Create Player Movement in UNITY (Rigidbody & Character Controller)
