using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineSolution
{
    public class GetSubscribersResponse
    {
        public List<Subscriber> data { get; set; }

        public string success { get; set; }

        public string token { get; set; }

        public string message { get; set; }

    }

    public class Subscriber
    {

        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<int> magazineIds { get; set; }
    }

   
}
