using System;
using System.Collections.Generic;
using System.Text;
using Flyer.Extensions.Sequence;

namespace Flyer.Extensions
{
    /// <summary>
    /// sequence helper
    /// </summary>
    public class SequenceHelper
    {
        public static IDWorker worker;
        /// <summary>
        /// 创建worker
        /// </summary>
        public static void Create()
        {
            Create(0, 0, 0);
        }
        /// <summary>
        /// 创建worker
        /// </summary>
        /// <param name="workId"></param>
        /// <param name="dataCenterId"></param>
        /// <param name="sequence"></param>
        public static void Create(long workId, long dataCenterId, long sequence = 0L)
        {
            if (worker == null)
            {
                lock (typeof(SequenceHelper))
                {
                    if (worker == null)
                    {
                        worker = new IDWorker(workId, dataCenterId, sequence);
                    }
                }
            }
        }
        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public static long NextId()
        {
            if (worker == null)
            {
                Create();
            }
            return worker.NextId();
        }
    }
}
