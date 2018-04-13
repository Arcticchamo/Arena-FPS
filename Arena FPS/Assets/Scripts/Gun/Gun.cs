using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float m_damage = 10.0f; //Damage of each shot
    public float m_range = 100.0f; //Range of each bullet
    public float m_impact = 30.0f; //Used to simulate physics
    public float m_fireRate = 30.0f; //How fast the gun can fire
    public float m_reloadTime = 5.0f; //How fast the gun reloads
   
	public int m_clipSize = 30; //How many bullets are stored in the gun
	public GameObject m_bullet; //The type of bullet the gun will use
	public GameObject[] m_ammoClip; //An array to store all the bullets.
	public Transform m_bulletObjectHolder; //An empty object to hold the bullets 

    public GunAudioManager m_gunAudioManager; //Allows each gun to use a different audio clip for each sound

	//Get Rid of the Raycasting and replace with bullets 
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

	private Transform m_transform;

    void OnEnable()
    {
        m_reloading = false;  
		m_playerStats.SetAmmoCount(m_ammoClip.Length - m_bulletsFired);
    }

	void Start()
	{
		m_transform = GetComponent<Transform>();

	}

	public void GenerateAmmoClip()
	{
		//Create and fill the clips. 
		//Each clip is an object pool
		m_ammoClip = new GameObject[m_clipSize];
		for (int i = 0; i < m_ammoClip.Length; i++)
		{
			m_ammoClip[i] = Instantiate(m_bullet);
			m_ammoClip[i].GetComponent<Bullets>().m_bulletFired = false;
			m_ammoClip[i].GetComponent<Bullets>().m_bulletFlash = m_bulletFlash;
			m_ammoClip[i].GetComponent<Transform>().SetParent(m_bulletObjectHolder);
			m_ammoClip[i].SetActive(false);
		}
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
			else if (Input.GetKeyDown(KeyCode.R)) //Start reloading
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
		for (int i = 0; i < m_ammoClip.Length; i++)
		{
			if (!m_ammoClip[i].GetComponent<Bullets>().m_bulletFired && !m_ammoClip[i].activeInHierarchy)
			{
				m_muzzelFlash.Play();
				m_gunAudioManager.Firing();
				m_ammoClip[i].GetComponent<Bullets>().m_bulletFired = true;
				m_ammoClip[i].GetComponent<Bullets>().ResetTransform(m_transform.position, m_transform.rotation);
				m_ammoClip[i].SetActive(true);
				m_bulletsFired++;
				m_playerStats.SetAmmoCount(m_ammoClip.Length - m_bulletsFired);
				break;
			}
		}
    }

    IEnumerator Reloading()
    {
        //Start reload animation 
        Debug.Log("Start Reloading");
        m_gunAudioManager.Reloading();
        yield return new WaitForSeconds(m_reloadTime);
        Debug.Log("Stop Reloading");

		for (int i = 0; i < m_ammoClip.Length; i++)
		{
			//Reset their activity
			m_ammoClip[i].GetComponent<Bullets>().m_bulletFired = false;
			m_ammoClip[i].SetActive(false); 
		}

        m_bulletsFired = 0;
        m_reloading = false;
		m_playerStats.SetAmmoCount(m_ammoClip.Length - m_bulletsFired);
    }

	public void StopReloading()
	{
		StopCoroutine(Reloading());
	}
}
