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

        // Constructor that allows setting the image path upon object creation
        public Image(string imagePath)
        {
            _imagePath = imagePath;
        }

        // Default constructor
        public Image()
        {
            _imagePath = ""; // Set a default image path if necessary
        }

        // You can add methods here that work with the image if needed
    }
}
