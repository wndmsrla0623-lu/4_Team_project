
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectRange = 15f;
    public float attackRange = 1.5f;
    public int damage = 1;
    public float attackCooldown = 1f;

    public Transform model;
    public float modelRotationOffset = 180f;

    Transform player;
    float lastAttackTime = -999f;

    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");

        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;

        float distance = toPlayer.magnitude;

        if (distance > detectRange) return;

        if (distance > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(toPlayer);

            if (model != null)
            {
                model.rotation = targetRotation *
                    Quaternion.Euler(0f, modelRotationOffset, 0f);
            }
            else
            {
                transform.rotation = targetRotation;
            }
        }

        if (distance > attackRange)
        {
            transform.position += toPlayer.normalized *
                moveSpeed * Time.deltaTime;
        }
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            Health hp = player.GetComponent<Health>();

            if (hp != null)
                hp.TakeDamage(damage);
        }
    }
}
