using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 7;
	public event System.Action OnPlayerDeath;

	float screenHalfWidthInWorldUnits;
	private Camera mainCamera = null;

    private void Awake()
    {
		mainCamera = Camera.main;
    }
    void Start () {
		
		float halfPlayerWidth = transform.localScale.x / 2f;
		screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
	
	}
	
	// Update is called once per frame
	void Update () {
		// Handle movement
		if(mainCamera == null)
        {
			return;
        }

		Vector3 touchPosition;
		if(Input.touchCount > 0)
        {
			var touch = Input.GetTouch(0);
			touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
            // Debug.Log(touchPosition);
        }
        else
        {
			touchPosition = Vector3.zero;
        }

		if(touchPosition.x > 0)
        {
			touchPosition.x = 1;
        }

		if (touchPosition.x < 0)
        {
			touchPosition.x = -1;
        }

		// float inputX = Input.GetAxisRaw ("Horizontal");
		float inputX = touchPosition.x;
		Debug.Log(inputX);
		float velocity = inputX * speed;
		transform.Translate (Vector2.right * velocity * Time.deltaTime);

		if (transform.position.x < -screenHalfWidthInWorldUnits) {
			transform.position = new Vector2 (screenHalfWidthInWorldUnits, transform.position.y);
		}

		if (transform.position.x > screenHalfWidthInWorldUnits) {
			transform.position = new Vector2 (-screenHalfWidthInWorldUnits, transform.position.y);
		}

	}

	void OnTriggerEnter2D(Collider2D triggerCollider) {
		if (triggerCollider.tag == "Falling Block") {
			if (OnPlayerDeath != null) {
				OnPlayerDeath ();
			}
			Destroy (gameObject);
		}
	}
}
