using Converter.ImageBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.ImageConcrete
{
    public class ImagePpm : Image
    {
        public override Header Header { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string Path { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override Color[][] Bitmap { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
