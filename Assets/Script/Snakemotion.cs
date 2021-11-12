using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakemotion : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> segments;
    public Transform SegmentPrefab;
    public int initialSize = 4;
    public void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
        for (int i = 1; i < initialSize; i++)
        {
            segments.Add(Instantiate(this.SegmentPrefab));
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }
    private void FixedUpdate()
    {
        for(int i=segments.Count-1;i>0;i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y, 
            0.0f);
    }
    public void Grow()
    {
        Transform segment = Instantiate(this.SegmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }
    private void ResetState()
    {
        for(int i=1;i<segments.Count;i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);
        for (int i = 1; i < initialSize; i++)
        {
            segments.Add(Instantiate(this.SegmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.CompareTag("Food"))
        { Grow(); }
        if (other.CompareTag("Obstacle"))
        {
            ResetState();
        }


    }

}
