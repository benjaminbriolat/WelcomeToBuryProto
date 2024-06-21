using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    public void OnHighlighted();
    public void OnUnHighlighted();
    public void OnSelected();
    public void OnDeselect();
}
