using UnityEngine;

public class Gun : MonoBehaviour {
	[SerializeField]
	private GameObject bullet; // the original bullet we will clone, think of this as our template/blueprint.
	[SerializeField]
	private Transform muzzle; // the location the bullet will start.
	private ObjectPool<GameObject> bullets;

	/// <summary>
	/// Called during initialization of the component.
	///
	/// <see cref="https://docs.unity3d.com/Manual/ExecutionOrder.html"/>
	/// </summary>
	private void Awake() {
		this.bullets = new ObjectPool<GameObject>(create: this.Create, isAvailable: this.IsAvailable, destroy: this.Delete);
	}

	private GameObject Create() {
		return Instantiate(this.bullet);
	}

	private bool IsAvailable(GameObject go) {
		return go != null && !go.activeSelf;
	}

	private void Delete(GameObject go) {
		Destroy(go);
	}

	/// <summary>
	/// Called once per frame.
	/// <see cref="https://docs.unity3d.com/Manual/ExecutionOrder.html"/>
	/// </summary>
	private void Update() {
		// Check if the Space bar has been pressed this frame. 
		if(Input.GetKeyDown(KeyCode.Space)) {
			// the reference to contain the bullet, should one be available.
			GameObject bullet;

			// Attempt to get the next available bullet.
			if(this.bullets.GetAvailable(out bullet)) {
				// reposition/reorient the bullet based on the muzzle.
				bullet.transform.position = this.muzzle.position;
				bullet.transform.rotation = this.muzzle.rotation;

				// in our example, bullets when they are ready for reuse will be disabled
				// so we need to enable them to get them going.
				bullet.SetActive(true);
			}
		}
	}

	/// <summary>
	/// Called when this object is destroyed.
	/// <see cref="https://docs.unity3d.com/Manual/ExecutionOrder.html"/>
	/// </summary>
	private void OnDestroy() {
		// we should call dispose on our pool, to clean up the objects.
		this.bullets.Dispose();
	}
}