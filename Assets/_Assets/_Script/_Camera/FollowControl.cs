using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowControl : MonoBehaviour {

    public float m_DampTime = 1.0f;
    public GameObject m_followTarget;
    public Vector3 m_initPosition;

    private Camera m_Camera;
    private Vector3 m_DesiredPosition;
    private Vector3 m_MoveVelocity;
    private Vector3 m_lookAtOffset = new Vector3(0, 5, 100);

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
	}

    void FollowMove () {
        Vector3 lookAtPos = m_followTarget.transform.position + m_lookAtOffset;
		transform.LookAt(lookAtPos);
		m_DesiredPosition = m_followTarget.transform.position + m_initPosition;
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }
}
