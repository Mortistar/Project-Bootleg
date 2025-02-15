using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Object Refs")]
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerUIHandler playerUI;
    [SerializeField] private Animator anim;
    [SerializeField] private EventReference hurtRef;
    [SerializeField] private EventReference deathRef;
    [SerializeField] private EventReference sweepRef;
    [SerializeField] private EventReference kickRef;

    private PlayerStats stats;
    private bool isDead = false;
    private bool isAttacking = false;

    private List<Key> keys;
    
    void Awake()
    {
        stats = new PlayerStats();
    }
    void Start()
    {
        if (GameManager.instance.playerData != null)
        {
            stats = GameManager.instance.playerData;
        }
        playerUI.INIT(stats);
        keys = new List<Key>();
        InputManager.instance.controls.Gameplay.Kick.performed += Kick;
        InputManager.instance.controls.Gameplay.Sweep.performed += Sweep;
    }
    public bool TryUnlock(Key.KeyType keyType)
    {
        foreach(Key key in keys)
        {
            if (key.keyType == keyType)
            {
                keys.Remove(key);
                playerUI.RemoveKey(keyType);
                return true;
            }
        }
        return false;
    }
    public void AddKey(Key.KeyType keyType)
    {
        Key newKey = new Key(keyType);
        keys.Add(newKey);
        //Update UI
        playerUI.AddKey(keyType);
    }

    public void TakeDamage(float value)
    {
        stats.LoseHealth(value);
        playerUI.UpdateHealth(stats.health);
        if (stats.health == 0)
        {
            if (!isDead)
            {
                Death();
                return;
            }
        }
        RuntimeManager.PlayOneShot(hurtRef);
    }
    public bool GetHealable()
    {
        return stats.health < stats.healthMax;
    }
    public void RestoreHealth(float value)
    {
        stats.GainHealth(value);
        playerUI.UpdateHealth(stats.health);
    }
    private void CompleteLevel()
    {
        GameManager.instance.SetPlayerData(stats);
    }
    private void Death()
    {
        StartCoroutine(IDeath());
    }
    private IEnumerator IDeath()
    {
        //Player related death stuff
        RuntimeManager.PlayOneShot(deathRef);
        isDead = true;
        InputManager.instance.DisableControls();
        yield return new WaitForSeconds(2f);
        GameManager.instance.FailLevel();
    }
    private void Kick(InputAction.CallbackContext ctx)
    {
        if (!isAttacking)
        {
            StartCoroutine(IKick());
        }
    }
    private IEnumerator IKick()
    {
        anim.SetTrigger("isKicking");
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        if (Physics.BoxCast(transform.position + (Vector3.up * 0.9f), new Vector3(0.5f, 3f, 0.5f), transform.forward, out RaycastHit hit, Quaternion.identity, 1.5f, 1 << 6))
        {
            IKickable kickScript = hit.collider.GetComponent<IKickable>();
            if (kickScript != null)
            {
                Vector3 kickDirection = (hit.collider.transform.position - transform.position).normalized;
                kickScript.Kick(stats.attack, kickDirection);
                RuntimeManager.PlayOneShot(kickRef);
            }
        }
        isAttacking = false;
        yield return null;
    }
    private void Sweep(InputAction.CallbackContext ctx)
    {
        if (!isAttacking)
        {
            StartCoroutine(ISweep());
        }
    }
    private IEnumerator ISweep()
    {
        RuntimeManager.PlayOneShot(sweepRef);
        anim.SetTrigger("isSweeping");
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        
        RaycastHit[] hits = Physics.BoxCastAll(cam.transform.position, new Vector3(0.5f, 3f, 0.5f), transform.forward, Quaternion.identity, 1.5f, 1 << 6);
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                ISweepable sweepScript = hit.collider.GetComponent<ISweepable>();
                if (sweepScript != null)
                {
                    Vector3 sweepDirection = (hit.collider.transform.position - transform.position);
                    sweepDirection = new Vector3(sweepDirection.x, 0, sweepDirection.z).normalized;
                    sweepScript.Sweep(sweepDirection);
                    yield return null;
                }
            }
            //Slow motion
            RuntimeManager.StudioSystem.setParameterByName("TIMESCALE", 0);
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            yield return new WaitForSeconds(3 * Time.timeScale);
            RuntimeManager.StudioSystem.setParameterByName("TIMESCALE", 1);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            yield return null;
        }
    }
    void OnDisable()
    {
        InputManager.instance.controls.Gameplay.Kick.performed -= Kick;
        InputManager.instance.controls.Gameplay.Sweep.performed -= Sweep;
        RuntimeManager.StudioSystem.setParameterByName("TIMESCALE", 1);
    }
}
