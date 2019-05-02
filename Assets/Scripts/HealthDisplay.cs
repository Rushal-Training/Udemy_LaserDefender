using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
	TextMeshProUGUI healthText;
	Player player;

	void Start()
	{
		healthText = GetComponent<TextMeshProUGUI>();
		player = FindObjectOfType<Player>();
	}

	void Update()
	{
		healthText.text = player.Health.ToString();
	}
}
