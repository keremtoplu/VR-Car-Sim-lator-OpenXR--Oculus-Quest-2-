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

	private Transform attach;
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
			attach=socket.GetComponent<XRSocketInteractor>().attachTransform;
			attach.transform.localPosition = desiredPosition;
			uýManager.MusicText.text = music.clip.name;
			StartCoroutine(PlayMusic());
			
			

		}
	}

}
