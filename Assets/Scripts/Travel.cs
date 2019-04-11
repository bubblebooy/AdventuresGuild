using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : MonoBehaviour
{
    public GameObject targetLocation;
    public float speed = 1;
    private float step;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        step = 2 * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetLocation.transform.position, step);
        if (Vector3.Distance(targetLocation.transform.position, transform.position) < 0.05f)
        {
            Destroy(this);
        }
    }
}
