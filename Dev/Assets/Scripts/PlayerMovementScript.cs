using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public static PlayerMovementScript Instance;
    public float startSpeed = 10.0f;
    public float maxSpeed = 15.0f;
    public float currentSpeed;
    public bool gameStarted = false, gameOver, block;
    public float swerveSpeed = 5.0f;
    public float maxHorizontalPosition = 3.0f;
    private float mouseStart;
    DoorScript doorScript;
    bool doorFactor;
    public GameObject heart;
    public float dst,dly = 0;
    Vector3 collision;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        currentSpeed = startSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = Input.mousePosition.x;
            gameStarted = true;
        }

        if (gameStarted)
        {
            if (currentSpeed > maxSpeed && !doorFactor)
            {
                currentSpeed -= Time.deltaTime * 10;
            }
            if (currentSpeed <= maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            float newXPos = Mathf.Clamp(transform.position.x, -maxHorizontalPosition, maxHorizontalPosition);
            transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButton(0) && !block)// && currentSpeed == maxSpeed)
        {
            float moveInput = Input.mousePosition.x - mouseStart;
            moveInput = Mathf.Clamp(moveInput, -maxHorizontalPosition, maxHorizontalPosition);
            Vector3 moveVector = new Vector3(moveInput * Time.deltaTime * swerveSpeed, 0, currentSpeed * Time.deltaTime);
            transform.Translate(moveVector);
        }
        else if (gameStarted && !gameOver && !block)
        {
            UIManager.Instance.tapToStart.SetActive(false);
            transform.position += transform.forward * Time.deltaTime * currentSpeed;
        }
        if (transform.childCount < 4)
        {
            gameOver = true;
            GetComponent<Rigidbody>().isKinematic = true;
            heart.GetComponent<Animator>().SetTrigger("heart");
            UIManager.Instance.CheckSucces(5);
        }
        dst = Vector3.Distance(collision, transform.position);

        if (dly > 0)
        {
            dly -= Time.deltaTime;
        }
        if (transform.position.z - dst + 2< 0.0001f && dly < 0)
        {
            block = false;
            dly = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DoorScript>())
        {
            doorScript = other.GetComponent<DoorScript>();
            doorFactor = true;

            if (doorScript.speed != 0 )
            {
                currentSpeed += doorScript.speed * Time.deltaTime;
            }
        }
      //  if (other.CompareTag("Block"))
      //  {
      //      dly = 1.8f;
      //      GetComponent<Rigidbody>().AddForce(Vector3.back * 100);
      //      collision = transform.position;
      //      block = true;
      //
      //  }
    }
}

