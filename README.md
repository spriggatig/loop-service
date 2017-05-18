# loop-service
## Aire Loop Service homework

### Setup instructions

- Download the Github repo <https://github.com/spriggatig/loop-service> and extract to C:\Users\{youraccount}}\Documents\Visual Studio 2017\Projects into a new folder.
- Download Visual Studio Community Edition <https://www.visualstudio.com/downloads>
- When the installer prompts you to choose a workload, select ASP.net and web development. This will run through the download and installation.
- Launch Visual Studio and select open project/solution and selct C:\Users\{youraccount}}\Documents\Visual Studio 2017\Projects\{loop-service-master\Aire.LoopService.sln.
- It may prompt to install missing features - choose install (you may need to close VS2017), the workloads will be selected, then click Modify.
- After it has completed installing open the solution as before.
- From the Build menu, select Build solution
- From the Debug menu, select Start debugging. This should open up a browser window and load the default page.
- Using Postman <https://www.getpostman.com/> submit the following GET request http://localhost:58155/api/events, initally this should return and empty array.
- The using the test data in the root folder (see below) submit a POST request to http://localhost:58155/api/apps with the json body as raw and the Content-Type set to application/json.

### Testing the data

I used <http://www.csvjson.com/csv2json> to convert the some of the stream csv data to Json and then submitted via Postman

There are a couple of json files in the root folder which I used for testing:
 - HighRiskApplications.json contains only applications that have low income (annual_inc field) (37 applications)
 - LowRiskApplications.json contains only applications that have high income (37 applications)
 - applications.json contains a large number of applications (985) that have a mixture of incomes and empty values (this was generated from the stream file)

In order to generate a INCREASE_HIGH_RISK event I submitted the LowRiskApplication json and then checked the events api to see if the event was present (It should not be). 
Then I posted the HighRiskApplications json and checked the events api. There should be an event for this. As shown below.
```
[
  {
    "event_name": "INCRESE_HIGH_RISK",
    "event_description": "Total application count: 74, high risk application count 37, 50% of 4% threshold",
    "event_datetime": "2017-05-18T15:23:24.5795759+01:00"
  }
]
```

### Issues
- Within the given time period I spent a lot of time setting up the initial project, then dependency injection and mapping
- I did not complete the homework within the timescale of 6 hours, having already spent that amount of time (1hr planning Tuesday, 2.5hrs Tuesday evening, 3hrs Wednesday evening, 1hr this morning)
- I did spend a few more hours getting something working and writing the missing unit tests because I wanted to handover a completed solution

### Improvements that could made

- Pass all the applications and select from them which are considered High Risk, then add that collection to the HighRisk collection. Rather than one app at a time.
- Build out the LowIncome logic so it is based on more values, rather than the one.
- Build out the Events api to retrieve the number of high risk applications within a given time period and compare to the threshold for the given time period and then return a Increase High Risk Event if over
- Make the threshold of high risk applications percentage configurable. It is set to 4% in the Events Api. If more than 4% of total applications are high risk then it returns a INCRESE_HIGH_RISK event. 

### Assumptions

I made the following assumptions during the task:

- That the api would already have an authentication system in place
- That there would be a data store for the HighRisk applications, or a way to mark an application as High Risk 
- That LowIncome could be a simple check on the reported income field, where in actual fact it would probably be a more complex calculation based on multiple fields on the application, and may involve and looking up futher data to compare to.

### Dev process

- First stage- create an empty api with the two end points for posting apps and getting events
- Second stage - set up dependency injection, mapping for mapping of the models (which separate front end models to domain model), and unit tests
- Third stage - implement logic and storage of HighRiskApplications, return event based on numbers of HighRiskApplications
 
#### Designing the service

  - EventProcessor - this takes a dependency on IncreaseHighRisk event processor 
  - EventProcessor would use IncreaseHighRisk and process the application. This is where further events (NEW_GEO etc.) would be checked against the applications.
  - IncreaseHighRisk event processor has a dependency on LowIncomeRiskFactor
  - IncreaseHighRisk would call LowIncomeRiskFactor.IsHighRisk. It would then use the result to determine whether or not an application should be added to the list of HighRiskApplications. There could be other RiskFactors implemented here such as JobTitle check.
  - LowIncomeRiskFactor, calculate whether the application is high risk based on its logic
  - Logic for LowIncomeRiskFactor, if application Income is below configurable threshold then it is considered LowIncome and therefore return true for IsHighRisk.

The reason for this design is so that each part is repsonsible for a single thing, this ensures that it easier to unit test and it makes it re-usable (other parts may need to know if an application is LowIncome for a difference Event)
Responsibilities

- AppsController - responsible for passing the mapped application to the EventProcessor
- EventProcessor - responsible for calling the events processors (currently only IncreaseHighRisk implemented, but could add in more such as NewGeo or ExaggeratedIncome)
- IncreaseHighRisk - responsible for calling the all risk factors that are required to determine if an application is HighRisk (currently only LowIncomeRiskFactor implemented, but could add in more, i.e. JobTitle)
- LowIncomeRiskFactor - responsible for determining whether or not an application is considered low income.

![alt text](https://github.com/spriggatig/loop-service/raw/master/sequennceone.PNG "sequence diagram")



