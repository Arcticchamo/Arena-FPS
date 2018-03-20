using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private int m_health = 100;
    private int m_targetsKilled = 0;

    public UIManager m_uiManager;

    public void SetHealth(int _health)
    {
        m_health += _health;
    }

    public void SetTargetsKilled(int _targetsKilled)
    {
        m_targetsKilled += _targetsKilled;
        m_uiManager.SetKillText(m_targetsKilled);
    }
}
