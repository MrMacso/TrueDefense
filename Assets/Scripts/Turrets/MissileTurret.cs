using UnityEngine;

public class MissileTurret : MonoBehaviour
{
    public Transform partToRotate;
    public GameObject missile;
    public Transform firePoint;
    private Transform target;
    readonly float range = 40f;
    readonly float turnSpeed = 10f;
    readonly float fireRate = 3f;
    private float fireCountDown = 0f;
    //cost and amount varaibles can be found inside the Shop_Panel object
    void Start()
    {
        //checks the closest target every 0.5 second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        //finds the closest enemy
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortesDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    void Update()
    {
        if (target == null) return;
        //rotates body to closest enemy
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        //shoot by firerate
        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }
    private void Shoot()
    {
        //building bullet prefab
        GameObject bulletObject = Instantiate(missile, firePoint.position, firePoint.rotation);
        Missile bull = bulletObject.GetComponent<Missile>();
        if (bull != null)
        {
            //send bullet to the target
            bull.Seek(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
