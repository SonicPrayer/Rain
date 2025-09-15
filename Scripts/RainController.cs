using UnityEngine;
public class RainController : MonoBehaviour
{
    public ParticleSystem rainSystem;
    public Transform followTarget;
    public float offsetHeight = 15f;
    public LayerMask rainOccluderMask;
    public float checkInterval = 0.2f;
    public float rayHeight = 20f;

    private float timer;

    void Start()
    {
        // Если не назначен игрок — ищем камеру
        if (followTarget == null && Camera.main != null)
        {
            followTarget = Camera.main.transform;
        }
    }

    void Update()
    {
        if (followTarget == null) return;

        transform.position = followTarget.position + Vector3.up * offsetHeight;

        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            CheckRoofAbove();
        }
    }

    void CheckRoofAbove()
    {
        Vector3 origin = followTarget.position + Vector3.up * 0.1f;
        Ray ray = new Ray(origin, Vector3.up);
        bool blocked = Physics.Raycast(ray, rayHeight, rainOccluderMask);

        var emission = rainSystem.emission;
        emission.enabled = !blocked;
    }
}

