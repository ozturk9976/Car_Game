using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface IMechanic
{
    void IDoMove();
    void ISetCarAsChild(Transform carTransform);
}
