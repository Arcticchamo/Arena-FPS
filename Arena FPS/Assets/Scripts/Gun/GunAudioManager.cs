using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudioManager : MonoBehaviour {

    private AudioSource m_audioSource;

    public AudioClip m_reloading;
    public AudioClip m_firing;
    public AudioClip m_changingWeapon;

    public float m_volume = 1.0f;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void Reloading()
    {
        if (m_reloading != null)
        {
            m_audioSource.PlayOneShot(m_reloading, m_volume);
        }
    }

    public void Firing()
    {
        if (m_firing != null)
        {
            m_audioSource.PlayOneShot(m_firing, m_volume);
        }
    }

    public void ChangingWeapons()
    {
        if (m_changingWeapon != null)
        {
            m_audioSource.PlayOneShot(m_changingWeapon, m_volume);
        }
    }
}
