using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    BuildManager buildManager;
    EnemySpawner spawner;
    NavMeshAgent agent;
    public Image healthBar;
    private Transform location;
    //ingame points
    readonly float maxHealth = 100f;
    readonly int currencyDrop = 60;
    private float currentHealth;

    void Start()
    {
        buildManager = FindObjectOfType<BuildManager>();
        spawner = FindObjectOfType<EnemySpawner>();
        agent = GetComponent<NavMeshAgent>();
        location = GameObject.FindGameObjectWithTag("EndPoint").transform;
        currentHealth = maxHealth;
    }
    void Update()
    {
        //send monster to the end point
        agent.destination = location.position;
    }
    public void TakeDamage(float damage)
    {
        //substract damage from current health
        currentHealth -= damage;
        float percentOfHealth = currentHealth / maxHealth;
        //refresh health bar
        healthBar.fillAmount = percentOfHealth;
        //if health drops below 0 the monster dies
        if(currentHealth <= 0)
            Die();
        
    }
    private void Die()
    {
        //destroy itself
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        //add currency to player after death
        int newCur = PlayerStats.currency += currencyDrop;
        //reset currency counter
        buildManager.SetCurrencyText(newCur.ToString());
        //add +1 to the dead counter
        spawner.AddToDeadCounter();
    }
}
