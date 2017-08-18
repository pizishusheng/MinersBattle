using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    enum AnimState{
        RUN,
        WALK_FRONT,
        WALK_BACK,
        WALK_RIGHT,
        WALK_LEFT,
        Idle,
        Idle2,
        Attack1,
        Attack2,
    }
    AnimState m_currentState = AnimState.Idle;
    Animation m_animtion;

	void Start () {
        m_animtion = GetComponent<Animation>();
	}
	
	
	void Update () {
        
	}

    void SwitchAnimationState (AnimState state) {
        
        switch (state) {
            case AnimState.RUN:
                m_animtion.Play("Run");
                break;
            case AnimState.WALK_FRONT:
                m_animtion.Play("WalkFront");
                break;
            case AnimState.WALK_BACK:
                m_animtion.Play("WalkBack");
                break;
            case AnimState.WALK_RIGHT:
                m_animtion.Play("WalkRight");
                break;
            case AnimState.WALK_LEFT:
                m_animtion.Play("WalkLeft");
                break;
        }
    }
}
