using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class GetPatient : GAction
{
    private GameObject _resource;
    public override bool PrePerform()
    {
        target = GWorld.Instance.RemovePatient();
        if (target == null)
        {
            return false;
        }

        _resource = GWorld.Instance.RemoveCubicle();
        if (_resource != null)
        {
            inventory.AddItem(_resource);
        }
        else
        {
            GWorld.Instance.AddPatient(target);
            target = null;
            return false;
        }
        
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if (target)
        {
            target.GetComponent<GAgent>().inventory.AddItem(_resource);
        }
        return true;
    }
}
