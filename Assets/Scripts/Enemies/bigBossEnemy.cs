using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBossEnemy : meleeEnemyController
{
    [SerializeField]
    DialogObjects[] dialogs;

    [SerializeField]
    GameObject loot;

    public override void TakeDamage(Vector3 position)
    {
        DialogUIController.instance.showTempDialog(dialogs[Random.Range(0, dialogs.Length)]);
    }

    public override void TakeRangeDamage()
    {
        DialogUIController.instance.showTempDialog(dialogs[Random.Range(0, dialogs.Length)]);
    }

    protected override void spawnObjectOnDeath()
    {
        Vector3 newPostion = new Vector3(transform.position.x, 20, transform.position.z);
        Instantiate(loot, newPostion, transform.rotation);
        base.spawnObjectOnDeath();
    }


    public void realDamage()
    {
        base.TakeDamage(Vector3.zero);
    }
}
