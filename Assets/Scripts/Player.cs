using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Reference")]
    public GameManager Manager;
    public Material normalMat;
    public Material phaseMat;

    [Header("GamePlay")]
    public float Bounds = 3f;
    public float StrafeSpeed = 4f;
    public float PhaseCooldown = 2f;

    Renderer mesh;
    Collider collision;
    bool canPhase = true;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        collision = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * StrafeSpeed;
        Vector3 position = transform.position;
        position.x += xMove;
        position.x = Mathf.Clamp(position.x, -Bounds, Bounds);
        transform.position = position;

        if(Input.GetButtonDown("Jump") && canPhase) {
            canPhase = false;
            mesh.material = phaseMat;
            collision.enabled = false;
            Invoke("PhaseIn", PhaseCooldown);
        }
    }

    void PhaseIn()
    {
        canPhase = true;
        mesh.material = normalMat;
        collision.enabled = true;
    }

}
