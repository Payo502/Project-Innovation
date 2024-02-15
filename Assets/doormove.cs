using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doormove : MonoBehaviour
{
    [SerializeField] bool up;
    [SerializeField] float speed;
    [SerializeField] float upwardsHeight;
    private float lerp;
    private Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            lerp += speed * Time.timeScale;
        }
        else
        {
            lerp -= speed * Time.timeScale;
        }
        lerp = Mathf.Clamp(lerp, 0, 1);

        transform.position = start + new Vector3(0f, upwardsHeight * lerp, 0f);
    }
}
