using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Player;

public class StringEvent : UnityEvent<string> {}

public class Controller : MonoBehaviour
{

    public int m_speed;
    [HideInInspector] public StringEvent m_pickUpEvent = new StringEvent();

    [SerializeField] Slider _bloodSlider;
    [SerializeField] Transform _leftArmIK;
    [SerializeField] Transform _rightArmIK;
    [SerializeField] Transform _weaponIK;

    AnimState m_currentState = AnimState.IDLE;
    Animator m_animator;
    bool m_isAttacking = false;
    bool m_isMoveOk = true;
    Vector3 m_curPos;

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }


    void Update()
    {

    }

    #region 人物动作相关------------------------------------------

    public void OnMove(float x, float z)
    {
        Vector3 dir = new Vector3(x, 0, z);
        dir.Normalize();
        Move(dir);
        Rotate(dir);
    }

    public void OnMoveEnd()
    {
        if (m_currentState != AnimState.IDLE)
        {
            m_speed = 20;
            m_animator.SetBool("IsMove", false);
            m_currentState = AnimState.IDLE;
        }

        if (m_isAttacking)
        {
            m_animator.SetTrigger("Go2Attack");
            m_isAttacking = false;
        }
    }

    void Move(Vector3 direction)
    {
        m_animator.SetBool("IsMove", true);
        m_currentState = AnimState.RUN;

        if (m_isMoveOk)
        {
            Vector3 movement = direction * m_speed * Time.deltaTime;
            Vector3 nextPos = transform.position + movement;
            Vector3 targetPos = Vector3.Lerp(transform.position, nextPos, Time.deltaTime * m_speed);
            transform.position = new Vector3(targetPos.x, 0, targetPos.z);
        }
    }

    void Rotate(Vector3 direction)
    {
        float angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * 10f);
    }

    public void Hit()
    {
        if (!m_isAttacking)
        {
            m_animator.SetTrigger("Go2Attack");
            m_isAttacking = true;
        }
    }

    public void Jump()
    {

    }
    #endregion 人物动作相关------------------------------------------


    #region  更换装备相关---------------------------------------

    public void ChangeWeapon(Transform weapon)
    {
        string wname = weapon.name;
        string[] arr = wname.Split('_');
        string flag = arr[1];

        if (flag == "Tools")
        {
            if (!_weaponIK.gameObject.activeSelf)
            {
                Change(_weaponIK, weapon);
                Destroy(_weaponIK.gameObject);
                _weaponIK = weapon;
                return;
            }

            if (!_leftArmIK.gameObject.activeSelf)
            {
                Change(_leftArmIK, weapon);
                Destroy(_leftArmIK.gameObject);
                _leftArmIK = weapon;
                return;
            }

            if (!_rightArmIK.gameObject.activeSelf)
            {
                Change(_rightArmIK, weapon);
                Destroy(_rightArmIK.gameObject);
                _rightArmIK = weapon;
                return;
            }
        }
    }

    public void EquipWeapon(bool isLeft)
    {
        if (isLeft)
        {
            if (_leftArmIK.gameObject.activeSelf)
            {
                Transform newWeapon = Instantiate(_weaponIK) as Transform;
                Change(_leftArmIK, newWeapon);
                Change(_weaponIK, _leftArmIK);
                Destroy(_weaponIK.gameObject);
                _weaponIK = _leftArmIK;
                _leftArmIK = newWeapon;
            }
        }
        else
        {
            if (_rightArmIK.gameObject.activeSelf)
            {
                Transform newWeapon = Instantiate(_weaponIK) as Transform;
                Change(_rightArmIK, newWeapon);
                Change(_weaponIK, _rightArmIK);
                Destroy(_weaponIK.gameObject);
                _weaponIK = _rightArmIK;
                _rightArmIK = newWeapon;
            }
        }
    }

    void Change(Transform oldObj, Transform newObj)
    {
        newObj.parent = oldObj.parent;
        newObj.position = oldObj.position;
        newObj.localScale = oldObj.localScale;
        newObj.localRotation = oldObj.localRotation;
    }

    #endregion 更换装备相关----------------------------------------------

#region  背包中的使用物品
    public void PickUpItem (string itemName) {
        m_pickUpEvent.Invoke(itemName);
    }

    public void UseItems () {
        _bloodSlider.value += 0.1f;
    }
#endregion
}
