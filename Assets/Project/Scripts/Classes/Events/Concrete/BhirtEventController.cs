using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhirtEventController : AbstractEventController {
	public Sprite bhirtHead;
	public GameObject bhirtObject;
	public GameObject nextEventObject;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		PlayAnimationPersistent(player.gameObject,"IdleRight");
		PlayAnimationPersistent(bhirtObject,"IdleLeft");
		yield return StartCoroutine(ShowDialogue("Hey, Mason. There are a couple of guards by the entrance, but... I think I should talk to them. One's Drake, the other's Lizard, and I'm a bit of both. It could help.","Bhirt",bhirtHead));
		yield return StartCoroutine(ShowDialogue("You'll get further than I would. Have at it!","Mason",masonHead));
		StartCoroutine(MoveCamera(new Vector3(6.5f,2.5f,-5.842f),4.5f));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		StartCoroutine(MoveObject(bhirtObject,Direction.Right));
		yield return StartCoroutine(ResetCamera(3.0f));
		yield return null;
		PlayAnimationPersistent(bhirtObject,"IdleRight");
		SetObjectActive(nextEventObject,true);
		EndEventCoroutine();
	}
}
