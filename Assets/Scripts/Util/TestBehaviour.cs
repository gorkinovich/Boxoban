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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// This class represents a test behaviour.
/// </summary>
public class TestBehaviour : MonoBehaviour {
    //--------------------------------------------------------------------------------------
    // Methods (Events):
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Initializes the component.
    /// </summary>
    void Start() {
        var core = CoreManager.Instance;
        Action<string> test = (path) => {
            Debug.Log(path);
            var victim = core.LoadXmlFromResources<LevelXml>(path);
            Debug.Log(victim);
        };
        test(CoreManager.BASE_XML_PATH + "TestLevel");
        test(CoreManager.BASE_XML_PATH + "TestLevel2");
	}

    /// <summary>
    /// Updates the component. This is called once per frame.
    /// </summary>
	void Update () {
	}
}
