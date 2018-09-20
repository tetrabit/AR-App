using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCardAnimationController : MonoBehaviour {

    public Animator anim;
    AnimatorClipInfo[] clipInfo;
    AnimationClip ac;
    public Animation animation;
    public AnimationState animState;

	// Use this for initialization
	void Start () {

        //animState = animation.GetComponent<AnimationState>();
        //clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        //Debug.Log(clipInfo[0].clip);
        //ac = clipInfo[0].clip;
        //anim.

        Debug.Log(animation.clip.name);
        animation.Play("Fence");
    }
	
    public void PlayFence()
    {
        anim.SetTrigger("PlayFence");

    }

    public void PlayHouse()
    {
        anim.SetTrigger("PlayHouse");
    }
}
