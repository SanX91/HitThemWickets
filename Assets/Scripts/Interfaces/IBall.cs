﻿using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// An interface for basic ball functionalities.
    /// </summary>
    public interface IBall
    {
        void SetSpin(float spin);
        void SetPosition(Vector3 position);
        void SetBounce(float amount);
        void Bowl();
        void Initialize();
    } 
}