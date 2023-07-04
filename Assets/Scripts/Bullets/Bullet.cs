using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    readonly float speed = 40;
    readonly float damageAmount = 10;
    [SerializeField] private GameObject ImpactEffect;
    private AudioManager audioManager;
    
    public void Seek(Transform _target)
    { 
        target = _target;
    }
    private void Start()
    {
        //sound
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Hit");
    }
    // Update is called once per frame
    void Update()
    {
        
        if(target == null) 
        {
            Destroy(gameObject);
            return;
        }
        Vector3 add = new Vector3(0f, 2f, 0f);
        Vector3 dir = target.position - transform.position + add;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
    private void Damage(Transform enemy)
    {
        Monster monster = enemy.GetComponent<Monster>();

        if(monster != null)
                monster.TakeDamage(damageAmount);
    }
    private void HitTarget() 
    {
        GameObject effectIns = Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        Damage(target);

        Debug.Log("Hit");
        Destroy(gameObject);
    }
}
