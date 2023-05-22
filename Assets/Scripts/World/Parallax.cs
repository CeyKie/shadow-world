using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private float parallaxEffect;

    private float length;
    private float startpos;

    private void Awake()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        var temp = mainCamera.transform.position.x * (1 - parallaxEffect);
        var distance = mainCamera.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
