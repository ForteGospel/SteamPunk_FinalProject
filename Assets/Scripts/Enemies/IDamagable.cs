using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(Vector3 position);

    void TakeRangeDamage();
}
