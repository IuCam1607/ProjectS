using UnityEngine;
using System.Collections;

public class OrbitRotation : MonoBehaviour {

	//*** This are SerializeField
	//more info: http://docs.unity3d.com/ScriptReference/SerializeField.html

	//Var: rotateSpeed: The value of how smooth of the camera rotating.
	public float rotateSpeed = 8.0f;
	//Var: lookAtTarget: 
	public Transform lookAtTarget;

	//*** This is not SerializeFields

	//Var: zoomValue: zoom in/out variable.
	private float zoomValue;
	//Var: defaultEulerAngles: use for saving start rotation.
	private Vector3 defaultEulerAngles;
	//Var: targetEulerAngles: for the marking rotation target.
	private Vector3 targetEulerAngles;
	//Var: lastMousePosition: Capture mouse position.
	private Vector3 lastMousePosition;
	//Var: distanceToParent: This variable will control how far to the parent.
	private float distanceFromParent;

	void Start () {
		/* When game is started, defaultEulerAngles variable will
		 * save this transform's parent (Empty GameObject) euler angles.*/
		defaultEulerAngles = transform.parent.eulerAngles;

		/* Currently, target angles equle default angles.*/
		targetEulerAngles = defaultEulerAngles;

		// Set start distance from parent.
		distanceFromParent = Vector3.Distance (transform.parent.position, transform.position);
	}

	void Update () {

		//*** Left mouse GetMouseButton(0).
		//*** Right mouse GetMouseButton(1).
		if (Input.GetMouseButtonDown (0)) {
			/* When mouse or touch down on the screen*/
			lastMousePosition = Input.mousePosition;
		} else if (Input.GetMouseButton (0)) {
			/* When mouse or touch moving (Still pressing down)*/

			//Var: deltaMouseSpeed: Use for controlling how big of delta position.
			float deltaMouseSpeed = 0.25f;
			//Var: deltaMousePosition: Delta position of current and last frame posion.
			Vector3 deltaMousePosition = (Input.mousePosition - lastMousePosition) * deltaMouseSpeed;

			// We will past delta position value to Rotate function.
			Rotate(deltaMousePosition);

			lastMousePosition = Input.mousePosition;
		} else if (Input.GetMouseButtonUp (0)) {
			/* When mouse or touch ended/canceeled*/

		}

		//*** Zoom-In with "W" key.
		//*** Zoom-Out with "S" key.
		if (Input.GetKey (KeyCode.W)) {
			zoomValue += 0.1f;
		} else if(Input.GetKey(KeyCode.S)){
			zoomValue -= 0.1f;
		}

		/* Final we will apply target euler angles to this Camera parent.
		 * Using Lerp function to smoothly rotation.*/

		// Convert euler angles to Rotation (Quaternion).
		Quaternion targetRotation = Quaternion.Euler (targetEulerAngles);
		// Apply to this Camera parent's rotation.
		transform.parent.rotation = Quaternion.Lerp (transform.parent.rotation, targetRotation, Time.deltaTime * rotateSpeed);
		// Keep distance to Camera's parent and apply zooming.
		transform.localPosition = (Vector3.back * distanceFromParent) - (Vector3.back * zoomValue);

		// Always keep looking to parent.
		transform.LookAt (transform.parent);

		if (lookAtTarget != null) {
			// Move Camera's parent to look at target transform's position.
			transform.parent.position = Vector3.Lerp(transform.parent.position, lookAtTarget.position, Time.deltaTime * 6.0f);
		}
	}

	public void Rotate(Vector2 deltaAngles) {

		// Left-Right rotation (Y axis) add by X axis of delta mouse position.
		targetEulerAngles.y += deltaAngles.x;

		// Up-Down rotation (X axis) minus by Y axis of delta mouse position.
		// Minus because the screen origin (0, 0) is Left-Bottom
		targetEulerAngles.x -= deltaAngles.y;
	}
}