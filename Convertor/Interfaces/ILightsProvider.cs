using System;
using System.Collections.Generic;
using System.Text;
using Converter.Models;

namespace Converter.Interfaces
{
    public interface ILightsProvider
    {
        public List<Light> GetLights();
    }
}
