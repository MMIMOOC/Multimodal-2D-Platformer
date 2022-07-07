using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using System.Collections;
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
    [SerializeField] private int availableBullets;
    [SerializeField] private Transform ammoText;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private KeywordRecognizer m_Recognizer;
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
 
    private void UpdateAmmoText() {
        if(availableBullets == 0) {
            ammoText.GetComponent<Text>().color = Color.red;
            ammoText.GetComponent<Text>().text = "Reloading needed. \r\nAvailable fireballs: " + availableBullets.ToString() + " / 10";
        } else {
            ammoText.GetComponent<Text>().text = "Bullets left: " + availableBullets.ToString() + " / 10";
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        ammoText.GetComponent<Text>().text = "Bullets left: 10 / 10";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && availableBullets < 10)
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.V) && cooldownTimer > attackCooldown 
        && playerMovement.canAttack() && availableBullets > 0)
        {
            Attack();
            UpdateAmmoText();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        availableBullets -= 1;
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
        ammoText.GetComponent<Text>().color = Color.blue;
        ammoText.GetComponent<Text>().text = "Reloading done. \r\nAvailable fireballs: " + availableBullets.ToString() + " / 10";
    }


    void Start()
    {
        keywordActions.Add("shoot", Attack);
        m_Recognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        m_Recognizer.OnPhraseRecognized += OnKeywordsRecognized;
        m_Recognizer.Start();
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("keyword: " + args.text);
        
        if(availableBullets > 0){
            keywordActions[args.text].Invoke();
            UpdateAmmoText();
        }
    }


}