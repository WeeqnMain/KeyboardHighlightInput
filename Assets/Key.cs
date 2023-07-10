using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRange;
    [SerializeField] private float _timeToExplode;
    private float _explosionTimer;

    private Image _highlight;
    [SerializeField] private KeyCode m_keyCode;

    private void Awake()
    {
        _highlight = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        _explosionTimer = _timeToExplode;

        if (m_keyCode == KeyCode.None)
        {
            Enum.TryParse(gameObject.name, true, out m_keyCode);
            if (m_keyCode == KeyCode.None)
                throw new Exception("can't identify button: " + gameObject.name);
        }
        
    }

    private void Update()
    {
        _highlight.fillAmount = 1 - _explosionTimer / _timeToExplode;
        if (Input.GetKey(m_keyCode))
        {
            _explosionTimer -= Time.deltaTime;
        }
        if (Input.GetKeyUp(m_keyCode))
        {
            _explosionTimer = _timeToExplode;
        }

        if (_explosionTimer <= 0)
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRange);
        foreach (Collider2D collider in colliders)
        {
            Vector2 direction = collider.transform.position - transform.position;
            Rigidbody2D rigidbody2D = collider.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.AddForce(direction * _explosionForce);
            }
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}
