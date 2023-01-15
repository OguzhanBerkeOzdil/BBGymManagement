using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace BBGymManagement.Helpers
{
    public class Trigger
    {
        private async Task<IScheduler> Run()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = await schedFact.GetScheduler();
            if (!sched.IsStarted)
                await sched.Start();

            return sched;
        }

        public async void QuestTrigger()
        {
            IScheduler sched = await Run();

            IJobDetail quest = JobBuilder.Create<Quest>().WithIdentity("Quest", null).Build();

            ISimpleTrigger TriggerQuest = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("Quest").StartAt(DateTime.UtcNow).WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever()).Build();
            await sched.ScheduleJob(quest, TriggerQuest);
        }
    }
}