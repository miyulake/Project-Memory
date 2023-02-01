using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Platform : MonoBehaviour
{
    [SerializeField] private Ability ability;
    [SerializeField] private TextMeshPro countdownText;
    [SerializeField] private Transform textObject;
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float time;
    private bool isMoving;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ability = player.GetComponent<Ability>();
    }

    private void Start()
    {
        time = ability.abilityDuration;
    }

    private void Update()
    {
        speed = ability.abilitySpeed;

        countdownText.text = "" + Mathf.CeilToInt(time);

        TextFaceTarget();

        time -= Time.deltaTime;

        if (isMoving)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        
        if (time <= 0)
        {
            player.transform.parent = null;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isMoving = true;
            col.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.parent = null;
        }
    }

    private void TextFaceTarget()
    {
        if (player != null)
        {
            var offsetToTarget = player.transform.position - textObject.position;
            offsetToTarget.y = 0;
            textObject.rotation = Quaternion.LookRotation(offsetToTarget);
        }
    }
}
