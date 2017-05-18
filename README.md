# loop-service
Aire Loop Service homework

Issues
- Within the given time period I spent a lot of time setting up the initial project, then dependency injection and mapping

Improvements

- Pass all the applications and select from them which are considered High Risk, then add that collection to the HighRisk collection. Rather than one app at a time.
- Build out the Events api to retrieve the number of high risk applications within a given time period and compare to the threshold for the given time period and then return a Increase High Risk Event if over

Assumptions

I made the following assumptions during the task:

- That the api would already have an authentication system in place
- That there would be a data store for the HighRisk applications, or a way to mark an application as High Risk 
- That LowIncome could be a simple check on the reported income field, where in actual fact it would probably be a more complex calculation based on multiple fields on the application, and may involve and looking up futher data to compare to.

Dev process

- First stage- create an empty api with the two end points for posting apps and getting events
- Second stage - set up dependency injection, mapping for mapping of the models (which separate front end models to domain model), and unit tests
- Third stage - implement logic and storage of HighRiskApplications, return events based on numbers of HighRiskApplications
 
Designing the service
  - EventProcessor - this would take a dependency on IncreaseHighRisk event processor 
  - EventProcessor would use IncreaseHighRisk and process the application.
  - IncreaseHighRisk event processor would have a dependency on LowIncomeRiskFactor
  - IncreaseHighRisk would call LowIncomeRiskFactor.IsHighRisk. It would then use the result to determine whether or not an application should be added to the list of HighRiskApplications
  - LowIncomeRiskFactor, calculate whether the application is high risk based on its logic
  - Logic for LowIncomeRiskFactor, if application Income is below configurable threshold then it is considered LowIncome and therefore return true for IsHighRisk.


The reason for this design is so that each part is repsonsible for a single thing, this ensures that it easier to unit test and it makes it re-usable (other parts may need to know if an application is LowIncome for a difference Event)
Responsibilities

- AppsController - responsible for passing the mapped application to the EventProcessor
- EventProcessor - responsible for calling the events processors (currently only IncreaseHighRisk implemented, but could add in more)
- IncreaseHighRisk - responsible for calling the all risk factors that are required to determine if an application is HighRisk (currently only LowIncomeRiskFactor implemented, but could add in more, i.e. JobTitle)
- LowIncomeRiskFactor - responsible for determining whether or not an application is considered low income.

![alt text](https://github.com/spriggatig/loop-service/raw/master/sequennceone.PNG "sequence diagram")