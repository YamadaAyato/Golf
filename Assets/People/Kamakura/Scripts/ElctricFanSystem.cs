using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public Transform _thisTrasform;
    private Rigidbody2D _targetRb;

    [Header("  扇風機の強さ  ")]
    [SerializeField]
    private float _force = 10f;

    [Header("  対象のタグ  ")]
    [SerializeField]
    private string _targetTag = "Player";


    private void Start()
    {
        _thisTrasform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_targetTag))
        {
            _targetRb = collision.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_targetRb == null) return;
        _targetRb.AddForce(_thisTrasform.up * _force, ForceMode2D.Force);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _targetRb = null;
    }
}
