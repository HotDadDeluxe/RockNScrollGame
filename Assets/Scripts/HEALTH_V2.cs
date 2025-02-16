using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEALTH_V2 : MonoBehaviour
{
    public int CURhealth;
    public int maxHealth = 3;
    // Start is called before the first frame update
    void Start()
    {
        CURhealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int amount)
    {
        CURhealth -= amount;
        if(CURhealth <=0)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        
    }
}
