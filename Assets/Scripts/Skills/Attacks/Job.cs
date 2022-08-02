using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Character Job", menuName = "New Character Job")]
public class Job : ScriptableObject
{
    // Start is called before the first frame update

    public List<AbilityBase> skills;
}
