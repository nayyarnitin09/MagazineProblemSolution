using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineSolution
{
    public class GetAnswerResponse
    {
        public Answer data { get; set; }
        public string success { get; set; }

        public string token { get; set; }

        public string message { get; set; }
    }

    public class Answer
    {
        public string totalTime { get; set; }
        public bool answerCorrect { get; set; }

        public List<string> shouldBe { get; set; }

    }
}
