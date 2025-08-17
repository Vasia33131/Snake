using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Settings")]
    [Range(0, 1)] public float musicVolume = 0.5f;
    [Range(0, 1)] public float sfxVolume = 0.7f;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip eatSound;

    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudio()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        PlayBackgroundMusic(); // Автоматически запускаем музыку
    }

    // Добавляем недостающий метод
    public void PlayBackgroundMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void PlayEatSound()
    {
        AudioSource.PlayClipAtPoint(eatSound, Camera.main.transform.position, sfxVolume);
    }

    public void UpdateMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
    }

    public void UpdateSfxVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
}