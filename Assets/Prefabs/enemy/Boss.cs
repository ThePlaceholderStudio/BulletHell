using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected override void Init()
    {
        base.Init();
        EnemyName = "Basic Boss";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
