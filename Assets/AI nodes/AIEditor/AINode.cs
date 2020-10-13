using Attributes;
using System;
using System.Collections.Generic;

public enum AINodeType { Main, Warp, If, Do }
public enum AIWhos { Target, My/*, Player*/}
public enum AIWhat { HP, Energy, Distance, Speed }
public enum AICompare { More, MoreOrEqual, Equals, LessOrEqual, Less }
public enum AIValue { Normal, Variable }
public enum AITargetType { Friendly, Enemy, Any}

[Serializable]
public class AINode
{
    public AINodeType type;
    public List<AINode> outputs = new List<AINode>();
    public int executionOrder = 0;

    [ConditionalField("type", AINodeType.If)] public AIIfPart ifpart;
    [ConditionalField("type", AINodeType.Do)] public AIDoPart dopart;

}
[Serializable]
public class AIIfPart
{
    public AIWhos whos;
    public AIWhat what;
    public AICompare compare;
    public AIValue aivalue;
    [ConditionalField("aivalue", AIValue.Normal)] public float value;
    [ConditionalField("aivalue", AIValue.Variable)] public int id;

}
[Serializable]
public class AIDoPart
{
    public AIDos dos;
    [ConditionalField("dos", AIDos.FindTarget)] public AITargetingDos aitdos;
    [ConditionalField("dos", AIDos.FindTarget)] public AITargetType aitt;

}