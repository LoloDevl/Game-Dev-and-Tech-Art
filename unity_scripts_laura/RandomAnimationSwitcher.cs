using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomAnimationSwitcher : MonoBehaviour
{
    private Animator animator;

    public float checkInterval = 1.0f; // How often we set a new random value

    private void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("SetRandomValue", 0f, checkInterval);
    }

    void SetRandomValue()
    {
        float randomVal = Random.value; // Random.value returns a float between 0 and 1.
        animator.SetFloat("RandomValue", randomVal);
    }
}
