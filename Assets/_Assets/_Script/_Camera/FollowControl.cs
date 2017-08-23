﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowControl : MonoBehaviour {

    public float m_DampTime = 1.0f;
    public float m_mouseSpeed = 5f;
    public float m_zoomSpeed = 2f;
    public GameObject m_followTarget;
    public Vector3 m_initPosition;

    private Camera m_Camera;
    private Vector3 m_DesiredPosition;
    private Vector3 m_MoveVelocity;
    private Vector3 m_lookAtOffset = new Vector3(0, 5, 100);

    float m_distance;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    void Start () {
        //transform.position = m_followTarget.transform.position + m_initPosition;
	}
	
	
	void Update () {
        if (m_followTarget != null)
            FollowMove();

        if (Input.GetMouseButton(1))
        {
            ViewRotate();
        }
	}

    void FollowMove () {
        Vector3 lookAtPos = m_followTarget.transform.position + m_lookAtOffset;
		transform.LookAt(lookAtPos);
		m_DesiredPosition = m_followTarget.transform.position + m_initPosition;
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }

    void ViewRotate()
    {
        Vector2 delta = Input.mouseScrollDelta;

        float offset = -100f;

        float angle = offset * m_mouseSpeed;

        //Quaternion targetRot = Quaternion.Euler(0f, angle, 0f);
        m_followTarget.transform.rotation = Quaternion.Lerp(m_followTarget.transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * 0.5f);
    }
}
