﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Interfaces
{
    public interface IImageReader
    {
        public Image.Image Read(string path);
    }
}
