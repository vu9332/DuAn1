using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ISkill 
{
    void ExecuteSkill(InputAction.CallbackContext context);
}