using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePelletsCommand : ICommand
{
    Vector3 position;
    Transform item;

    public PlacePelletsCommand(Vector3 position, Transform item)
    {
        this.position = position;
        this.item = item;
    }

    public void Execute()
    {
        PelletPlacer.PlacePellet(item);
    }

    public void Undo()
    {
        PelletPlacer.RemovePellet(position);
    }
}
