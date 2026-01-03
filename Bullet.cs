using UnityEngine;
using Worq.AEAI.Enemy;

public class Bullet : MonoBehaviour
{
    public float damage = 20f;
    public float lifeTime = 3f;

    private bool hasHit = false;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit object is NOT Enemy");
            return;
        }
        EnemyAI enemy = collision.gameObject.GetComponentInParent<EnemyAI>();
        if (enemy == null)
        {
            return;
        }
        enemy.enemyTakeDamage();
        hasHit = true;
        Destroy(gameObject);
    }
}
