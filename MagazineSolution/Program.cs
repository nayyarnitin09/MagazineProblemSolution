using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineSolution
{
    public class Program
    {
        // Magazine ids not unique across categories 
        //dict of ctaegory along with list of magazine ids is formed
        static void Main(string[] args)
        {

            var subscribers = MagazineAPICalls.GetSubscribers();
            var categoriesMagazines = MagazineAPICalls.GetCategoriesAndMagazines();
            ListOfSubscribers resultset = new ListOfSubscribers();
            //looping through subscribers
            foreach(var subscriber in subscribers.data)
            {
                var insertFlag = true;
                foreach(var dictObj in categoriesMagazines)
                {
                    if (dictObj.Value.Intersect(subscriber.magazineIds).Count() == 0)
                    {
                        insertFlag = false;
                        break;
                    }
                    else
                        // Magazine ids are not unique across categories 
                        subscriber.magazineIds.RemoveAll(x => dictObj.Value.Contains(x));
                }
                if (insertFlag)
                    resultset.subscribers.Add(subscriber.id);
            }

            Console.WriteLine(MagazineAPICalls.IsAnswerCorrect(resultset));
        }
    }
}
