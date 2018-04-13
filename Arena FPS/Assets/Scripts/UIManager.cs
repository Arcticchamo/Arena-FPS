using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text m_kills;
	public Text m_ammoRemaining;

    void Start()
    {
        m_kills.text = "Kills: 0";
		m_ammoRemaining.text = "Ammo: 0";
    }

    public void SetKillText(int _kills)
    {
        m_kills.text = "Kills: " + _kills.ToString();
    }

	public void SetAmmoRemaining(int _ammo)
	{
		m_ammoRemaining.text = "Ammo: " + _ammo.ToString();
	}
}
