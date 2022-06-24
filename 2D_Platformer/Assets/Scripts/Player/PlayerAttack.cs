using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Collections.Generic;
using System.Linq;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private string[] m_Keywords;

    private int availableBullets = 10;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private KeywordRecognizer m_Recognizer;
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && availableBullets < 10)
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.V) && cooldownTimer > attackCooldown 
        && playerMovement.canAttack() && availableBullets > 0)
            Attack();
            availableBullets -= 1;

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        Debug.logger.Log("Fireballs left:" + availableBullets);
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Reload()
    {
        availableBullets = 10;
    }


    void Start()
    {
        keywordActions.Add("fire", Attack);
        m_Recognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        m_Recognizer.OnPhraseRecognized += OnKeywordsRecognized;
        m_Recognizer.Start();
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }


}