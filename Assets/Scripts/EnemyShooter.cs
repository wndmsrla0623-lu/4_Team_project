using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float moveSpeed = 2f;
    public float keepDistance = 8f;
    public float fireRange = 12f;
    public float fireInterval = 2f;
    public float bulletSpeed = 10f;

    Transform player;
    float lastFireTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
        lastFireTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        float distance = toPlayer.magnitude;
        transform.forward = toPlayer.normalized;

        if (distance > keepDistance)
        {
            float move = moveSpeed * Time.deltaTime;
            transform.position += transform.forward * move;
        }

        bool inRange = distance < fireRange;
        bool ready = Time.time >= lastFireTime + fireInterval;
        if (inRange && ready)
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 target = player.position + Vector3.up * 1f;
        Vector3 dir = (target - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab,
            firePoint.position, Quaternion.LookRotation(dir));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = dir * bulletSpeed;
    }
}
