using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monster : MonoBehaviour
{
    public float monsterhealth = 100f; 
    public float kovalamamesafe = 15f;
    public float saldýrmamesafesi = 2f;
    private float mesafe;
    private bool monsterdeath = false;

    private Animator monsteranim;
    private GameObject targetplayer;
    private NavMeshAgent monsternavmesh;
    AudioSource seskaynak;
    public AudioClip monsterses;

    void Start()
    {
        monsteranim = GetComponent<Animator>();
        monsternavmesh = GetComponent<NavMeshAgent>();
        targetplayer = GameObject.FindWithTag("Player");
        seskaynak = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (monsterdeath)
        {
            if (!monsteranim.GetBool("dead"))
            {
                
                monsteranim.SetBool("dead", true);
                seskaynak.Stop();
                StartCoroutine(yoket());
            }
            return;
        }

        if (monsterhealth <= 0)
        {
            monsterdeath = true;
            monsternavmesh.isStopped = true;
            return;
        }

        mesafe = Vector3.Distance(transform.position, targetplayer.transform.position);

        if (mesafe < saldýrmamesafesi)
        {
            monsternavmesh.isStopped = true;
            monsteranim.SetBool("walk", false);
            monsteranim.SetBool("attack", true);
            transform.LookAt(targetplayer.transform.position);
        }
        else if (mesafe < kovalamamesafe)
{
    monsternavmesh.isStopped = false;
    monsternavmesh.SetDestination(targetplayer.transform.position);

    
    float speed = monsternavmesh.velocity.magnitude;
    monsteranim.SetFloat("Speed", speed);

    
    monsteranim.SetBool("walk", speed > 0.1f);
    monsteranim.SetBool("attack", false);
}

        else
        {
            monsternavmesh.isStopped = true;
            monsteranim.SetBool("walk", false);
            monsteranim.SetBool("attack", false);
        }
    }

    public void hasarver()
    {
        if (targetplayer != null)
        {
            
            karakterkontrol kontrol = targetplayer.GetComponent<karakterkontrol>();
            if (kontrol != null)
            {
                kontrol.hasaral();
            }
        }
    }
    public void hasarvermesesi()
    {
        seskaynak.PlayOneShot(monsterses);
    }
    IEnumerator yoket()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public void hasar()
    {
        float zarar = Random.Range(20, 30); 
        monsterhealth -= zarar; 
        Debug.Log($"{name} hasar aldý: {zarar}. Kalan saðlýk: {monsterhealth}");
    }
}
