using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] GameObject laserPrefab;
	[SerializeField] float projectileSpeed = 15f;
	[SerializeField] float health = 100f;
	[SerializeField] float minTimeBetweenShots = 0.2f;
	[SerializeField] float maxTimeBetweenShots = 0.3f;

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
	}

	private void ProcessHit( DamageDealer damageDealer )
	{
		health -= damageDealer.Damage;
		if ( health <= 0 )
		{
			Destroy( gameObject );
		}
	}
}
