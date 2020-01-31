namespace Pattern.Example {
	using UnityEngine;

	/// <summary>
	/// A fake API service to demonstrate the Singleton Pattern.
	/// </summary>
	public sealed class APIService : Singleton<APIService> {
		/// <summary>
		/// Gets the JSON resource from an endpoint and route non-blocking (async)
		/// </summary>
		/// <param name="route"></param>
		/// <param name="callback"></param>
		public void GetResource(string route, System.Action<string> callback) {
			callback.Invoke("{ \"result\": \"some JSON data\" }");
		}

		/// <summary>
		/// Gets the JSON resource from an endpoint and route blocking (sync)
		/// </summary>
		/// <param name="route"></param>
		/// <returns></returns>
		public string GetResource(string route) {
			return "{ \"result\": \"some JSON data\" }";
		}
	}
}