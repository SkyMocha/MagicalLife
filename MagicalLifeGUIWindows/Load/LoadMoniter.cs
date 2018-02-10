﻿using MagicalLifeAPI.Universal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeGUIWindows.Load
{
    /// <summary>
    /// Used to handle calculating how many jobs to do, and how many completed.
    /// </summary>
    public class LoadMoniter
    {
        /// <summary>
        /// The number of jobs total to do.
        /// </summary>
        private int JobCount = 0;

        /// <summary>
        /// The number of jobs that have been completed.
        /// </summary>
        private int JobsCompleted = 0;

        /// <summary>
        /// The jobs that are still queued up.
        /// </summary>
        private Queue<IGameLoader> Jobs { get; set; } = new Queue<IGameLoader>();

        /// <summary>
        /// The message to display.
        /// </summary>
        public string message { get; set; } = "Calculating Jobs";

        /// <summary>
        /// Adds a job to the queue.
        /// </summary>
        /// <param name="job"></param>
        public void AddJob(IGameLoader job)
        {
            this.JobCount += job.GetTotalOperations();
            this.Jobs.Enqueue(job);
        }

        /// <summary>
        /// Begins execution of the jobs.
        /// </summary>
        public void ExecuteJobs()
        {
            IGameLoader job;
            int progress;
            while (this.Jobs.Count > 0)
            {
                progress = 0;
                job = this.Jobs.Dequeue();
                job.InitialStartup(ref progress);
                this.JobsCompleted += job.GetTotalOperations();
                this.UpdateMessage();
            }
        }

        private void UpdateMessage()
        {
            this.message = this.JobsCompleted.ToString() + "out of " + this.JobCount.ToString() + " jobs completed";
        }
    }
}
