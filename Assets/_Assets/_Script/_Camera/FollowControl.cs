using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowControl : MonoBehaviour {

    public float m_DampTime = 1.5f;
    public GameObject m_followTarget;
    public Vector3 m_initPosition;

    private Camera m_Camera;
    private Vector3 m_currentPosition;
    private Vector3 m_MoveVelocity;
    private Vector3 m_lookAtOffset = new Vector3(0, 5, 100);

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    void Start () {
		
	}
	
	
	void Update () {
        if (m_followTarget != null)
            FollowMove();

	}

    void FollowMove () {
		m_currentPosition = m_followTarget.transform.position + m_initPosition;
        transform.position = Vector3.SmoothDamp(transform.position, m_currentPosition, ref m_MoveVelocity, m_DampTime);

		Vector3 lookAtPos = m_followTarget.transform.position;
		transform.LookAt(lookAtPos);
    }
}
