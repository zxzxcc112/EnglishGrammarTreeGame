using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        RubyPlayerController controller = collision.GetComponent<RubyPlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
