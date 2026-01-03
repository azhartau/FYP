using UnityEngine;
using Worq.AEAI.Enemy;
public class EnemyDetectionAndAttack : MonoBehaviour
{
    public float detectionRange = 10f;  
    public string playerTag = "Player";
    public bool detect = false;
    private Transform player;
    void Update()
    {
        DetectPlayer();     
    }
    void DetectPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj == null)
            return;
        float distance = Vector3.Distance(transform.position, playerObj.transform.position);
        if (distance <= detectionRange)
        {
            player = playerObj.transform;
            detect = true;
        }
        else
        {
            player = null;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}