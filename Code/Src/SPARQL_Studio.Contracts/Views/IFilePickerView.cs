﻿// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using System.Collections.Generic;

namespace dk.ModularSystems.Sparql.Contracts.Views
{
    public interface IFilePickerView : IView
    {
        void SetFileList(IEnumerable<String> filePaths);
        void SetFileList(String folderPath, String wildCard);
    }
}
