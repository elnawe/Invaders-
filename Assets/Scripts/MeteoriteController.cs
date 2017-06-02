using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteoriteController : MonoBehaviour {
	public Canvas statsCanvas;
	public float healthPowerMultiplier = 1;
	public float healthPowerInitial = 50;
	public float maximumScaleSize = 3f;
	public float minimumScaleSize = 0.8f;
	public float minimumMaximumRotationSpeed = 3f;
	public GameObject hitParticle;
	public Image healthBarImage;

	private bool collisionFeedback = false;
	private float collisionFeedbackDelay = 0.2f;
	private float collisionFeedbackTime;
	private float healthPower;
	private float rotationSpeed;
	private float scaleSize = 1;
	private Quaternion defaultCanvasRotation;
	private Renderer rendererComponent;

	void Start () {
		rendererComponent = GameObject.FindObjectOfType<Meteorite>().GetComponent<SpriteRenderer>();
		scaleSize = Random.Range(minimumScaleSize, maximumScaleSize);
		healthPower = this.GetInitialHealthPower();
		transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
		transform.position = new Vector3(Random.Range(-7, 7), 6, 0);
		defaultCanvasRotation = statsCanvas.transform.rotation;
		rotationSpeed = Random.Range(-minimumMaximumRotationSpeed, minimumMaximumRotationSpeed);
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, rotationSpeed);
		statsCanvas.transform.rotation = defaultCanvasRotation;

		if (this.GetInitialHealthPower() != healthPower) {
			statsCanvas.enabled = true;
		}

		if (this.IsOutsideScreen()) {
			Destroy(gameObject);
		}

		this.FlashColorOnCollision();
	}

	void OnCollisionEnter2D (Collision2D collision) {
		float damageDealt;
		GameObject colliderGameObject = collision.gameObject;

		if (this.isBulletCollider(colliderGameObject)) {
			damageDealt = colliderGameObject.GetComponent<BulletController>().calculateDamage();
			collisionFeedback = true;
			collisionFeedbackTime = Time.time + collisionFeedbackDelay;
			healthPower -= damageDealt;
			healthBarImage.fillAmount = healthPower / this.GetInitialHealthPower();

			Instantiate(hitParticle, collision.gameObject.transform.position, hitParticle.transform.rotation);

			if (healthPower <= 0) {
				Destroy(gameObject);
			}

			Destroy(collision.gameObject);
		}
	}

	private void FlashColorOnCollision () {
		if (collisionFeedback) {
			rendererComponent.material.color = Color.red;

			if (Time.time >= collisionFeedbackTime) {
				rendererComponent.material.color = Color.white;
				collisionFeedback = false;
			}
		}
	}

	private bool IsOutsideScreen () {
		return (transform.position.y <= -7);
	}

	private float GetInitialHealthPower () {
		return (healthPowerInitial * healthPowerMultiplier) * scaleSize;
	}

	private bool isBulletCollider (GameObject colliderObject) {
		return (colliderObject.GetComponent<BulletController>());
	}
}
