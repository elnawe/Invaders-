using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject bulletObject;
	public GameObject Player;
	public float fireCooldownSeconds = 0.2f;
	public float horizontalSpeed = 15;
	public float turnAngle = 10;
	public float verticalSpeed = 10;

	private float cooldownCountdownTimer;

	void Start () {
		Player = GameObject.FindObjectOfType<Player>().gameObject;
	}
	
	void Update () {
		if (this.CanMoveHorizontally()) {
			transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime);
		}

		if (this.CanMoveVertically()) {
			transform.Translate(Vector2.up * Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime);
		}

		if (this.IsFiring()) {
			cooldownCountdownTimer = Time.time + fireCooldownSeconds;
			GameObject.Instantiate(bulletObject);
		}

		Player.transform.rotation = Quaternion.Euler(0, 0, Input.GetAxis("Horizontal") * -turnAngle);
	}

	private bool CanMoveHorizontally () {
		float horizontalInput = Input.GetAxis("Horizontal");

		return !(
			transform.position.x <= -8 && !(horizontalInput > 0) ||
			transform.position.x >= 8 && !(horizontalInput < 0)
		);
	}

	private bool CanMoveVertically () {
		float verticalInput = Input.GetAxis("Vertical");

		return !(
			transform.position.y <= -4.4 && !(verticalInput > 0) ||
			transform.position.y >= 4.4 && !(verticalInput < 0)
		);
	}

	private bool IsFiring () {
		return (Input.GetButton("Fire1") && bulletObject && (cooldownCountdownTimer <= Time.time));
	}
}
