using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int CurrentHealth { get; private set; }
    private bool isDead;

    void Awake()
    {
        CurrentHealth = maxHealth;
        isDead = false;
    }

    // Bullets already destroy themselves on collision (see Bullet.cs),
    // so we only need to read damage here before the bullet disappears.
    void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            int dmg = 50; // default if Bullet component not found
            var b = collision.gameObject.GetComponent<Bullet>();
            if (b) dmg = b.damage;

            TakeDamage(dmg);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Optional: freeze player controls/motion
        var rb = GetComponent<Rigidbody>();
        if (rb) rb.velocity = Vector3.zero;
        var controller = GetComponent<PlayerTankController>();
        if (controller) controller.enabled = false;

        GameManager.Instance.ShowDeathScreen(); // implemented in Step 2
    }
}
