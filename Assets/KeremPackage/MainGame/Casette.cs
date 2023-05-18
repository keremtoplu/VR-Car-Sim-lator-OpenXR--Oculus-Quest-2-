using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Casette : MonoBehaviour
{
	[SerializeField]
	private Socket socket;

	[SerializeField]
	private Vector3 desiredPosition;

	[SerializeField]
	private UIManager uýManager;

	private Transform presser;
	private Vector3 casetteStartPosition;
	private Vector3 presserStartTransform;
	private AudioSource music;

	private void Start()
	{
		music = transform.GetComponent<AudioSource>();

	}
	IEnumerator PlayMusic()
	{
		yield return new WaitForSeconds(2f);
		music.Play();

	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("collision");
		var hand = other.GetComponent<XRDirectInteractor>();
		if (hand && socket.IsActive)
		{
			Debug.Log("hareket");
			presser=other.transform;
			presserStartTransform = presser.transform.localPosition;
			

		}
	}

	private void OnTriggerStay(Collider other)
	{
		if(socket.IsActive)
		{
			var diff = presser.localPosition.z - presserStartTransform.z;
			var clamped=Mathf.Clamp()
		}
	}
}
