using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineSolution
{
    public class GetCategoriesResponse
    {

        public List<string> data { get; set; }

        public string success { get; set; }

        public string token { get; set; }

        public string message { get; set; }
    }
}
