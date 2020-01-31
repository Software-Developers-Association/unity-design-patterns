using UnityEngine;

/// <summary>
///	Pattern - Singleton aka Anti-Pattern
/// 
///	Purpose - Construct a way to restrict the creation of certain classes so only 1 instance of a particular
///	class is intantiated during the lifetime of the application. It must also be easily accessable and should not
///	cluttery the global namespace.
///
/// Type - Creational
/// 
/// <see cref="https://en.wikipedia.org/wiki/Singleton_pattern"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
	private static T instance = null; // cache the actual instance.
	private static object mutex = new object(); // a mutex object for a thread-safety approach.

	#region - Property Version
	/// <summary>
	/// Gets the global instance for this class. (thread safe)
	/// </summary>
	/// <returns>Global Instance</returns>
	public static T Instance {
		get {
			lock(mutex) {
				if(instance == null) {
					// In Unity we can scan the scene for a particular type
					// let's try to locate this component in the event the component
					// already exists in the scene
					// https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
					instance = Object.FindObjectOfType<T>();

					// The instance was not found in the scene...
					if(instance == null) {
						// Let's create an empty object, and attach the component to it.
						GameObject go = new GameObject(typeof(T).Name);

						// because of the script execution order in Unity,
						// we must first disable the GameObject to ensure Awake is not called before
						//go.SetActive(false);

						instance = go.AddComponent<T>();
					}

					// Ensure that when Unity loads a different scene,
					// that this GameObject persist into the next scene.
					// https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html
					Object.DontDestroyOnLoad(instance.gameObject);
				}

				return instance;
			}
		}
	}
	#endregion

	#region - Method Version
	/// <summary>
	/// Gets the global instance for this class. (thread safe)
	/// </summary>
	/// <returns>Global Instance</returns>
	public static T GetIntance() {
		// lock this object for thread safety
		// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement
		lock(mutex) {
			if(instance == null) {
				// In Unity we can scan the scene for a particular type
				// let's try to locate this component in the event the component
				// already exists in the scene
				// https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
				instance = Object.FindObjectOfType<T>();

				// The instance was not found in the scene...
				if(instance == null) {
					// Let's create an empty object, and attach the component to it.
					GameObject go = new GameObject(typeof(T).Name);

					// because of the script execution order in Unity,
					// we must first disable the GameObject to ensure Awake is not called before
					//go.SetActive(false);

					instance = go.AddComponent<T>();
				}

				// Ensure that when Unity loads a different scene,
				// that this GameObject persist into the next scene.
				// https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html
				Object.DontDestroyOnLoad(instance.gameObject);
			}

			return instance;
		}
	}
	#endregion

	/// <summary>
	/// Initialization - This method is invoked when this component first gets loaded.
	///
	/// <see cref="https://docs.unity3d.com/Manual/ExecutionOrder.html"/>
	/// </summary>
	protected virtual void Awake() {
		if(Instance != this) {
			DestroyImmediate(this.gameObject);
			return;
		}
	}
}