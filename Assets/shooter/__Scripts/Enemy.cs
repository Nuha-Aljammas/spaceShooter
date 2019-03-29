using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f; // The speed in m/s
    public float fireRate = 0.3f; // Seconds/shot 
    public float health = 10;
    public int score = 10; // Points earned for destroying this
    public float showDamageDuration = 0.1f; // # seconds to show damage 
    public float powerUpDropChance = 1f;


    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;// All the Materials of this & its children
    public bool showingDamage = false;
    public float damageDoneTime; // Time to stop showing damage
    public bool notifiedOfDestruction = false; // Will be used later

    protected BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();

        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
        Debug.Log(this.score);

    }


    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
    protected virtual void Update()
    {
        Move();
        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }
        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);

        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectilePlayer":
                projectile p = otherGO.GetComponent<projectile>();
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO);
                    break;
                }

                //hurt the enemy
                //get the damage amount from the main weap_dect
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                ShowDamage();
                if (health <= 0)
                {
                    //tell the main singelton that that the ship has been destroyed 
                    if (!notifiedOfDestruction)
                    {
                        Main.S.shipDestroyed(this);
                        Main.S.score += this.score;
                        audioManager.instance.distroy.Play();
                        
                        
                    }
                    notifiedOfDestruction = true;
                    Destroy(this.gameObject);
                }

                Destroy(otherGO);
                break;
            default:
                print("Enemy hit by non-projectilePlayer: " + otherGO.name);
                break;
        }
    }

    void OnDestroy()
    {
        Main.S.enemiesOnScene--;
    }
    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }


    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage =false;

    }
}