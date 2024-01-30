using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.AI;
public class SkeletonWeak : MonoBehaviour, IKickable, ISweepable, IDamageable
{
    private enum State
    {
        Idle,
        Chase,
        Attack,
        Ragdoll,
        Standing
    }
    [SerializeField] private EventReference OnAggroRef;
    [SerializeField] private EventReference OnAttackRef;
    [SerializeField] private EventReference OnDeathRef;

    [SerializeField] private GameObject gibs;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform hipTransform;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float attackDelay;
    [SerializeField] private float sightLostThreshold;
    [SerializeField] private float attackValue;

    private float attackTimer = 0;
    private float sightTimer = 0;
    private float ragDollTimer = 0;

    private State currentState;
    
    private NavMeshAgent navAgent;
    private CapsuleCollider col;
    private Transform target;
    private Transform playerTransform;

    private float health = 30;

    
    // Start is called before the first frame update
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        col = GetComponent<CapsuleCollider>();
        transform.Rotate(Vector3.up, Random.Range(0,360));
    }

    void Start()
    {
        //Init ragdoll colliders
        rb.isKinematic = true;
        foreach (Rigidbody rig in GetComponentsInChildren<Rigidbody>())
        {
            rig.isKinematic = true;
        }
        foreach (Collider cCol in GetComponentsInChildren<Collider>())
        {
            if (cCol.gameObject != gameObject)
            {
                cCol.enabled = false;
            }
        }
        anim.enabled = true;
        navAgent.enabled = true;
        NavMesh.SamplePosition(transform.position, out NavMeshHit navHit, 0.1f, NavMesh.AllAreas);
        navAgent.Warp(navHit.position);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SetState(State.Idle);
        
    }
    void Update()
    {
        UpdateState();
    }
    private void UpdateState()
    {
        switch(currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Standing:
                Standing();
                break;
        }
    }
    private void SetState(State state)
    {
        switch(currentState)
        {
            case State.Idle:
                IdleExit();
                break;
            case State.Chase:
                ChaseExit();
                break;
            case State.Attack:
                AttackExit();
                break;
            case State.Standing:
                StandingExit();
                break;
        }
        switch(state)
        {
            case State.Idle:
                currentState = State.Idle;
                IdleEnter();
                break;
            case State.Chase:
                currentState = State.Chase;
                ChaseEnter();
                break;
            case State.Attack:
                currentState = State.Attack;
                AttackEnter();
                break;
            case State.Standing:
                currentState = State.Standing;
                StandingEnter();
                break;
        }
    }
    private void IdleEnter()
    {

    }
    private void Idle()
    {
        //Idle

        //If target spotted, chase!
        if (Physics.Raycast(GetHeadPosition(), playerTransform.position - Vector3.up - transform.position, out RaycastHit hit, 20f))
        {
            if (hit.collider.tag == "Player")
            {
                RuntimeManager.PlayOneShotAttached(OnAggroRef, gameObject);
                target = hit.collider.transform;
                SetState(State.Chase);
            }
        }
    }
    private void IdleExit()
    {

    }
    private void ChaseEnter()
    {
        //Animation
        anim.SetBool("isRunning", true);

        //Set navmesh agent target to target
        NavMesh.SamplePosition(target.position, out NavMeshHit navHit, 5f, NavMesh.AllAreas);
        navAgent.SetDestination(navHit.position);
    }
    private void Chase()
    {
        //If target exists and in sight timer, run towards target
        if (target != null)
        {
            //If reached target, attack
            if (Vector3.Distance(transform.position, navAgent.destination) < 1f)
            {
                SetState(State.Attack);
            }
            if (Vector3.Distance(navAgent.destination, target.position) > 1f)
            {
                //Resample destination
                NavMesh.SamplePosition(target.position, out NavMeshHit navHit, 5f, NavMesh.AllAreas);
                navAgent.SetDestination(navHit.position);
            }

            //If has sight of target
            if (Physics.Raycast(GetHeadPosition(), target.position - Vector3.up - transform.position, out RaycastHit hit, 20f))
            {
                if (hit.collider.tag != "Player")
                {
                    sightTimer += Time.deltaTime;
                }
            }else
            {
                sightTimer += Time.deltaTime;
            }
        }
        //If lost sight, idle
        if (sightTimer >= sightLostThreshold)
        {
            target = null;
            sightTimer = 0;
            SetState(State.Idle);
        }
        
    }
    private void ChaseExit()
    {
        anim.SetBool("isRunning", false);
        if (navAgent.enabled)
        {
            navAgent.ResetPath();
        }
    }
    private void AttackEnter()
    {
        //ATTACK
        if (attackTimer > attackDelay || attackTimer == 0)
        {
            anim.SetTrigger("Attack");
            StartCoroutine(IAttack());
            RuntimeManager.PlayOneShotAttached(OnAttackRef, gameObject);
            attackTimer = 0.01f;
        }
    }
    private IEnumerator IAttack()
    {
        yield return new WaitForSeconds(0.5f);
        if (target != null && Vector3.Distance(transform.position, target.position) <= 1.5f)
        {
            target.GetComponent<IDamageable>()?.TakeDamage(attackValue);
        }
    }
    private void Attack()
    {
        //If in attack range
        if (target != null && Vector3.Distance(transform.position, target.position) <= 1f)
        {
            attackTimer += Time.deltaTime;
        }else
        {
            //If target exists, chase
            if (target != null)
            {
                SetState(State.Chase);
            }else //If target doesn't exist, idle
            {
                attackTimer = 0.01f;
                SetState(State.Idle);
            }
        }
        //Attack reset
        if (attackTimer > attackDelay)
        {
            SetState(State.Attack);
        }
    }
    private void AttackExit()
    {

    }
    private void StandingEnter()
    {
        anim.SetTrigger("Standing");
    }
    private void Standing()
    {
        //If stood up, if target is null idle. If target exists, chase
    }
    private void StandingExit()
    {

    }
    public void Kick(float damage, Vector3 direction)
    {
        OnDeath();
    }
    public void Sweep(Vector3 direction)
    {
        OnDeath();
    }
    public void TakeDamage(float damage)
    {
        OnDeath();
    }
    private void OnDeath()
    {
        RuntimeManager.PlayOneShotAttached(OnDeathRef, gameObject);
        GameManager.instance.dungeonData.KillEnemy(DungeonStats.EnemyScore.weak);
        GameObject objGib = Instantiate(gibs);
        objGib.transform.position = hipTransform.position;
        objGib.transform.rotation = hipTransform.rotation;
        Destroy(gameObject);
    }
    private Vector3 GetHeadPosition()
    {
        return transform.position + (Vector3.up * 1.7f) + (transform.forward * 0.2f);
    }
}
