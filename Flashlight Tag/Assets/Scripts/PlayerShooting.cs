﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour 
{
	[SerializeField] float shotCooldown = .3f;
	[SerializeField] int killsToWin = 5;
	[SerializeField] Transform firePosition;
	[SerializeField] TagEffectManager tagEffects;

	[SyncVar (hook = "OnScoreChanged")] int score;

	Player player;
	float ellapsedTime;
	bool canShoot;

	void Start()
	{
		player = GetComponent<Player> ();
		tagEffects.Initialize ();

		if (isLocalPlayer)
			canShoot = true;
	}

	[ServerCallback]
	void OnEnable()
	{
		score = 0;
	}

	void Update()
	{
		if (!canShoot)
			return;

		ellapsedTime += Time.deltaTime;

		if (Input.GetButtonDown ("Fire1") && ellapsedTime > shotCooldown) 
		{
			ellapsedTime = 0f;
			CmdFireShot (firePosition.position, firePosition.forward);
		}
	}

	[Command]
	void CmdFireShot(Vector3 origin, Vector3 direction)
	{
		RaycastHit hit;

		Ray ray = new Ray (origin, direction);
		Debug.DrawRay (ray.origin, ray.direction * 3f, Color.red, 1f);

		bool result = Physics.Raycast (ray, out hit, 50f);

		if (result) 
		{
			PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth> ();

			if (enemy != null) 
			{
				bool wasKillShot = enemy.TakeDamage ();

				if (wasKillShot && ++score >= killsToWin)
					player.Won ();          
			}
		}

		RpcProcessShotEffects (result, hit.point);
	}

	[ClientRpc]
	void RpcProcessShotEffects(bool playImpact, Vector3 point)
	{
		tagEffects.PlayTagEffects ();

		if (playImpact)
			tagEffects.PlayImpactEffect (point);
	}

	void OnScoreChanged(int value)
	{
		score = value;
		if (isLocalPlayer)
			PlayerCanvas.canvas.SetKills (value);
	}

	public void FireAsBot()
	{
		CmdFireShot (firePosition.position, firePosition.forward);
	}
}