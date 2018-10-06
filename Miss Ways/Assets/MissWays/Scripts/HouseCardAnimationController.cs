using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCardAnimationController : MonoBehaviour
{
    private Animation animation;
    private string animationClip;

    void Awake()
    {
        animation = GetComponent<Animation>();
        animationClip = animation.clip.name;
        animation.Play();
    }

    public void PauseFence()
    {
        animation.Stop();  
    }

    public void PauseHouse()
    {
        animation.Stop();
    }

    public void PlayFence()
    {
        StartCoroutine(CRPlayFence());
    }

    public void PlayHouse()
    {
        StartCoroutine(CRPlayHouse());
    }

    IEnumerator CRPlayFence()
    {
        yield return new WaitForEndOfFrame();
        animation[animationClip].time = 0.0f;
        animation.Play();
    }

    IEnumerator CRPlayHouse()
    {
        yield return new WaitForEndOfFrame();
        animation[animationClip].time = 3.8f;
        animation.Play();
    }
}
