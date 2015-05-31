//******************************************************************************************
// Boxoban: A retro pixel-art puzzle 2D game made with Unity 4.5
// Copyright (C) 2015  Gorka Suárez García
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//******************************************************************************************
using System;
using UnityEngine;

/// <summary>
/// This class represents a timer inside the game.
/// </summary>
public class Timer {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region bool Enable

    /// <summary>
    /// Gets or sets the is enable flag of the timer.
    /// </summary>
    public bool Enable { get; set; }

    #endregion

    #region float currentTime

    /// <summary>
    /// The current accumulated time of the timer.
    /// </summary>
    private float currentTime;

    #endregion

    #region float Interval

    /// <summary>
    /// Gets or sets the interval of the timer.
    /// </summary>
    public float Interval { get; set; }

    #endregion

    #region Action<Timer> OnAction

    /// <summary>
    /// Gets or sets the timer action method.
    /// </summary>
    public Action<Timer> OnAction { get; set; }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    #region void Update()

    /// <summary>
    /// Updates the inner timer logic.
    /// </summary>
    public void Update() {
        if (Enable && OnAction != null) {
            currentTime += Time.deltaTime;
            while (currentTime >= Interval) {
                currentTime -= Interval;
                OnAction(this);
            }
        }
    }

    #endregion

    #region void ResetTime()

    /// <summary>
    /// Resets the current time.
    /// </summary>
    public void ResetTime() {
        currentTime = 0.0f;
    }

    #endregion

    #region void SetAndEnable(float, Action<Timer>, bool)

    /// <summary>
    /// Sets and enable the timer.
    /// </summary>
    /// <param name="interval">The interval of the timer.</param>
    /// <param name="action">The timer action method.</param>
    /// <param name="resetTime">If the time must be reseted or not.</param>
    public void SetAndEnable(float interval, Action<Timer> action, bool resetTime = true) {
        if (action == null) {
            throw new Exception("The action of the timer can't be null.");
        }
        OnAction = action;
        Interval = interval;
        Enable = true;
        if (resetTime) ResetTime();
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    #region Timer()

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    public Timer() {
        Enable = false;
        currentTime = 0.0f;
        Interval = 0.0f;
        OnAction = null;
    }

    #endregion
}
