# Excalibur
## Info

This project tries to bring structure for developers that are using MvvmCross and Xamarin to develop their mobile applications. 

Excalbur achieves this by using Generics for a lot of boilerplating that you tend to write when developing using Xamarin. 

Basic idea 

## Some examples

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