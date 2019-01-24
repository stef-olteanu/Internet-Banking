using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace PCBanking.Helpers
{
    public class FAQHelper
    {

        public struct FAQ
        {
            public string Hint;

            public string Description;
        }

        private List<FAQ> questions = new List<FAQ>();

        public static FAQHelper Instance { get; set; } = new FAQHelper();

        public List<FAQ> Questions { get { return questions; } }

        private FAQHelper()
        {
            try
            {
                using (StreamReader reader = new StreamReader(ConfigurationManager.AppSettings["PathFAQ"]))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        questions.Add(new FAQ()
                        {
                            Hint = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[0],
                            Description = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[1]
                        });

                    }
                }
            }
            catch
            {
                questions.Add(new FAQ()
                {
                    Hint = "Failed to get FAQ",
                    Description = "The file which contains FAQ failed to get open"
                });
            }
        }


    }
}
