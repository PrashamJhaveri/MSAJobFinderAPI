using MSAJobFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MSAJobFinder.Helper
{
    public class JobHelper
    {
        public static void testProgram()
        {
            Console.WriteLine(GetJobInfo("Software", "auckland"));
            Console.ReadLine();
        }


        public static List<Jobs> getFromSeek(String searchJob, String location)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.seek.co.nz/" + searchJob + "-jobs/in-" + location);

            var jobTitle = doc.DocumentNode.SelectNodes("//a[@data-automation='jobTitle']").ToList();
            var description = doc.DocumentNode.SelectNodes("//div[@data-search-sol-meta]").ToList();
            var JobURL = doc.DocumentNode.SelectNodes("//a[@class='_2iNL7wI']").ToList();
            var company = doc.DocumentNode.SelectNodes("//span[@class='_3FrNV7v _3PZrylH E6m4BZb']").ToList();

            String pattern = "href=.+ c";

            List<Jobs> jobs = new List<Jobs>();
            for (int i = 0; i < description.Count; i++)
            {
                Match match = Regex.Match(JobURL[i].OuterHtml, pattern);
                if (match.ToString() != "")
                {

                    String jobLink = "https://www.seek.co.nz" + match;
                    jobLink = CleanSeekLink(jobLink);

                    String JobTitleTEST = jobTitle[i].InnerHtml.ToString();
                    String WebUrlTEST = jobLink;
                    String CompanyNameTEST = description[i].FirstChild.ChildNodes[4].InnerText;
                    String LocationTEST = location;
                    String JobDescriptionTEST = description[i].FirstChild.ChildNodes[7].InnerText;

                    Jobs job = new Jobs
                    {
                        JobTitle = JobTitleTEST,
                        WebUrl = jobLink,
                        CompanyName = CompanyNameTEST,
                        Location = location,
                        JobDescription = JobDescriptionTEST,
                        ImageUrl = "blank",
                        Applied = false,
                    };
                    jobs.Add(job);
                }
            }
            return jobs;
        }

        public static List<Jobs> GetJobInfo(String searchJob, String location){
            List<Jobs> jobs = new List<Jobs>();
            jobs = getFromSeek(searchJob, location);
            return jobs;
        }




        private static String CleanSeekLink(String webURL)
        {
            webURL = webURL.Replace("href=\"", "");
            webURL = webURL.Replace("\" c", "");
            return webURL;
        }
    }
}
