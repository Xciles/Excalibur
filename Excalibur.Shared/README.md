Lol markdown file w/o markdown!


Todo: 
	- Business
		- No more DomainEntity
		- Layer for logic and services
		- Viewmodel communicates with the business classes
			- Viewmodel lists can talk with statics
			- Viewmodel for specific instance of a domain class will get a specific business instance
	- Presentation 
		- Observables
		- Bind observables by ref
		- Pub/sub on Business
	- ViewModel
		- Observable ref bindings instead of lining property in Presentation
	- Domain
	- Services


	- DI for Business and Presentation 
	- Business still has static methods to provide for overal operations
	- ViewModel has a ref to a Business 