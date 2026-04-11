using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTestController : MonoBehaviour
{
    private CircleCollider2D _collider;
    private ContactFilter2D filter = new ContactFilter2D();
    private List<Collider2D> results = new List<Collider2D>();

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        filter.useTriggers = false;

        int hitCount = _collider.OverlapCollider(filter, results);
        print(hitCount);
    }
}
