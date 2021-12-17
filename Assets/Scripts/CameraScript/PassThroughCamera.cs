using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PassThroughCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,200)]
    public float speed = 50;

    [Range(0, 50)] 
    public float speedRotation = 9f;

    [Range(0, 4)]
    public float margin = 1;

    private GameObject player;
    public void JumpToPlayer(GameObject player) {
        this.player = player;
    }

    void LateUpdate() {
        if (player == null) {
            return;
        }

        //wy³¹cza siê 0.5 przed celem
        if (Vector3.Distance(transform.position, player.transform.position) > 0.5f) {
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }

        Transform cel = player.transform;
       

        //pozycja ustawienia kamery podczas podsumowania!
        //var target = cel.position + cel.forward * 3 + cel.up*2 +cel.up*-3+ cel.right * -1;
        var target = cel.position + cel.forward * 3 + (cel.up / 20) + cel.right * -1;
        //prêdkoœæ
        float mnoznik = Vector3.Distance(transform.position, target);
        float step = speed * mnoznik * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
