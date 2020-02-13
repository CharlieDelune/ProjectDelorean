using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldDayEnterTrigger : AbstractEventController
{
    public Sprite catHead;
    public Sprite eiraHead;
    public Sprite valdisHead;
    public Sprite garfHead;
    public Sprite bhirtHead;
    public Sprite kodaHead;
    public GameObject catObject;
    public GameObject eiraObject;
    public GameObject valdisObject;
    public GameObject garfObject;
    public GameObject bhirtObject;
    public GameObject kodaObject;
    public override IEnumerator EventCoroutine()
    {
        player.StopMovement();
        PlayAnimationPersistent(player.gameObject, "IdleUp");
        SetObjectPosition(player.gameObject, new Vector3(-5.0f, 1.15f, 39.0f));
        SetObjectPosition(bhirtObject, new Vector3(-5f, 2.22f, 31f));
        SetObjectPosition(garfObject, new Vector3(-4.5f, 2.22f, 31f));
        SetObjectPosition(eiraObject, new Vector3(-3.5f, 2.22f, 30f));
        SetObjectPosition(valdisObject, new Vector3(-5.5f, 2.22f, 30.5f));
        SetObjectPosition(kodaObject, new Vector3(-4.0f, 2.22f, 30.5f));
        SetObjectPosition(catObject, new Vector3(-6f, 2.22f, 30f));
        PlayAnimationPersistent(bhirtObject, "IdleDown");
        PlayAnimationPersistent(garfObject, "IdleDown");
        PlayAnimationPersistent(eiraObject, "IdleDown");
        PlayAnimationPersistent(valdisObject, "IdleDown");
        PlayAnimationPersistent(kodaObject, "IdleDown");
        PlayAnimationPersistent(catObject, "IdleDown");
        PlayAnimationPersistent(player.gameObject, "IdleUp");
        yield return new WaitForSeconds(1.0f);
        PlayAnimationPersistent(player.gameObject, "IdleUp");
        yield return StartCoroutine(ShowDialogue("Oh gods, I will <i>never</i> get used to that.", "Eira", eiraHead));
        yield return StartCoroutine(ShowDialogue("Don't worry, you definitely will.", "Garf", garfHead));
        yield return StartCoroutine(ShowDialogue("So where exactly do we start?", "Bhirt", bhirtHead));
        yield return StartCoroutine(ShowDialogue("Ummm... I don't know. I haven't done much of this yet...", "Mason", masonHead));
        yield return StartCoroutine(ShowDialogue("Start in town.", "Valdis", valdisHead));
        yield return StartCoroutine(ShowDialogue("In town?", "Mason", masonHead));
        yield return StartCoroutine(ShowDialogue("Yes. We know when the attack occurs, so we should vacate the city before it happens.", "Valdis", valdisHead));
        yield return StartCoroutine(ShowDialogue("But don't we already know that won't work? I mean, Mason saw corpses in town, he saw people getting attacked, he saw the fire.", "Eira", eiraHead));
        yield return StartCoroutine(ShowDialogue("We find there's not much point in worrying about paradoxes. Honestly, the Gods seem to have a way of making things right in the end.", "Garf", garfHead));
        yield return StartCoroutine(ShowDialogue("Oh yeah, like that turtle thing. Once we found a city on the back of a gi-", "Mason", masonHead));
        yield return StartCoroutine(ShowDialogue("Now's not really the time, Mason. We are actually on the clock here.", "Garf", garfHead));
        yield return StartCoroutine(ShowDialogue("Right. Alright, let's go head into town and see if we can get evacuation going. I think we should stick together on this one.", "Mason", masonHead));
        yield return StartCoroutine(ShowDialogue("Rograwwrr?", "Cat", catHead));
        yield return StartCoroutine(ShowDialogue("Yep, even you, Cat.", "Mason", masonHead));
        yield return StartCoroutine(ShowDialogue("Alright, everyone, let's get moving!", "Mason", masonHead));
        PlayAnimationPersistent(player.gameObject, "IdleDown");
        yield return StartCoroutine(FadeOut());
        DeleteObject(bhirtObject);
        DeleteObject(garfObject);
        DeleteObject(eiraObject);
        DeleteObject(valdisObject);
        DeleteObject(kodaObject);
        DeleteObject(catObject);
        SetPartyMemberAvailable(new UnitStats[] {party.GetUnitStats("Cat"),party.GetUnitStats("Eira"),party.GetUnitStats("Valdis"),party.GetUnitStats("Bhirt"),party.GetUnitStats("Garf"),party.GetUnitStats("Koda")},true);
		yield return StartCoroutine(FadeIn());
        yield return StartCoroutine(ShowDialogue("You now have access to all of the party members. The suggested members are Mason, Bhirt, Valdis, and Cat, because many of the other party members don't have their proper abilities yet and are completely useless at attacking."));
        EndEventCoroutine();
    }
}
