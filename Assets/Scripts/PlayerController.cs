using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
// Include the namespace required to use Unity UI and Input System


public class PlayerController : MonoBehaviour
{
    // Create public variables for player speed, and for the Text UI game objects
    public float speed = 0;
    public TextMeshProUGUI countText;

    private Rigidbody rb;
    private int count;
    //public float jumpHeight;
    private float movementX;
    private float movementY;
    public GameObject winTextObject;
    //public float jumpHeight;
    public Transform respawnPoint;

    // Start is called before the first frame update
    // At the start of the game..
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();
        // Set the count to zero 
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    private void update()
    {
        if(transform.position.y < -1)
        {
            Respawn();
        }
    }

    void OnMove(InputValue movementValue) 
    {
        // Function body
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        //movementZ = movementVector.z;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight);
        }*/

        /*if (Input.GetKeyDown("space"))
        {
            Vector3 jump = new Vector3 (0.0f, jumpHeight, 0.0f);
            rb.AddForce (jump);
        }*/
    }


    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            OnCol
        }
    }*/

   /* private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Enemy"))
        {
            Sc
        }

    } */   


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    void Respawn()
    {

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        transform.position = respawnPoint.position;

    }

}
