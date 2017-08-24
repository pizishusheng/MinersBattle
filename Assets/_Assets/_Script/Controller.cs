using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;

public class Controller : MonoBehaviour {

    public int m_speed;

    AnimState m_currentState = AnimState.IDLE;
    Animation m_animtion;

	void Start () {
        m_animtion = GetComponent<Animation>();
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
			SwitchAnimationState(AnimState.IDLE);
		}
    }

    void Move(Vector3 direction) {
        SwitchAnimationState(AnimState.RUN);

        Vector3 movement = direction * m_speed * Time.deltaTime;
        Vector3 nextPos = transform.position + movement;
        Vector3 targetPos = Vector3.Lerp(transform.position, nextPos, Time.deltaTime * m_speed);
        transform.position = new Vector3(targetPos.x, 0, targetPos.z);
    }

    void Rotate(Vector3 direction) {
        float angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * 10f);
    }

	void SwitchAnimationState(AnimState state)
	{
		if (m_currentState == state)
			return;

		m_animtion.Stop();
		m_currentState = state;
		switch (state)
		{
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
			case AnimState.IDLE:
				m_animtion.Play("Idle");
				break;
			case AnimState.IDLE_2:
				m_animtion.Play("Idle2");
				break;
            case AnimState.ATTACK_1:
                m_animtion.Play("Mine1/Attack1");
                break;
            case AnimState.ATTACK_2:
                m_animtion.Play("Mine2/Attack2");
                break;
            case AnimState.GATHER:
                m_animtion.Play("Gather");
                break;
            case AnimState.DEATH:
                m_animtion.Play("Death");
                break;
            case AnimState.JUMP:
                m_animtion.Play("Jump");
                break;
		}
	}
}
