using System.Collections.Generic;
using UnityEngine;
//Fonte: https://forum.unity.com/threads/lwrp-using-2d-lights-in-a-particle-system-emitter.718847/
//Este código é uma versão modificada do original.
//com partes daqui: https://answers.unity.com/questions/693044/play-sound-on-particle-emit-sub-emitter.html?_ga=2.157928186.1767920396.1591293573-1075136355.1582741123
//Aqui só permitimos 1 luz e controlamos o tempo de ativação dela de acordo com a duração do sistema de partículas.

//Motivo pelo qual as vezes a luz não funciona? Não sei.
[RequireComponent(typeof(ParticleSystem))]
public class AttachGameObjectsToParticles : MonoBehaviour
{
    public GameObject m_Prefab;

    private ParticleSystem m_ParticleSystem;
    private GameObject shine;
    private int number = 0;

    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();

        shine = Instantiate(m_Prefab, m_ParticleSystem.transform);
        shine.SetActive(false);
    }

    void Update()
    {

        if (m_ParticleSystem.emission.enabled) //Se está emitindo
        {
            if (m_ParticleSystem.particleCount < number)
            { //particle has died

            }
            else if (m_ParticleSystem.particleCount > number)
            { //particle has been born
                shine.SetActive(true);
                Invoke("TimeToLiveLight", m_ParticleSystem.main.duration / 3);
            }
            number = m_ParticleSystem.particleCount;
        }
        else
        {
            number = 0; 
        }

    }

    void TimeToLiveLight()
    {
        shine.SetActive(false);
    }
}
