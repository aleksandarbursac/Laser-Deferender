using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioClip deathAudio;
    [SerializeField] [Range(0, 1)] float deathAudioVolume = 2f;
    [SerializeField] AudioClip projectileAudio;
    [SerializeField] [Range(0, 1)] float projectileAudioVolume = 0.5f;

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = 1f;
    [SerializeField] float yPaddingBottom = 1f;
    [SerializeField] float yPaddingTop = 3f;
    [SerializeField] int health = 200;
 
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float firingSpeed = 0.1f;

    [Header("Particles")]
    [SerializeField] GameObject playerHitParticles;

    Coroutine firingCoroutine;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    IEnumerator PrintSomething()
    {
        Debug.Log("something");
        yield return new WaitForSeconds(3);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        } 
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity)
                    as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(firingSpeed);
            if (projectileAudio)
            {
                AudioSource.PlayClipAtPoint(
                    projectileAudio, 
                    Camera.main.transform.position, 
                    projectileAudioVolume);
            }
        }
    }

    private void Move()
    {
        //delta is difference between something. deltaTime is difference between the last frame
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        //world space value of the x axis
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPaddingBottom;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPaddingTop;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.Damage;
        damageDealer.Hit();
        if (playerHitParticles)
        {
            GameObject explosion = Instantiate(playerHitParticles, transform.position, transform.rotation);
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        if (deathAudio)
        {
            AudioSource.PlayClipAtPoint(deathAudio,
                    Camera.main.transform.position);
        }
    }

    public int ReturnHealth(){
        return health;
    }
}
