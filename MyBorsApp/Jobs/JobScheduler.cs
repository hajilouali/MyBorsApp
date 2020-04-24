using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace MyBorsApp.Jobs
{
    public class JobScheduler
    {
        public async static void Start()
        {
            
            IJobDetail job = JobBuilder.Create<OrderContractJob>()
                .WithIdentity("job1")
                
                .Build();
                
            
            ITrigger trigger = TriggerBuilder.Create()
                .ForJob(job)
                .WithIdentity("trigger1")
                
                .StartNow()
            
                 .WithCronSchedule("* * 10-16 ? * SUN,MON,TUE,WED,SAT *")
                .Build();
            
            
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sc = await sf.GetScheduler();

            await sc.ScheduleJob(job, trigger);
            
            await sc.Start();
        }
    }
    public class JobSchedulerPan
    {
        public async static void Start()
        {
            IJobDetail job = JobBuilder.Create<OrderContractJob>()
                .WithIdentity("job11")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .ForJob(job)
                .WithIdentity("trigger11")
                .StartNow()
                .WithCronSchedule("* * 10-15 ? * THU *")
                .Build();

            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sc = await sf.GetScheduler();
            await sc.ScheduleJob(job, trigger);

            await sc.Start();
        }
    }

    public class logerContract
    {
        public async static void starterLog()
        {
            IJobDetail JobLog = JobBuilder.Create<Contractlog>()
                .WithIdentity("key")
                .Build();
            ITrigger triggerJob = TriggerBuilder.Create()
                .WithIdentity("triger")
                .StartAt(DateBuilder.DateOf(10, 30, 0))
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                .EndAt(DateBuilder.DateOf(17,0,0))
                .Build();
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sc = await sf.GetScheduler();
            await sc.ScheduleJob(JobLog, triggerJob);

            await sc.Start();
        }
    }
}