using IPStackExternalService.Exceptions;
using IPStackExternalService.Models;
using IPStackWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace IPStackWebAPI.Infrastructure
{
    public class IPDetailsBuffer : IIPDetailsBuffer
    {
        private Queue<IPDetailsExtDTO> _ipDetailsQueue;
        public IPDetailsBuffer()
        {
        }

        /// <summary>
        /// Initliaze Queue Of Buffer with a collection of IPDetails
        /// </summary>
        /// <param name="iPDetailsExtDTOs"></param>
        public void Initliaze(IEnumerable<IPDetailsExtDTO> iPDetailsExtDTOs) =>
            _ipDetailsQueue = new Queue<IPDetailsExtDTO>(iPDetailsExtDTOs);


        /// <summary>
        /// Add an IPDetail item to Queue of Buffer
        /// </summary>
        /// <param name="iPDetailsExtDTOs"></param>
        public void Put(IPDetailsExtDTO iPDetailsExtDTOs)
        {
            if (_ipDetailsQueue != null)
                _ipDetailsQueue.Enqueue(iPDetailsExtDTOs);
            else
                throw new IPDetailsUpdateRequestInternalError("Buffer is not initialized");
        }

        /// <summary>
        /// Take specific number of times from queue dequeueing them at the same time too
        /// </summary>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public IEnumerable<IPDetailsExtDTO> Take(int chunkSize)
        {
            if (_ipDetailsQueue != null)
                return _ipDetailsQueue.DequeueChunk(chunkSize);
            else
                throw new IPDetailsUpdateRequestInternalError("Buffer is not initialized");
        }

        /// <summary>
        /// Returns the total remained items inside the queue
        /// </summary>
        /// <returns></returns>
        public bool HasItems()
        {
            if (_ipDetailsQueue != null)
                return _ipDetailsQueue.Count() > 0;
            else
                throw new IPDetailsUpdateRequestInternalError("Buffer is not initialized");
        }

    }
}
