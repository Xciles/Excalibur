using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excalibur.Cross
{
    public interface IBase<T>
    {
        Task<T> GetAll(T t);
         Task Save(T t);
        Task Publish(T t);
    }

    public interface IParticipantInstance
    {
        Task Like();
        Task ReinitializeRouteFor();
    }

    public class BaseC<T> : IBase<T>, IParticipantInstance
    {
        public Task Like()
        {
            throw new NotImplementedException();
        }

        public Task ReinitializeRouteFor()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAll(T t)
        {
            throw new NotImplementedException();
        }

        public Task Save(T t)
        {
            throw new NotImplementedException();
        }

        public Task Publish(T t)
        {
            throw new NotImplementedException();
        }

    }


    public class DomainParticipant
    {
        
    }

    public class ErgensAnders
    {
        public ErgensAnders()
        {
            IBase<DomainParticipant> bla;
            IParticipantInstance bli;
        }
    }


}
