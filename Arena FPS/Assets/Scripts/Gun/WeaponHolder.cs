using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {

    public List<Transform> m_weaponList;

    void Start()
    {
        m_weaponList = new List<Transform>();

        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
			weapon.GetComponent<Gun>().GenerateAmmoClip();
            m_weaponList.Add(weapon);
        }

        if (m_weaponList.Count != 0)
        {
            m_weaponList[0].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ScrollNextWeapon();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ScrollPreviousWeapon();
        }

        if (Input.GetKeyDown("1")) ChangeWeapon(1);
        if (Input.GetKeyDown("2")) ChangeWeapon(2);
        if (Input.GetKeyDown("3")) ChangeWeapon(3);
        if (Input.GetKeyDown("4")) ChangeWeapon(4);
        if (Input.GetKeyDown("5")) ChangeWeapon(5);
        if (Input.GetKeyDown("6")) ChangeWeapon(6);
        if (Input.GetKeyDown("7")) ChangeWeapon(7);
        if (Input.GetKeyDown("8")) ChangeWeapon(8);
        if (Input.GetKeyDown("9")) ChangeWeapon(9);
    }

    void ScrollNextWeapon()
    {
        for (int i = 0; i < m_weaponList.Count; i++)
        {
            if (m_weaponList[i].gameObject.activeInHierarchy)
            {
				//If the gun is currently reloading. Stop it.
				m_weaponList[i].gameObject.GetComponent<Gun>().StopReloading();
                m_weaponList[i].gameObject.SetActive(false);
                
                if (i == m_weaponList.Count - 1)
                { 
                    m_weaponList[0].gameObject.SetActive(true);
                    return;
                }
                else
                { 
                    m_weaponList[i + 1].gameObject.SetActive(true);
                    return;
                }
            }
        }
    }

    void ScrollPreviousWeapon()
    {
        for (int i = 0; i < m_weaponList.Count; i++)
        {
            if (m_weaponList[i].gameObject.activeInHierarchy)
            {
				m_weaponList[i].gameObject.GetComponent<Gun>().StopReloading();
                m_weaponList[i].gameObject.SetActive(false);

                if (i == 0)
                {
                    m_weaponList[m_weaponList.Count - 1].gameObject.SetActive(true);
                    return;
                }
                else
                {
                    m_weaponList[i - 1].gameObject.SetActive(true);
                    return;
                }
            }
        }
    }

    void ChangeWeapon(int _index)
    {
        if (_index > m_weaponList.Count) return;

        foreach (Transform weapon in m_weaponList)
        {
            weapon.gameObject.SetActive(false);
        }

        m_weaponList[_index - 1].gameObject.SetActive(true);
    }
}
