using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ancient : Structures
{
    // Start is called before the first frame update

    public override void Death(Health objectHealth = null)
    {
        // base.Death(objectHealth);
        Events.OnGameOver.Invoke();
    }
}
