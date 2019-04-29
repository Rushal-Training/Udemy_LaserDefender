using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float padding = 1f;
	[SerializeField] GameObject laserPrefab;
	[SerializeField] float projectileSpeed = 20f;

	float xMin, xMax, yMin, yMax;

    void Start()
    {
		SetupMoveBoundaries();
    }

	void Update()
    {
		Move();
		Fire();
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
		if ( Input.GetButton( "Fire1" ) )
		{
			GameObject laser = Instantiate( laserPrefab, transform.position, Quaternion.identity ) as GameObject;
			laser.GetComponent<Rigidbody2D>().velocity = new Vector2( 0, projectileSpeed );
		}
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
