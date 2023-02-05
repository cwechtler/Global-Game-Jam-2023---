using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class BossZone : MonoBehaviour
{
	[SerializeField] private BoxCollider2D boxColider;
	[SerializeField] private UnityEvent enterBossZoneEvent;
	[SerializeField] private GameObject gnarlwoodHealthBar;
	[SerializeField] private GameObject growth;
	[SerializeField] private CameraShake cameraShake;
	[SerializeField] private float shakeDuration = 1f;
	[SerializeField] private float shakeMagnitude = .4f;

	private bool noShake = false;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			GameController.instance.isInBossZone = true;
			boxColider.enabled = true;
			gnarlwoodHealthBar.SetActive(true);	
			growth.SetActive(true);
			if (!noShake) {
				noShake = true;	
				StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
			}
			
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			GameController.instance.isInBossZone = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			GameController.instance.isInBossZone = false;
		}
	}
}
