# Excalibur Change log

## [0.6.8-Beta](https://github.com/xciles/excalibur) (2019-02-24)
[Raw change log by Commits](https://github.com/Xciles/Excalibur/compare/68bd7dd...188951f)

**Features:**
* Added ObjectsForListUpdatedHandler
  * This can be used by the presentations to override the items that will be returned by the business for the presentation to load, instead of overriding the whole ListUpdatedX method.
* Added functionality to clear the presentation and items that are kept in memory.
* Moved some of the Typed implementations from the .Typed namespace to the upper namespace.
* Added IDisposable to some entities that should allow disposing
* Some styling updates
* Added ISharedTextProvider (PR #9)

**Fixed bugs:**
* EncryptedFileStorage plugin will now register correctly even if ProtectedStore has not yet been registered.
* When deleting a collection and using LiteDB as storage provider, we now also shrink the database.

## [0.6.7-Beta](https://github.com/xciles/excalibur) (2018-12-10)
[Raw change log by Commits](https://github.com/Xciles/Excalibur/compare/3d20b65...68bd7dd)

**Features:**
* None

**Fixed bugs:**
* Various changes to the ProtectedStore
  * ProtectedStore will remove the file if we are InitializeFirstTimeAndGenerate
  * ProtecedStore sill try to reset, and ignore if we can't
* EncryptedFileStorageConfig will try to initialize otherwise we'll reset and reinit

## [0.6.3-Beta](https://github.com/xciles/excalibur) (2018-12-10)
[Raw change log by Commits](https://github.com/Xciles/Excalibur/compare/ba80b89...3d20b65)

**Features:**
* Change to the RootChecker to allow overrides of the method. 
  * This change might help with the simulator issues

**Fixed bugs:**
* None!

## [0.6.2-Beta](https://github.com/xciles/excalibur) (2018-12-09)
[Raw change log by Commits](https://github.com/Xciles/Excalibur/compare/f3f98c5...ba80b89)

**Features:**
* Removed the improvement logs, CHANGELOG.md supersedes this

**Fixed bugs:**
* Merged P !4 to fix the Simulator Check in iOS
* Fixed liteDb not clearing or removing the complete storage
* Fixed liteDb not deleting the proper items from the collection

## [0.6.1-Beta](https://github.com/xciles/excalibur) (2018-12-05)
[Raw change log by Commits](https://github.com/Xciles/Excalibur/compare/306e23e8...f3f98c5)

**Features:**
* Added posibilty to delete items that were not returned when the service syncs
  * DeleteNotReturnedItems can be used for this in various business entities
* Added an AfterServiceSyncData (in UpdateFromService) method you can use to do additional checks or deletions before saving
* Added CHANGELOG.md
* CreateDefaultHttpClient in the ServiceBase is now virtual
* Added reset methods to the ConfigurationManager
  * Reset deletes the current configuration without creating (saving) a new one
* Added HasConfiguration to the BaseState
* Added Reset to the BaseSate
  * Reset deletes the current configuration without creating (saving) a new one
* Added NotifyTask (from Mvvm.Async)
* Added AsyncMvxValueConverter

**Examples**
* Started on a Full Forms example

## [0.6.0-Beta](https://github.com/xciles/excalibur) (2018-11-20)
[Raw change log by Commits](https://github.com/Xciles/Excalibur/compare/7a79ecf8...306e23e8)

**Features:**
* Updating MvvmCross to 6.2.2
* Adding Initialize and Terminate for ProtectedStore (mainly for Android)
  * Changes to the configuration manager to support these changes
* Clearing store when attempts fail
* Changes to the LiteDb Connection string handling
  * This can now also be reinitialized
* Removed old nuspec files
* Removing clearStorage because of the new Clear method
* Renaming GetAsync to FirstOrDefault
* Adding clear method to the database providers
* Adding FirstOrDefault to IListBusiness
* Adding find (expression, skip, take) to list business 
* Renaming GetAllAsync to FindAll
* Removing Async postfixes
* Adding lots of comments

**Fixed bugs:**
* SelectedObservable now had async support
* GetObservable now has async support
* Removing service base shared http client
  * Seems to be a problem with Mono. Some scenarios result into strange behaviour. Shared client has been removed and has been changed to a Create Http Instance method.
  * CreateDefaultHttpClient will create a default Http Client with a AutomaticDecompressionHandler.

## [0.5.1-Beta](https://github.com/xciles/excalibur)
[Raw change log by Commits](https://github.com/Xciles/Excalibur/compare/4dac37bd...7a79ecf8)

**Features:**
* Bumping packages to the latest version. (MvvmCross 6.2.1)
* Introduced csproj based nuget package information
* Moving plugins to a different folder (no namespace change)
* Moving various classes arround to support extending
* Added Encrypted storage provider
* Added Encryption and decription helper s based on PCLCrypto
* Added more comments to the various projects
* Added Plugin files to auto load the plugins and dependencies
* Added example encryption project
* Adding HasConfiguration to the configuration manager
* Adding checks to see if the file exists in the encrypted provider
* Adding check for the encrypted config if it has been initialized properly
* Adding Init And validation check to EncryptedConfig
* Moving ProtectedStore the Async, just for making UAP work...
* Removed ExMainThreadDispatcher, this is no longer needed. Just use the MvvmCross one.

**Bugfixes:**
* ObservableObjectBase is now a MvxNotifyPropertyChanged

## [0.5.0-Beta](https://github.com/xciles/excalibur) | 14-11-2018   
[Raw changelog by Commits](https://github.com/Xciles/Excalibur/compare/b3b7804...4dac37bd)

*This version contains **breaking changes***

**Features:**
* Moving away from Xamarin IoC to the MvvmCross IoC provider
* Lots of constructor changes from protected to public to support the MvvmCross IoC
* Adding editorconfig
* Adding LiteDbProvider
* Renaming the File provider (File) to FileStorage
* Introducing IDatabaseProvider and reworked the IObjectStorageProvider
* The Common project is now named Avalon, because of reasons...
* Adding more extensions to Avalon

**Bugfixes:**
* Fixing mapper order error in presentation (Observable Mapper > Selected Mapper)

## [0.4.1-Beta](https://github.com/xciles/excalibur) | 04-11-2018
[Change log by Commits](https://github.com/Xciles/Excalibur/compare/fbed79fd...b3b7804)

**Features:**
* Adding base project
* Changing dependency resolving to use constructor injection instead
* Merging Shared and Cross projects
* Adding Setter for mapping configuration

## [0.3.3-Beta](https://github.com/xciles/excalibur)

**Features:**
* Adding xml documentation
* The SharedClient will now be instantiated with an AutomaticDecompressionHandler
* BaseMapper private _config has been changed to protected Config {get;}


## [0.3.2-Beta](https://github.com/xciles/excalibur)

**Features:**
* Adding more dependencies
* Adding File serializer options

## [0.3.1-Beta](https://github.com/xciles/excalibur)

**Fixed bugs:**
* Removing Excalibur.Shared from the dependency list

## [0.3.0-Beta](https://github.com/xciles/excalibur)

Netstandard 2.0 release!

**Features:**
* Moving to netstandard2.0
* Adding Excalibur Common to this packages for now...
* Changes to make things work again with MvvmCross 6
* Adding some new BaseModel Classes

## [0.2.0-Beta](https://github.com/xciles/excalibur)

This package contains Excalibur.Cross, Excalibur.Shared and the Excalibur.Providers.File. At a later point different nuget packages will be introduced.

**Features:**
* Changing BasePresentation to BaseListPresentation.
* Changing IPresentation to IListPresentation.
* ToUnixTimeInSeconds fix to return actual seconds instead of millisecondes.
* Adding base implementation for various Excalibur items (Int).
* Introducing PCL for shared project (instead of shared project)
* Typed implementation to custom files and folder
* Moving Providers to Storage (-.Provider)
* MessageBase moved to Excalibur.Shared.Utils
* Renamed BPresentation to BasePresentation
* Bumping package versions (To latest version, 5th Jan*)
* Upgrading MvvmCross to 5.6.3
* Adding comments
* Incluing Inhericdoc
      
## [0.1.1-Beta](https://github.com/xciles/excalibur)

**Features:**
* Adding code comments
* Changing few method signatures to include Async
* Renaming few methods to better represent what they are doing
      
## [0.1.0-Beta](https://github.com/xciles/excalibur) 

**Features:**
* First beta release

SetSelectedObservable > Async
Async modifiers > be gone
Business > expose first or default things
liteDb > encryption password
business > delete all from storage





      0.5.0-beta
      Breaking changes release. 
      Change list coming soon.
      0.4.1-beta
      todo
      0.4.0-beta
      todo
      0.3.3-beta
      0.3.2-beta
