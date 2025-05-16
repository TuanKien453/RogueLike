using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;

public class ParticleLightAttacher : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject light2DPrefab;

    private ParticleSystem.Particle[] particles;
    private Dictionary<uint, GameObject> particleLights = new Dictionary<uint, GameObject>();

    void LateUpdate()
    {
        if (particleSystem == null || light2DPrefab == null) return;

        int maxParticles = particleSystem.main.maxParticles;
        if (particles == null || particles.Length < maxParticles)
            particles = new ParticleSystem.Particle[maxParticles];

        int count = particleSystem.GetParticles(particles);

        var activeIDs = new HashSet<uint>();

        for (int i = 0; i < count; i++)
        {
            uint randomSeed = particles[i].randomSeed;
            activeIDs.Add(randomSeed);

            if (!particleLights.ContainsKey(randomSeed))
            {
                Vector3 worldPos = particleSystem.transform.TransformPoint(particles[i].position);
                GameObject lightInstance = Instantiate(light2DPrefab, worldPos, Quaternion.identity, transform);
                particleLights[randomSeed] = lightInstance;
            }
            else
            {
                particleLights[randomSeed].transform.position = particleSystem.transform.TransformPoint(particles[i].position);
            }
        }

        List<uint> toRemove = new List<uint>();
        foreach (var kvp in particleLights)
        {
            if (!activeIDs.Contains(kvp.Key))
            {
                Destroy(kvp.Value);
                toRemove.Add(kvp.Key);
            }
        }
        foreach (uint id in toRemove)
            particleLights.Remove(id);
    }
}
