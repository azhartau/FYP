using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinTriggerParticles : MonoBehaviour
{
    [Header("Particle Prefabs")]
    public GameObject particlePrefab1;
    public GameObject particlePrefab2;
    [Header("Sound Settings")]
    public AudioClip winSound;
    public float soundVolume = 1f;
    [Header("Win Screen")]
    public GameObject winScreen;
    public float winScreenDelay = 5f;
    [Header("Spawn Settings")]
    public Vector3 spawnOffset = Vector3.zero;
    private bool hasPlayed = false;
    private AudioSource audioSource;
    
    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (winScreen != null)
            winScreen.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;
            PlayParticles();
            PlaySound();
            StartCoroutine(ShowWinScreenAfterDelay());
        }
    }
    public void PlayParticles()
    {
        if (particlePrefab1 != null)
            Instantiate(particlePrefab1, transform.position + spawnOffset, Quaternion.identity);

        if (particlePrefab2 != null)
            Instantiate(particlePrefab2, transform.position + spawnOffset, Quaternion.identity);
    }
    public void PlaySound()
    {
        if (winSound != null)
        {
            audioSource.volume = soundVolume;
            audioSource.PlayOneShot(winSound);
        }
    }
    IEnumerator ShowWinScreenAfterDelay()
    {
        yield return new WaitForSeconds(winScreenDelay);
        if (winScreen != null)
            winScreen.SetActive(true);

        Time.timeScale = 0f;
    }
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void ReplayLevel()
    {
        Time.timeScale = 1f; // game normal speed par lao
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene("reload");
    }
}
