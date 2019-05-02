using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] GameObject laserPrefab;
	[SerializeField] float projectileSpeed = 15f;
	[SerializeField] float minTimeBetweenShots = 0.2f;
	[SerializeField] float maxTimeBetweenShots = 3f;

	[SerializeField] float health = 100f;
	[SerializeField] int scoreValue = 150;

	[SerializeField] GameObject deathVFX;
	[SerializeField] float durationOfExplosion = 1f;

	[SerializeField] AudioClip deathSFX;
	[Range( 0, 1 )] [SerializeField] float deathVolume = 0.75f;
	[SerializeField] AudioClip laserSFX;
	[Range( 0, 1 )] [SerializeField] float laserVolume = 0.05f;

	float shotCounter;

    void Start()
    {
		shotCounter = Random.Range( minTimeBetweenShots, maxTimeBetweenShots );
    }

    void Update()
    {
		CountdownAndShoot();
    }

	private void OnTriggerEnter2D( Collider2D other )
	{
		DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

		if ( !damageDealer ) { return; }

		ProcessHit( damageDealer );
	}

	private void CountdownAndShoot()
	{
		shotCounter -= Time.deltaTime;
		if( shotCounter <= 0f )
		{
			Fire();
			shotCounter = Random.Range( minTimeBetweenShots, maxTimeBetweenShots );
		}
	}

	private void Fire()
	{
		GameObject laser = Instantiate( laserPrefab, transform.position, Quaternion.identity ) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector2( 0, -projectileSpeed );
		AudioSource.PlayClipAtPoint( laserSFX, Camera.main.transform.position, laserVolume );
	}

	private void ProcessHit( DamageDealer damageDealer )
	{
		health -= damageDealer.Damage;
		damageDealer.Hit();

		if ( health <= 0 )
		{
			Die();
		}
	}

	private void Die()
	{
		FindObjectOfType<GameSession>().Score = scoreValue;

		Destroy( gameObject );

		AudioSource.PlayClipAtPoint( deathSFX, Camera.main.transform.position, deathVolume );

		GameObject explosion = Instantiate( deathVFX, transform.position, Quaternion.identity );
		Destroy( explosion, durationOfExplosion );
	}
}
