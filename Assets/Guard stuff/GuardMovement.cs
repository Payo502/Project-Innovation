using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    public GuardManager Manager;
    public GameObject Player;
    float stamina;
    public bool canDo;
    public Rigidbody guardRigidbody;

    private void Start()
    {
        stamina = 4000;
        canDo = true;
        guardRigidbody = this.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (Manager != null)
        {
            if (Manager.alertLevel > 50 && stamina > 0)
            {
                this.transform.LookAt(Player.transform.position);
                StartCoroutine(MoveTowards());
            }
        }
    }

    IEnumerator MoveTowards()
    {
        if (Manager != null)
        {
            yield return new WaitForSeconds(2);
            if (Manager.alertStage != AlertStage.Peaceful)
            {
                //guardRigidbody.AddForce(Player.transform.position, ForceMode.Force + 1);
                stamina--;
                if (stamina <= 0)
                {
                    canDo = false;
                    yield return new WaitForSeconds(6);
                    stamina = 4000;
                    canDo = true;
                }
            }
        }
    }
}
