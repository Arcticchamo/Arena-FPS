using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {

	public float m_speed; //How fast the bullet travels
	public float m_preAppliedForce; //The amount of force instantly applied upon firing the bullet
	public float m_bulletDrop; //How much the bullet drops per frame

	Rigidbody m_rigidbody;

	public GameObject m_bulletFlash
	{
		get;
		set;
	}

	public bool m_bulletFired //Tracks if the bullet has been fired
	{
		get;
		set;
	} 

	void Start () 
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () 
	{
		if (m_rigidbody.velocity == Vector3.zero) 
				m_rigidbody.AddForce((gameObject.transform.right * Time.deltaTime) * m_preAppliedForce);
		
		m_rigidbody.AddForce((gameObject.transform.right * Time.deltaTime) * m_speed);
	}

	public void ResetTransform(Vector3 _position, Quaternion _rotation)
	{
		gameObject.transform.position = _position;
		gameObject.transform.rotation = _rotation ;
		gameObject.transform.Rotate(new Vector3(0, -90, 0));
	}

	void OnCollisionEnter(Collision _col)
	{
		//Here check to see if the bullet has hit...
		if (_col.transform.tag == "Player")
		{
			//Another Player
			//deal damage
			SpawnCollision(_col);
		}
		else if (_col.transform.tag == "Enemy")
		{
			//An Enemy
			//deal damage
			SpawnCollision(_col);
		}
		else if (_col.transform.tag == "Environment")
		{
			//Environment
			//deal damage
			SpawnCollision(_col);
		}
		else
		{
			//Anything else
			//deal damage
			SpawnCollision(_col);
		}

		/*
		     if (target != null)
            {
                target.TakeDamage(m_damage);

                if (target.GetHealth() <= 0.0f)
                {
                    m_playerStats.SetTargetsKilled(1);
                }
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * m_impact);
            }
		 */
	}

	void SpawnCollision(Collision _col)
	{
		ContactPoint contact = _col.contacts[0];
		GameObject BFlash = Instantiate(m_bulletFlash, contact.point, Quaternion.LookRotation(contact.normal));
		Destroy(BFlash, 1.0f);
		gameObject.SetActive(false);
	}
}
