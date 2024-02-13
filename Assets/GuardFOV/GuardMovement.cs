using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    public GuardManager Manager;
    public GameObject Player;
    private CharacterController Controller;
    float stamina;
    public bool canDo;

    private void Start()
    {
        Controller = this.GetComponent<CharacterController>();
        stamina = 4000;
        canDo = true;
    }
    void Update()
    {
        if (Manager != null)
        {
            if (Manager.alertLevel > 50 && stamina > 0)
            {
                this.transform.LookAt(Player.transform.position);
                Vector3 moving = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);
                StartCoroutine(MoveTowards(moving));
            }
        }
    }

    IEnumerator MoveTowards(Vector3 moving)
    {
        if (Manager != null)
        {
            yield return new WaitForSeconds(2);
            if (Manager.alertStage != AlertStage.Peaceful)
            {
                stamina--;
                this.transform.position = Vector3.MoveTowards(this.transform.position, moving, 1f);
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
