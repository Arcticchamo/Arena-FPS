using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float m_damage = 10.0f; //Damage of each shot
    public float m_range = 100.0f; //Range of each bullet
    public float m_impact = 30.0f; //Used to simulate physics
    public float m_fireRate = 30.0f; //How fast the gun can fire
    public float m_reloadTime = 5.0f; //How fast the gun reloads
    public int m_clipSize = 30; //How many bullets are stored in the gun

    public GunAudioManager m_gunAudioManager; //Allows each gun to use a different audio clip for each sound

    public Camera m_FPS_cam;
    public PlayerStats m_playerStats;
    public ParticleSystem m_muzzelFlash;
    public GameObject m_bulletFlash;

    private float m_nextTimeToFire = 0.0f;
    private int m_bulletsFired = 0;

    [SerializeField]
    private bool m_semiAutomatic = true;

    [SerializeField]
    private bool m_reloading = false;

    void OnEnable()
    {
        m_reloading = false;    
    }

    void Update()
    {
        if (m_reloading == false)
        {
            if (m_bulletsFired >= m_clipSize)
            {
                m_reloading = true;
                StartCoroutine(Reloading());
            }
            else
            {
                if (m_semiAutomatic) SemiAutomatic();
                else Automatic();
            }
        }
    }

    void SemiAutomatic()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= m_nextTimeToFire)
        {
            m_nextTimeToFire = Time.time + 1.0f / m_fireRate;
            Shoot();
        }
    }

    void Automatic()
    {
        if (Input.GetButton("Fire1") && Time.time >= m_nextTimeToFire)
        {
            m_nextTimeToFire = Time.time + 1.0f / m_fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        m_muzzelFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(m_FPS_cam.transform.position, m_FPS_cam.transform.forward, out hit, m_range))
        {
            m_gunAudioManager.Firing();

            Target target = hit.transform.GetComponent<Target>();

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

            GameObject BFlash = Instantiate(m_bulletFlash, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(BFlash, 1.0f);

            m_bulletsFired++;
        }
    }

    IEnumerator Reloading()
    {
        //Start reload animation 
        Debug.Log("Start Reloading");
        m_gunAudioManager.Reloading();
        yield return new WaitForSeconds(m_reloadTime);
        Debug.Log("Stop Reloading");
        m_bulletsFired = 0;
        m_reloading = false;
    }
}
