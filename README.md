# Excalibur
## Info

This project tries to bring structure for developers that are using MvvmCross and Xamarin to develop their mobile applications. 

Excalibur achieves this by using Generics for a lot of boiler plating that you tend to write when developing using Xamarin. Excalibur currently has accelerators for Xamarin Native and Xamarin.Forms (MvvmCross based).
Custom implementations for other frameworks can be developed using the Excalibur.Shared.

## Idea 

The idea behind this project is to provide Xamarin developers a proper architecture based on .NET MS reference architecture. 
Excalibur uses an "extended" MVVM (Model View ViewModel) Pattern. 

All references and instances are usually DI'ed.

\<Image to be added>

### Description

#### Business
* Manages data within the app 
  * Saving
  * Retrieving
  * Updating
* Manages Application state
* Uses services to retrieve and update data
* Publish (pub/sub) entities when updated

#### Domain
* Just a tiny entity used for saving data and service communication

#### Observable
* Observable entities
* A Domain entity with extended properties and Notifications
* Used by ViewModels and Views instead of Domain

#### Presentation
* Manages observable instances
* Used for sharing the same observable instances between ViewModels
* Subscribes (pub/sub) on certain entities
* Uses Mappers to map Models to Observables

#### Service
* Communication with a back-end
* Used by the Business
* May have its own entity for data transfer (instead of Domain)

#### View Models
* Manages navigation
* Manages language bindings
* Views use the ViewModel Observables for binding

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