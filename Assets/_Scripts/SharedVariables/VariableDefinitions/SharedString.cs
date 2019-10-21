using System;
using UnityEngine;

[Serializable,CreateAssetMenu(fileName = "SharedString", menuName = "Shared Variables/String", order = 1)]
public class SharedString : SharedVariable<string> { }