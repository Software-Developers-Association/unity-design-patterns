using UnityEngine;

public class Bullet : MonoBehaviour {
	[SerializeField]
	private float speed = 10.0F; // the speed the bullet will move. (i.e. the number of units this object will move per second)
	[SerializeField]
	private float timeToLive = 2.0F; // the amount of time this bullet will live, in seconds.

	private float startTime = 0.0F;

	/// <summary>
	/// Called everytime this object is enabled.
	///
	/// <see cref="https://docs.unity3d.com/Manual/ExecutionOrder.html"/>
	/// </summary>
	private void OnEnable() {
		this.startTime = Time.time;
	}

	/// <summary>
	/// Called once per frame.
	///
	/// <see cref="https://docs.unity3d.com/Manual/ExecutionOrder.html"/>
	/// </summary>
	private void Update() {
		// move the bullet forward based on the speed and scale it based on the frame rate
		// so the delta distance each update is frame independent so the bullet moves
		// the same no matter what the frame rate.
		this.transform.position += this.transform.forward * this.speed * Time.deltaTime;

		// Once this object has outlived it's set time
		// we will disable it.
		if(Time.time >= (this.startTime + this.timeToLive)) {
			// disable the object.
			this.gameObject.SetActive(false);
		}
	}
}