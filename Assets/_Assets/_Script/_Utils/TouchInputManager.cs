﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : Singleton<TouchInputManager> {

    public GameObject m_player;
    public GameObject m_joystick;

    ETCJoystick m_joystickScript;

    Controller m_playerController;

	void Start () {
        m_joystickScript = m_joystick.GetComponent<ETCJoystick>();
        m_joystickScript.onMove.AddListener(OnMove);
        m_joystickScript.onMoveEnd.AddListener(OnMoveEnd);

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
}