using UnityEngine;

public class Target : MonoBehaviour {

    public Target m_parent; //Reference to the parent

    public float m_health = 50.0f; //Health per object 
    public float m_armour = 10.0f; //Armour currently on the object

    public void TakeDamage(float _damage)
    {
        m_health -= _damage;
        if (m_health <= 0.0f)
        {
            DestroyObject();
        }
    }

    public void ParentTakeDamage(float _damage)
    {
        m_health -= _damage;

        if (m_health <= 0.0f)
        {
            DestroyObject();
        }
    }

    private float ArmourReduction(float _damage)
    {
        return _damage;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return m_health;
    }
}
