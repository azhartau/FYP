using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlledHealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Damage Settings")]
    public float damagePerSecond = 2f;

    [Header("External Control Bools")]
    public bool detection = false;
    public bool totalHealthZero = false;

    [Header("UI")]
    public Slider healthBar;
    public GameObject failPanel;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        if (failPanel != null)
            failPanel.SetActive(false);
    }
    void Update()
    {
        if (detection && !totalHealthZero)
        {
            currentHealth -= damagePerSecond * Time.deltaTime;

            if (currentHealth <= 0)
            {
                KillPlayer();
            }
        }

        if (healthBar != null)
            healthBar.value = currentHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Die"))
        {
            KillPlayer();
        }
    }
    void KillPlayer()
    {
        if (totalHealthZero) return;  

        currentHealth = 0;
        totalHealthZero = true;

        StartCoroutine(ShowFailPanelAfterDelay());

        Debug.Log("Player Dead");
    }

    IEnumerator ShowFailPanelAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (failPanel != null)
            failPanel.SetActive(true);
    }
}
