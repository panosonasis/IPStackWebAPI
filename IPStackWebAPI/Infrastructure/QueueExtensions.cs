using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPStackWebAPI.Infrastructure
{
    public static class QueueExtensions
    {
        /// <summary>
        /// Queue extension for removing the first N items of the specified queue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<T> DequeueChunk<T>(this Queue<T> queue, int chunkSize)
        {
            for (int i = 0; i < chunkSize && queue.Count > 0; i++)
            {
                yield return queue.Dequeue();
            }
        }
    }
}
