using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : Singleton<TouchInputManager> {

    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_joystick;
    [SerializeField] GameObject m_hitButton;
    [SerializeField] GameObject m_jumpButton;
    [SerializeField] GameObject m_leftWeapon;
    [SerializeField] GameObject m_rightWeapon;

    ETCJoystick m_joystickScript;
    ETCButton m_hitButtonScript;
    ETCButton m_jumpButtonScript;
    ETCButton m_leftWeaponButScript;
    ETCButton m_rightWeaponButScript;
    Controller m_playerController;

	void Start () {
        m_joystickScript = m_joystick.GetComponent<ETCJoystick>();
        m_joystickScript.onMove.AddListener(OnMove);
        m_joystickScript.onMoveEnd.AddListener(OnMoveEnd);

        m_hitButtonScript = m_hitButton.GetComponent<ETCButton>();
        m_hitButtonScript.onDown.AddListener(OnHit);
        m_hitButtonScript.onUp.AddListener(OnMoveEnd);
        m_jumpButtonScript = m_jumpButton.GetComponent<ETCButton>();
        m_jumpButtonScript.onDown.AddListener(OnJump);
        //m_jumpButtonScript.onUp.AddListener(OnMoveEnd);

        m_leftWeaponButScript = m_leftWeapon.GetComponent<ETCButton>();
        m_leftWeaponButScript.onDown.AddListener(OnEquipLeftWeapon);

        m_rightWeaponButScript = m_rightWeapon.GetComponent<ETCButton>();
        m_rightWeaponButScript.onDown.AddListener(OnEquipRightWeapon);

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

    void OnEquipLeftWeapon() {
        m_playerController.EquipWeapon(true);
    }

	void OnEquipRightWeapon()
	{
        m_playerController.EquipWeapon(false);
	}
}
