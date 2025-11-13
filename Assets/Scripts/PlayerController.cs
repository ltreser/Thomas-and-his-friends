using UnityEngine;


public class PlayerController : MonoBehaviour
{
	public Rigidbody rb;
	public LayerMask groundLayers; 
	public Transform cam;
  
	void Awake() 
	{
		rb = GetComponent<Rigidbody>(); // grab reference early
	}

	bool IsGrounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, 0.31f, groundLayers);
	}

	void Update()
	{
		float x;
		float z;
		Vector3 forward = cam.forward;
    	Vector3 right = cam.right;

		// Flatten forward/right vectors so ball doesn't move up/down
		forward.y = 0f;
		right.y = 0f;
		forward.Normalize();
    	right.Normalize();

		x = Input.GetAxis("Horizontal");
    	z = Input.GetAxis("Vertical");
		Vector3 moveDir = forward * z + right * x;
        moveDir = Vector3.ClampMagnitude(moveDir, 1f);
		if (moveDir.sqrMagnitude > 0)
			rb.AddForce(moveDir * 1f, ForceMode.Force);
		if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
			rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
			
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Finish"))
		{
        	Debug.Log("Game Over!");
        	rb.linearVelocity = Vector3.zero;
        	rb.isKinematic = true;
        	UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    	}
	}
}
