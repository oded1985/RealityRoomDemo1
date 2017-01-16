using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;




	void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();

	}
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		move(h, v);
		turning();
		animating(h, v);
	}

	void move(float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);


	}

	void turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void animating(float h, float v)
	{

		bool walking = h != 0f || v != 0f;
		anim.SetBool("IsWalking", walking);
	}


    
}
