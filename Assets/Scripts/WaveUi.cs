using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUi : MonoBehaviour
{
    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnim;


    [SerializeField]
    Text wavecoutntDowntext;

    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;
    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null)
        {
            Debug.LogError("no spawner");
            this.enabled = false;
        }
        if (waveAnim == null)
        {
            Debug.LogError("no waveanim");
            this.enabled = false;
        }
        if (wavecoutntDowntext == null)
        {
            Debug.LogError("no countdowntext");
            this.enabled = false;
        }
        if (waveCountText == null)
        {
            Debug.LogError("no wavecounttext");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.SpawnState.counting:
                UpdateCountDownUI();
                break;
            case WaveSpawner.SpawnState.spawning:
                UpdateSpwaningUI();
                break;
        }
        previousState = spawner.State;
    }
    void UpdateCountDownUI()
    {
        if (previousState != WaveSpawner.SpawnState.counting)
        {
            waveAnim.SetBool("WaveIncoming", false);
            waveAnim.SetBool("WaveCoundown",true);

            Debug.Log("counting");
        }
        wavecoutntDowntext.text = ((int)spawner.WaveCountDown).ToString();
    }
    void UpdateSpwaningUI()
    {
       
        if (previousState != WaveSpawner.SpawnState.spawning)
        {

            waveAnim.SetBool("WaveCoundown", false);
            waveAnim.SetBool("WaveIncoming", true);

            waveCountText.text = spawner.NextWave.ToString();
            Debug.Log("spawining");
        }

    }
}
