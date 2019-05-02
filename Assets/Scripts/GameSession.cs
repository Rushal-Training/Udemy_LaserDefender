using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
	int score = 0;

	public int Score { get => score; set => score += value; }

	void Awake()
	{
		SetupSingleton();
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void SetupSingleton()
	{
		if ( FindObjectsOfType( GetType() ).Length > 1 )
		{
			Destroy( gameObject );
		}
		else
		{
			DontDestroyOnLoad( gameObject );
		}
	}

	public void ResetGame()
	{
		Destroy( gameObject );
	}
}
