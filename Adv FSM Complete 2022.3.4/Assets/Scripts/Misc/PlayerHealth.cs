using UnityEngine;
using UnityEngine.UI;  // for Slider

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int CurrentHealth { get; private set; }

    [Header("UI")]
    [SerializeField] private Slider healthBar; // drag your HealthBar here in Inspector

    private bool isDead;

    void Awake()
    {
        CurrentHealth = maxHealth;
        isDead = false;

        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            int dmg = 50; 
            var b = collision.gameObject.GetComponent<Bullet>();
            if (b) dmg = b.damage;

            TakeDamage(dmg);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        CurrentHealth -= amount;
        if (CurrentHealth < 0) CurrentHealth = 0;

        if (healthBar) healthBar.value = CurrentHealth;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        var rb = GetComponent<Rigidbody>();
        if (rb) rb.velocity = Vector3.zero;

        var controller = GetComponent<PlayerTankController>();
        if (controller) controller.enabled = false;

        GameManager.Instance.ShowDeathScreen();
    }
}
