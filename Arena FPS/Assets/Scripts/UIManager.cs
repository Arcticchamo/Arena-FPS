using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text m_kills;

    void Start()
    {
        m_kills.text = "Kills: 0";
    }

    public void SetKillText(int _kills)
    {
        m_kills.text = "Kills: " + _kills.ToString();
    }
}
