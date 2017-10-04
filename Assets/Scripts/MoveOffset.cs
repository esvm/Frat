using UnityEngine;
using System.Collections;

public class MoveOffset : MonoBehaviour {


    private Renderer CurretMaterial;
    Vector2 offset = new Vector2();
    public static float Speed = 2f;

    //Use this for initialization
    void Start () {
        CurretMaterial = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset = CurretMaterial.material.mainTextureOffset;

        offset.x += Time.deltaTime / Speed;

        CurretMaterial.material.mainTextureOffset = offset;
    }
}
