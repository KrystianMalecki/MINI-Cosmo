using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New AIBrain", menuName = "Custom/AIBrain")]

public class AIBrain : ScriptableObject
{
    [SerializeField]
    public AINode mainNode;
}