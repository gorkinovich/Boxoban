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
using SokobanTools.Logic;

namespace SokobanTools {
    /// <summary>
    /// This static class represents debug utility operations.
    /// </summary>
    public static class Debug {
        /// <summary>
        /// Shows some information inside the debug console.
        /// </summary>
        /// <param name="e">The exception to show.</param>
        public static void Log(Exception e) {
            if (e != null) {
                var msg = "Message: " + e.Message + "\n\n" +
                          "Source: " + e.Source + "\n\n" +
                          "StackTrace: " + e.StackTrace + "\n\n" +
                          "TargetSite: " + e.TargetSite.Name + "\n\n" +
                          "HelpLink: " + e.HelpLink + "\n";
                Console.WriteLine();
                Shared.ShowMessageInBox("               ERROR                ", '*', '*');
                Console.WriteLine();
                Console.WriteLine(msg);
                Console.WriteLine();
                if (e.InnerException != null) {
                    Log(e.InnerException);
                }
            }
        }
    }
}
