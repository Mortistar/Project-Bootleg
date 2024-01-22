using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Object Refs")]
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerUIHandler playerUI;
    [SerializeField] private bool debugAttack;

    private PlayerStats stats;
    private bool isDead = false;
    private bool isAttacking = false;
    
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
        InputManager.instance.controls.Gameplay.Kick.performed += Kick;
        InputManager.instance.controls.Gameplay.Sweep.performed += Sweep;
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
            }
        }
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
        isDead = true;
        yield return new WaitForSeconds(3f);
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
        Debug.Log("Hii..");
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("YAH");
        if (Physics.BoxCast(cam.transform.position, Vector3.one * 0.5f, cam.transform.forward, out RaycastHit hit, Quaternion.identity, 1f, 1 << 6))
        {
            IKickable kickScript = hit.collider.GetComponent<IKickable>();
            if (kickScript != null)
            {
                Vector3 kickDirection = (hit.collider.transform.position - transform.position).normalized;
                kickScript.Kick(stats.attack, kickDirection);
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
        
        Debug.Log("Ooh..");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("YEH");
        if (Physics.BoxCast(cam.transform.position, Vector3.one * 0.5f, cam.transform.forward, out RaycastHit hit, Quaternion.identity, 2f, 1 << 6))
        {
        ISweepable sweepScript = hit.collider.GetComponent<ISweepable>();
            if (sweepScript != null)
            {
                Vector3 sweepDirection = (hit.collider.transform.position - transform.position);
                sweepDirection = new Vector3(sweepDirection.x, 0, sweepDirection.z).normalized;
                sweepScript.Sweep(sweepDirection);
                isAttacking = false;
                //Slow motion
                Time.timeScale = 0.5f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                yield return new WaitForSeconds(3 * Time.timeScale);
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
                yield return null;
            }
        }
    }
    void OnDisable()
    {
        InputManager.instance.controls.Gameplay.Kick.performed -= Kick;
        InputManager.instance.controls.Gameplay.Sweep.performed -= Sweep;
    }
}
