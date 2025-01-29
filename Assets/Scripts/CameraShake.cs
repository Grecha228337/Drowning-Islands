using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake current;

    private Animator anim;
    private void Awake()
    {
        current = this;
        anim = GetComponent<Animator>();
    }

    public void Shake() {
        anim.SetTrigger("Shake");
    }

}
