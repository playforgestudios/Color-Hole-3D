using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using System.Collections;

public class UndergroundCollision : MonoBehaviour
{
	public Animator animator;

    void OnTriggerEnter(Collider other)
	{
		//Object or Obstacle is at the bottom of the Hole

		if (!Game.isGameover)
		{
			string tag = other.tag;
			//------------------------ O B J E C T --------------------------
			if (tag.Equals("Object"))
			{
				animator.SetTrigger("isFun");
				Level.Instance.objectsInScene--;
				UIController.uIController.UpdateLevelProgress();
				//Magnet.instance.animator.SetTrigger("isFun");
				//Make sure to remove this object from Magnetic field
				Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
				other.gameObject.SetActive(false);
				Destroy(other.gameObject);
				Vibrator.Vibrate(UIController.vibrationVol);
				//check if win
				if (Level.Instance.objectsInScene == 0)
				{
					Game.isGameover = true;
					AdsManager.showAdd = true;
					Level.Instance.PlayWinFx();
					Invoke("NextLevel", 2);
					
				}
			}
			//---------------------- O B S T A C L E -----------------------
			if (tag.Equals("Obstacle"))
			{

				Magnet.instance.animator.SetBool("isSad", true);
				Game.isGameover = true;
				other.gameObject.SetActive(false);
				Destroy(other.gameObject);
				Vibrator.Vibrate(UIController.vibrationVol);
				//Shake the camera for 1 second
				Camera.main.transform
					.DOShakePosition(1f, .2f, 20, 90f)
					.OnComplete(() =>
					{
						//restart level after shaking complet
						//Level.Instance.RestartLevel ();
						//UIController.uIController.gameOverPanel.SetActive(true);
						UIController.uIController.rewardedInterstitialPanel.SetActive(true);
						//UIController.uIController.check = true;
					});
			}
		}
	}

	void NextLevel()
	{
		Level.Instance.LoadNextLevel();
	}


	private void OnTriggerExit(Collider other)
	{
		if (!Game.isGameover && tag.Equals("Object"))
		{
			print("YESSS");
			Magnet.instance.animator.SetBool("isTrigger", false);
		}
	}

}
