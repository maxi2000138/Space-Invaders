using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
public class StarsMover : MonoBehaviour
{
    [SerializeField] private float _starsSpeed = 1f;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {   
        _meshRenderer.material.mainTextureOffset += Vector2.down * _starsSpeed;
    }
}
