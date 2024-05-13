using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaLibrairie
{
    public class Image
    {
        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        public Image(string imagePath)
        {
            _imagePath = imagePath;
        }

        public Image()
        {
            _imagePath = ""; 
        }

    }
}
