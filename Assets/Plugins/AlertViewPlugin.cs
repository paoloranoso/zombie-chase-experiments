using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AlertViewPlugin : MonoBehaviour {

#if UNITY_IPHONE && !UNITY_EDITOR

	[DllImport("__Internal")]
	private static extern void _ShowMessage(string title, string message);

	[DllImport("__Internal")]
	private static extern void _ShowConfirmMessage(string title, string message, string confirmText);


	public static void ShowMessage(string title, string message) {
		_ShowMessage(title, message);
	}

	public static void ShowConfirmMessage(string title, string message, string confirmText) {
		_ShowConfirmMessage(title, message, confirmText);
	}

#else

	public static void ShowMessage(string title, string message) {}
	public static void ShowConfirmMessage(string title, string message, string confirmText) {}

#endif

}
