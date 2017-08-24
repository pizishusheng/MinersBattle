using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowControl : MonoBehaviour {

    public float m_DampTime = 1.5f;
    public Transform m_followTarget;

    private Camera m_Camera;
    private Vector3 m_currentPosition;
    private Vector3 m_MoveVelocity;

    float m_initHeight = 50f;
    float m_heightDamping = 2.0f;
    float m_rotationDamping = 3.0f;
    float m_distance = 10.0f;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    void Start () {
        
	}

  //  private void Update()
  //  {
		//if (m_followTarget != null)
			//FollowMove();
    //}

    private void LateUpdate()
    {
		if (m_followTarget != null)
			FollowMove();
    }

    void FollowMove () {
        float wantedRotationAngle = m_followTarget.eulerAngles.y;
        float wantedHeight = m_followTarget.position.y + m_initHeight;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, m_rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, m_heightDamping * Time.deltaTime);

        // 将角度变为rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // 
        transform.position = m_followTarget.position;
        transform.position -= currentRotation * Vector3.forward * m_distance;

        // 设置相机高度
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z - 50);
        transform.LookAt(m_followTarget);
    }
}
