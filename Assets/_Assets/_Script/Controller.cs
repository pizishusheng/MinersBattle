using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;

public class Controller : MonoBehaviour {

    public int m_speed;

    AnimState m_currentState = AnimState.IDLE;
    Animator m_animator;

	void Start () {
        m_animator = GetComponent<Animator>();
	}
	
	
	void Update () {
        
	}

    public void OnMove(float x, float z) {
        Vector3 dir = new Vector3(x, 0, z);
        dir.Normalize();
        Move(dir);
        Rotate(dir);
    }

    public void OnMoveEnd(){
        if (m_currentState != AnimState.IDLE)
		{
			m_speed = 20;
			m_animator.SetBool("IsMove", false);
            m_currentState = AnimState.IDLE;
		}
    }

    void Move(Vector3 direction) {
        m_animator.SetBool("IsMove", true);
        m_currentState = AnimState.RUN;

        Vector3 movement = direction * m_speed * Time.deltaTime;
        Vector3 nextPos = transform.position + movement;
        Vector3 targetPos = Vector3.Lerp(transform.position, nextPos, Time.deltaTime * m_speed);
        transform.position = new Vector3(targetPos.x, 0, targetPos.z);
    }

    void Rotate(Vector3 direction) {
        float angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * 10f);
    }

    public void Hit() {
        m_animator.SetTrigger("IsAttack");
	}

    public void Jump() {
        if (m_currentState == AnimState.IDLE) {
            m_animator.ResetTrigger("IsRun2Jump");
            m_animator.SetTrigger("IsJump");
        }
        if (m_currentState == AnimState.RUN){
            m_animator.ResetTrigger("IsJump");
            m_animator.SetTrigger("IsRun2Jump");
        }
    }

	void SwitchAnimationState(AnimState state)
	{
		if (m_currentState == state)
			return;

		m_currentState = state;
		switch (state)
		{
			case AnimState.RUN:
                m_animator.SetBool("IsMove", true);
				break;
			case AnimState.IDLE:
                m_animator.SetBool("IsMove", false);
				break;
			case AnimState.IDLE_2:
				break;
            case AnimState.ATTACK_1:
                m_animator.SetTrigger("IsAttack");
                break;
            case AnimState.ATTACK_2:
                m_animator.SetBool("IsAttack2", true);
                break;
            case AnimState.GATHER:
                m_animator.SetBool("IsGather", true);
                break;
            case AnimState.DEATH:
                m_animator.SetBool("IsDeath", true);
                break;
            case AnimState.JUMP:
                m_animator.SetTrigger("IsJump");
                break;
		}
	}
}
