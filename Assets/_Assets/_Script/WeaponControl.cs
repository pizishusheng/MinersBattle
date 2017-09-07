using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour {

    Collider m_collider;

	void Start () {
        m_collider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent != null)
            return;
        
        Controller control = other.transform.GetComponent<Controller>();

        if (control == null)
            return;
        
        control.ChangeWeapon(transform);
    }
}
