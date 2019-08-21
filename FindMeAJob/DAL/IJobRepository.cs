using MSAJobFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSAJobFinder.DAL
{
    public interface IJobRepository : IDisposable
    {
        IEnumerable<Jobs> GetJobs();
        Jobs GetJobsByID(int JobId);
        void InsertJob(Jobs job);
        void DeleteJob(int JobId);
        void UpdateJob(Jobs job);
        void Save();
    }
}
