using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;

[DisallowMultipleComponent]
public class ClientNetworkAnimator : NetworkAnimator
{

    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }


}
