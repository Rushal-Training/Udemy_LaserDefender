using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header( "Player" )]
	[SerializeField] int health = 200;
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float padding = 1f;

	[Header("Projectile")]
	[SerializeField] GameObject laserPrefab;
	[SerializeField] float projectileSpeed = 20f;
	[SerializeField] float projectileFiringPeriod = 0.1f;

	[Header("Sound Effects")]
	[SerializeField] AudioClip deathSFX;
	[Range( 0, 1 )] [SerializeField] float deathVolume = 1f;
	[SerializeField] AudioClip laserSFX;
	[Range( 0, 1 )][SerializeField] float laserVolume = 0.05f;

	bool isFiring = false;
	float xMin, xMax, yMin, yMax;

	public int Health { get => health; }

	void Start()
    {
		SetupMoveBoundaries();
    }

	void Update()
    {
		Move();
		Fire();
    }

	private void OnTriggerEnter2D( Collider2D collision )
	{
		DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

		if (!damageDealer) { return; }

		ProcessHit( damageDealer );
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
		FindObjectOfType<Level>().LoadGameOver();
		Destroy( gameObject );
		AudioSource.PlayClipAtPoint( deathSFX, Camera.main.transform.position, deathVolume );
	}

	private void Move()
	{
		var deltaX = Input.GetAxis( "Horizontal" ) * Time.deltaTime * moveSpeed;
		var deltaY = Input.GetAxis( "Vertical" ) * Time.deltaTime * moveSpeed;

		var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax );
		var newYPos = Mathf.Clamp( transform.position.y + deltaY, yMin, yMax );

		transform.position = new Vector2( newXPos, newYPos );
	}

	private void Fire()
	{
		if ( Input.GetButtonDown( "Fire1" )  && !isFiring )
		{
			StartCoroutine(FireContinuously());
		}
	}

	IEnumerator FireContinuously()
	{
		isFiring = true;
		while( Input.GetButton( "Fire1" ) )
		{
			GameObject laser = Instantiate( laserPrefab, transform.position, Quaternion.identity ) as GameObject;
			laser.GetComponent<Rigidbody2D>().velocity = new Vector2( 0, projectileSpeed );
			AudioSource.PlayClipAtPoint( laserSFX, Camera.main.transform.position, laserVolume );
			yield return new WaitForSeconds( projectileFiringPeriod );
		}
		isFiring = false;
	}

	private void SetupMoveBoundaries()
	{
		Camera gameCamera = Camera.main;
		xMin = gameCamera.ViewportToWorldPoint( new Vector3( 0, 0, 0 ) ).x + padding;
		xMax = gameCamera.ViewportToWorldPoint( new Vector3( 1, 0, 0 ) ).x - padding;
		yMin = gameCamera.ViewportToWorldPoint( new Vector3( 0, 0, 0 ) ).y + padding;
		yMax = gameCamera.ViewportToWorldPoint( new Vector3( 0, 1, 0 ) ).y - padding;
	}
}
