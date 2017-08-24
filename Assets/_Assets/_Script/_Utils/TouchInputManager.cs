using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : Singleton<TouchInputManager> {

    public GameObject m_player;
    public GameObject m_joystick;
    public GameObject m_hitButton;
    public GameObject m_jumpButton;

    ETCJoystick m_joystickScript;
    ETCButton m_hitButtonScript;
    ETCButton m_jumpButtonScript;
    Controller m_playerController;

	void Start () {
        m_joystickScript = m_joystick.GetComponent<ETCJoystick>();
        m_joystickScript.onMove.AddListener(OnMove);
        m_joystickScript.onMoveEnd.AddListener(OnMoveEnd);

        m_hitButtonScript = m_hitButton.GetComponent<ETCButton>();
        m_hitButtonScript.onDown.AddListener(OnHit);
        //m_hitButtonScript.onUp.AddListener(OnMoveEnd);
        m_jumpButtonScript = m_jumpButton.GetComponent<ETCButton>();
        m_jumpButtonScript.onDown.AddListener(OnJump);
        //m_jumpButtonScript.onUp.AddListener(OnMoveEnd);

        m_playerController = m_player.GetComponent<Controller>();
	}
	
	void Update () {
		
	}

    void OnMove(Vector2 dir) {
        m_playerController.OnMove(dir.x, dir.y);
    }

    void OnMoveEnd() {
        m_playerController.OnMoveEnd();
    }

    void OnHit() {
        m_playerController.Hit();
    } 

    void OnJump() {
        m_playerController.Jump();
    }
}
