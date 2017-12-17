using UnityEngine;

public class LookAtCamera: MonoBehaviour  {
	Transform mainCamera;   

	void Start() {
		//Set the Main Camera as the target
		mainCamera = Camera.main.transform;
	}

	void LateUpdate() {
		if (mainCamera == null) {
			return;
		}

		//Apply the rotation needed to look at the camera. Note, since pointing a UI text element
		//at the camera makes it appear backwards, we are actually pointing this object
		//directly *away* from the camera.
		transform.rotation = Quaternion.LookRotation (transform.position - mainCamera.position);
	}
}