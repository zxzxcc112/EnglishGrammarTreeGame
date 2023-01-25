using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller =  collision.GetComponent<PlayerController>();

        if(controller != null)
        {
            if(controller.CurrentHealth < controller.MaxHealth)
            {
                controller.ChangeHealth(1);
                controller.PlaySound(clip);
                Destroy(gameObject);
            }
        }
    }
}
