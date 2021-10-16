using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffsetController : MonoBehaviour
{
    [SerializeField]
    Vector2 speed = new Vector2(1, 0);

    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

        material.mainTextureOffset += speed * Time.deltaTime;
    }
}
