using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void CreateParticle(GameObject particle, Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        Instantiate(particle, transform.position, rotation);
    }
}
