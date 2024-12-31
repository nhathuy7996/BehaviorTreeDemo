using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorLib{
    [Serializable]
public class SelectorNode : BehaviorNode
{
    private List<BehaviorNode> _children;

    public SelectorNode(params BehaviorNode[] children)
    {
        _children = new List<BehaviorNode>(children);
    }

    public override bool Execute()
    {
        foreach (var child in _children)
        {
            if (child.Execute())
            {
                return true; 
            }
        }
        return false; 
    }
}
}
