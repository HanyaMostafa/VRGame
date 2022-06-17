using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{ 
    bool alive = true;

    public float speed = 0.1f;
    public Rigidbody rb;

    float horizontalInput;
    public float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;

    public float jumpForce = 400f;
    LayerMask groundMask;

    void FixedUpdate ()
    {
        if (!alive) return;

	Vector3 forwardMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
	rb.MovePosition(transform.position + forwardMove * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.RightControl) | Input.GetKey(KeyCode.LeftControl))
        {
	rb.MovePosition(transform.position + forwardMove * Time.deltaTime * 20);
        }
    }
    
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

	if(Input.GetKeyDown(KeyCode.Space))
	{ 
	    Jump();
	}

        if (transform.position.y < -5) {
            Die();
        }
    }

    public void Die ()
    {
        alive = false;
        // Restart the game
        Invoke("Restart", 2);
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump ()
    {
	float height = GetComponent<Collider>().bounds.size.y;
	bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

	rb.AddForce(Vector3.up * jumpForce);
    }

}

