using UnityEngine;

public class TagEffectManager : MonoBehaviour 
{
	[SerializeField] Light flashLight;
	[SerializeField] AudioSource gunAudio;
	[SerializeField] GameObject impactPrefab;

	ParticleSystem impactEffect;

	//Create the impact effect for our shots
	public void Initialize()
	{
//		impactEffect = Instantiate(impactPrefab).GetComponent<ParticleSystem>();
	}

	//Play muzzle flash and audio
	public void PlayTagEffects()
	{
		// Adjust flashlight intensity
		gunAudio.Stop();
		gunAudio.Play();
	}

	//Play impact effect and target position
	public void PlayImpactEffect(Vector3 impactPosition)
	{
//		impactEffect.transform.position = impactPosition;   
//		impactEffect.Stop();
//		impactEffect.Play();
	}
}