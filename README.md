# Excalibur
Excalibur

Some examples

```C#
public class Participant : BaseListBusiness<int, ParticipantDomain, IServiceBase<IList<ParticipantDomain>>>
{

}


public class ParticipantDomain : StorageDomain
{

}

public class Friend : BaseListBusiness<int, FriendDomain, IFriendService>
{

}


public class FriendDomain : StorageDomain
{

}

public interface IFriendService : IServiceBase<IList<FriendDomain>>
{

}

public class Bla : IObjectStorageProvider<int, FriendDomain>
{
    public Task StoreRange(IList<FriendDomain> objectsToStore)
    {
        throw new System.NotImplementedException();
    }

    public Task<IList<FriendDomain>> GetRange()
    {
        throw new System.NotImplementedException();
    }

    public Task<FriendDomain> Get(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> AddOrUpdate(FriendDomain objectToStore)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new System.NotImplementedException();
    }
}
```