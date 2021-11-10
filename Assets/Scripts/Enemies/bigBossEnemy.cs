using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBossEnemy : meleeEnemyController
{
    [SerializeField]
    DialogObjects[] dialogs;

    public override void TakeDamage(Vector3 position)
    {
        DialogUIController.instance.showTempDialog(dialogs[Random.Range(0, dialogs.Length)]);
    }

    public override void TakeRangeDamage()
    {
        DialogUIController.instance.showTempDialog(dialogs[Random.Range(0, dialogs.Length)]);
    }

    public void realDamage()
    {
        base.TakeDamage(Vector3.zero);
    }
}
