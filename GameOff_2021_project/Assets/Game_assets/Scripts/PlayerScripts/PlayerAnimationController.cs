using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static PlayerAnimationController Instance { get; private set; }

    private Animator anim;


    private const string PLAYER_IDLE = "Idle State";
    private const string PLAYER_WALK = "Walk State";
    private const string PLAYER_CROUCH_IDLE = "Crouch Idle State";
    private const string PLAYER_CROUCH_WALK = "Crouch Walk State";
    private const string PLAYER_RUN = "Run State";
    private const string PLAYER_JUMP = "Jump State";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }


    private void Start()
    {
        anim = transform.GetComponent<Animator>();
    }



    public void CrouchIdleAnimation() =>  anim.Play(PLAYER_CROUCH_IDLE);
    public void CrouchWalkAnimation() =>  anim.Play(PLAYER_CROUCH_WALK);
    public void WalkAnimation() => anim.Play(PLAYER_WALK);
    public void IdleAnimation() => anim.Play(PLAYER_IDLE);
    public void RunAnimation() => anim.Play(PLAYER_RUN);
    public void JumpAnimation() => anim.Play(PLAYER_JUMP);



}
