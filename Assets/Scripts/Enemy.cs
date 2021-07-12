using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float health = 200;
    [SerializeField] int scoreWorth = 150;

    [Header("Audio")]
    [SerializeField] AudioClip deathAudio;
    [SerializeField] [Range(0,1)] float deathAudioVolume = 2f;
    [SerializeField] AudioClip projectileAudio;
    [SerializeField] [Range(0, 1)] float projectileAudioVolume = 1f;

    [Header("Death animation")]
    [SerializeField] float deathAnimationLength = 0.01f;
    [SerializeField] GameObject deathParticle;

    [Header("Shooting Parameters")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    // Start is called before the first frame update

    GameSession gameSession;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ResetCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (!projectile.Equals(null))
        {
            CountDownAndShoot();
        }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            ResetCounter();
        }
    }

    private void ResetCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }


    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity)
                    as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        if (projectileAudio)
        {
            AudioSource.PlayClipAtPoint(projectileAudio, Camera.main.transform.position, projectileAudioVolume);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.Damage;
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathParticle)
        {
            GameObject explosion = Instantiate(deathParticle, transform.position, transform.rotation);
        }
        if (deathAudio)
        {
            AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position, deathAudioVolume);
        }
        Destroy(gameObject, deathAnimationLength);
        gameSession.AddScore(scoreWorth);
        
    }
}
