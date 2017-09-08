using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputManager : Singleton<TouchInputManager> {

    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_joystick;
    [SerializeField] GameObject m_hitButton;
    [SerializeField] GameObject m_jumpButton;
    [SerializeField] GameObject m_leftWeapon;
    [SerializeField] GameObject m_rightWeapon;

    [SerializeField] List<Button> _itemList;

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

        m_playerController.m_pickUpEvent.AddListener(OnPickUpItem);
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

    void OnPickUpItem (string itemName) {
        foreach (Button item in _itemList) {
            Text nameText = item.transform.Find("Text").GetComponent<Text>();
            if (item.IsActive()) {
                if (nameText.text == itemName) {
                    Transform image = item.transform.Find("Image");
                    Text numText = image.Find("Text").GetComponent<Text>();
                    numText.text = "2";
                    break;
                }
                continue;
            }

            nameText.text = itemName;
            item.onClick.AddListener(OnUseItem);
            item.gameObject.SetActive(true);
            break;
        } 
    }

    void OnUseItem () {
        m_playerController.UseItems();
    }
}
