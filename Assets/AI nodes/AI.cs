using System.Collections;
using System.Linq;
using UnityEngine;

public enum AIDos { GoTo, Flee, Fire, FindTarget, Wait }
public enum AITargetingDos { Closest, Furthest, Random }

public class AI : ScriptedEntity
{
    public AIBrain brain;
    public Entity Target;
    public Shooting shooter;
    public GameObject FindGameObjectTarget(AITargetingDos AITD, float radius, AITargetType AITT)
    {

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
        switch (AITD)
        {
            case AITargetingDos.Closest:
                {
                    int pos = 0;
                    float dist = float.MaxValue;
                    for (int a = 0; a < cols.Length; a++)
                    {
                        if(Skip(AITT,cols[a].tag)) { continue; }
                        if (Vector2.Distance(transform.position, cols[a].transform.position) <= dist)
                        {
                            dist = Vector2.Distance(transform.position, cols[a].transform.position);
                            pos = a;
                        }
                    }
                    return cols[pos].gameObject;
                }
            case AITargetingDos.Furthest:
                {
                    int pos = 0;
                    float dist = float.MaxValue;
                    for (int a = 0; a < cols.Length; a++)
                    {
                        if (Skip(AITT, cols[a].tag)) { continue; }

                        if (Vector2.Distance(transform.position, cols[a].transform.position) >= dist)
                        {
                            dist = Vector2.Distance(transform.position, cols[a].transform.position);
                            pos = a;
                        }
                    }
                    return cols[pos].gameObject;
                }
            case AITargetingDos.Random:
                {
                    GameObject go = cols[UnityEngine.Random.Range(0, cols.Length)].gameObject;
                    int cap = 0;
                    while(Skip(AITT, go.tag))
                    {
                        go = cols[UnityEngine.Random.Range(0, cols.Length)].gameObject;
                        if (cap > 20)
                        {
                            break;
                        }
                        cap++;
                    }
                    return go;
                }
        }
        return null;
    }
    public bool Skip( AITargetType AITT,string tag)
    {
        switch (AITT)
        {
            case AITargetType.Any:
                return false;
            case AITargetType.Friendly:
                return !FractionManager.IsFriendly(gameObject.tag,tag);
            case AITargetType.Enemy:
                return FractionManager.IsFriendly(gameObject.tag, tag);
        }
        return false;
    }
    IEnumerator tick()
    {
        while (true)
        {
            Think();
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void Think()
    {
        ExecuteNode(brain.mainNode);
    }
    public void ExecuteNodeOutputs(AINode node)
    {
        node.outputs = node.outputs.OrderBy(pet => pet.executionOrder).ToList();
        foreach (AINode ain in node.outputs)
        {
            ExecuteNode(ain);
        }

    }
    public void ExecuteNode(AINode node)
    {
        switch (node.type)
        {
            case AINodeType.Main:
                {
                    ExecuteNodeOutputs(node);
                    break;
                }
            case AINodeType.If:
                {
                    if (CheckNodeStatement(node))
                    {
                        ExecuteNodeOutputs(node);
                    }
                    break;
                }
        }
    }
    public bool CheckNodeStatement(AINode node)
    {
        float outvalue = float.NaN;
        switch (node.ifpart.what)
        {
            case AIWhat.Distance:
                {
                    switch (node.ifpart.whos)
                    {
                        case AIWhos.My:
                            return true;
                        case AIWhos.Target:
                            {
                                outvalue = Vector2.Distance(transform.position, Target.transform.position);
                                break;
                            }
                    }
                    break;
                }
            case AIWhat.HP:
                {
                    switch (node.ifpart.whos)
                    {
                        case AIWhos.My:
                            {
                                outvalue = data.HP;
                                break;
                            }
                        case AIWhos.Target:
                            {
                                /* if(Target is ScriptedEntity)
                                 {
                                     outvalue = ((ScriptedEntity)Target).HP;
                                 }
                                 else
                                 {*/
                                outvalue = Target.HP;
                                // }
                                break;
                            }

                    }
                    break;
                }
            case AIWhat.Energy:
                {
                    switch (node.ifpart.whos)
                    {
                        case AIWhos.My:
                            {
                                outvalue = data.energy;
                                break;
                            }
                        case AIWhos.Target:
                            {
                                if (Target is ScriptedEntity)
                                {
                                    outvalue = ((ScriptedEntity)Target).data.energy;
                                }
                                else
                                {
                                    outvalue = -1;
                                }
                                break;
                            }

                    }
                    break;
                }
            case AIWhat.Speed:
                {
                    switch (node.ifpart.whos)
                    {
                        case AIWhos.My:
                            {
                                outvalue = data.stats.speed;
                                break;
                            }
                        case AIWhos.Target:
                            {
                                if (Target is ScriptedEntity)
                                {
                                    outvalue = ((ScriptedEntity)Target).data.stats.speed;
                                }
                                else
                                {
                                    outvalue = -1;
                                }
                                break;
                            }

                    }
                    break;
                }
        }
        float value = float.NaN;
        switch (node.ifpart.aivalue)
        {
            case AIValue.Normal:
                {
                    value = node.ifpart.value;
                    break;
                }
            case AIValue.Variable:
                {
                    value = -1;
                    break;
                }
        }

        switch (node.ifpart.compare)
        {

            case AICompare.More:
                {
                    return outvalue > value;
                }
            case AICompare.MoreOrEqual:
                {
                    return outvalue >= value;

                }
            case AICompare.Equals:
                {
                    return outvalue == value;

                }
            case AICompare.Less:
                {
                    return outvalue < value;

                }
            case AICompare.LessOrEqual:
                {
                    return outvalue <= value;

                }
        }
        return false;
    }
}
