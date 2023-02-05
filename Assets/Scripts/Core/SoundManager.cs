using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
	public static SoundManager instance = null;
	[Range(.01f, .5f)][SerializeField] private float fadeInTime = .05f;

	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private AudioSource MusicAudioSource;
	[SerializeField] private AudioSource SFXAudioSource;
	[SerializeField] private AudioSource ambientAudioSource;
	[SerializeField] private AudioSource SpookyAudioSource;
	[Space]
	[SerializeField] private AudioClip[] music;
	[SerializeField] private AudioClip ambientClip;
	[SerializeField] private AudioClip[] SpookyClips;


	[Space]
	[SerializeField] private AudioClip buttonClick;

	[Space]
	[Header("Player Sounds")]
	[SerializeField] private AudioClip movementClips;
	[SerializeField] private AudioClip shootClip;
	[SerializeField] private AudioClip jumpClip;
	[SerializeField] private AudioClip swingAxeClip;
	[SerializeField] private AudioClip swingAxeImpactClip;
	[SerializeField] private AudioClip axeImpactClip;
	[SerializeField] private AudioClip playerDeathClip;
	[SerializeField] private AudioClip[] HurtClips;

	[Space]
	[Header("Boss Sounds")]
	[SerializeField] private AudioClip swipeClip;
	[SerializeField] private AudioClip walkClip;
	[SerializeField] private AudioClip rootAttackClip;

	public int MusicArrayLength { get => music.Length; }
	public bool InBossZone { get; set; }

	private float audioVolume = 1f;
	private int clipIndex = 0;

	void Awake(){
		if (instance != null){
			Destroy(gameObject);
		} else{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		if (PlayerPrefs.HasKey("master_volume")) {
			ChangeMasterVolume(PlayerPrefsManager.GetMasterVolume());
		}
		else {
			ChangeMasterVolume(-20f);
		}

		if (PlayerPrefs.HasKey("music_volume")) {
			ChangeMusicVolume(PlayerPrefsManager.GetMusicVolume());
		}

		if (PlayerPrefs.HasKey("sfx_volume")) {
			ChangeSFXVolume(PlayerPrefsManager.GetSFXVolume());
		}
	}

	private void Update(){
		if (ambientAudioSource.isPlaying) {

		}
		if (LevelManager.instance.currentScene == "Level 1") {
			PlayAmbient();
			PlayRandomAmbient();
		}

		if (music.Length > 0)
		{
			if (GameController.instance.isInBossZone && LevelManager.instance.currentScene == "Level 1")
			{
				MusicAudioSource.clip = music[2];
			}
			else
			{
				MusicSelect();
			}
		}
		VolumeFadeIn(MusicAudioSource);
		VolumeFadeIn(ambientAudioSource);
		VolumeFadeIn(SpookyAudioSource);
	}

	void VolumeFadeIn(AudioSource audioSource) {
		if (audioVolume <= 1f){
			audioVolume += fadeInTime * Time.deltaTime;
			audioSource.volume = audioVolume;
		} else{
			audioVolume = 1f;
		}

		if (audioSource.clip != null){
			if (!audioSource.isPlaying){
				audioSource.Play();
				audioSource.volume = 0f;
				audioVolume = 0f;
			}
		}
	}

	void VolumeFadeOut(AudioSource audioSource) {
		if (audioVolume >= 1f){
			audioVolume -= fadeInTime * Time.deltaTime;
			audioSource.volume = audioVolume;
		} else{
			audioVolume = 0f;
		}

		if (audioSource.volume <= 0f){
			audioSource.Stop();
		}
	}

	public void MusicSelect()
	{
		//if (GameController.instance.isInBossZone)
		//{
		//	MusicAudioSource.clip = music[2];
		//}
		//else
		//{
			switch (LevelManager.instance.currentScene)
			{
				case "Main Menu":
					MusicAudioSource.clip = music[0];
					break;
				case "Options":
					MusicAudioSource.clip = music[0];
					break;
				case "Level 1":
					MusicAudioSource.clip = music[1];
					break;
				case "Credits":
					MusicAudioSource.clip = music[0];
					break;
				default:
					break;
			}
		//}
	}

	void PlayAmbient()
	{
		if (!ambientAudioSource.isPlaying)
		{
			ambientAudioSource.PlayOneShot(ambientClip);
		}
	}
	void PlayRandomAmbient()
	{
		if (!SpookyAudioSource.isPlaying && SpookyClips.Length > 0) {
			clipIndex = Random.Range(0, SpookyClips.Length);
			SpookyAudioSource.PlayOneShot(SpookyClips[clipIndex]);
		}
	}

	public void PlayMusicForScene(int index)
	{
		if (music.Length > 0)
		{
			MusicAudioSource.clip = music[index];
			//if (LevelManager.instance.currentScene == "MikeTest")
			//{
			//	MusicAudioSource.volume = 0;
			//	audioVolume = 0f;
			//}
		}
	}

	public void StartAudio(){
		MusicAudioSource.Play();
	}

	public void SetButtonClip(){
		if(buttonClick != null)
			SFXAudioSource.PlayOneShot(buttonClick, .5f);
	}

	public void PlayJumpClip()
	{
		if (jumpClip != null)
			SFXAudioSource.PlayOneShot(jumpClip, 2f);
	}

	public void PlaySwingAxeClip()
	{
		if (swingAxeClip != null)
			SFXAudioSource.PlayOneShot(swingAxeClip, 1f);
	}
	public void PlaySwingAxeImpactClip()
	{
		if (swingAxeImpactClip != null)
			SFXAudioSource.PlayOneShot(swingAxeImpactClip, 1f);
	}

	public void PlayPlayerDeathClip() {
		if (playerDeathClip != null)
			SFXAudioSource.PlayOneShot(playerDeathClip, .5f);
	}

	public void PlayAxeImpactClip()
	{
		if (axeImpactClip != null)
			SFXAudioSource.PlayOneShot(axeImpactClip, .5f);
	}

	public void PlayWalkClip() {
		if (movementClips != null)
			SFXAudioSource.PlayOneShot(movementClips, .3f);
	}

	public void PlayHurtClip() {
		clipIndex = Random.Range(0, HurtClips.Length);
		SFXAudioSource.PlayOneShot(HurtClips[clipIndex], .1f);
	}

	//public void PlayRunClip()
	//{
	//	if (movementClips[2] != null)
	//		SFXAudioSource.PlayOneShot(movementClips[2], .2f);
	//}

	public void PlayShootClip() {
		if (shootClip != null)
			SFXAudioSource.PlayOneShot(shootClip, .3f);
	}

	public void PlaySwipeClip()
	{
		if (swipeClip != null)
			SFXAudioSource.PlayOneShot(swipeClip, .3f);
	}
	public void PlayBossWalkClip()
	{
		if (walkClip != null)
			SFXAudioSource.PlayOneShot(walkClip, .3f);
	}
	public void PlayRootAttackClip()
	{
		if (rootAttackClip != null)
			SFXAudioSource.PlayOneShot(rootAttackClip, .3f);
	}

	public void ChangeMasterVolume(float volume) {
		audioMixer.SetFloat("Master", volume);
		if (volume == -40f){
			audioMixer.SetFloat("Master", -80f);
		}
	}

	public void ChangeMusicVolume(float volume){
		audioMixer.SetFloat("Music", volume);
		audioMixer.SetFloat("Ambient", volume - 6);
		if (volume == -40f){
			audioMixer.SetFloat("Music", -80f);
		}
	}

	public void ChangeSFXVolume(float volume){
		audioMixer.SetFloat("SFX", volume);
		if (volume == -40f){
			audioMixer.SetFloat("SFX", -80f);
		}
	}
}
