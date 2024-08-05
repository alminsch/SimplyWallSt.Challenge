Hi Simply Wall St engineers

Here are a few remarks about my solution:
* I have chosen to implement this API in an onion architecture style project. Onion architecture helps with maintainability and testability of the codebase and it does generally lead to clean code as long as dependency boundaries are respected. It does however, as you can see in this project, also lead to quite a lot of files and mappings and it is definitely not the holy grail of software architecture. I could just as well have implemented this API in a vertical slice or other architecture styles, each with their own trade offs of course.
* I have decided to split the reading of the database into 2 different services / repositories for company data and share prices. My reasoning for this is, since loading the share prices is explicitly optional and therefore company data can be used without share price data, I felt it would be more appropriate to separate those concerns into their own read-services and have a main **CompanyService** orchestrate them. If share price data was not optional, I would probably have left it with one single **CompanyReadService** however.
* In c#, folders usually represent namespaces. Personally, I like to have namespaces organised by the convention _Feature.Subfeature.Subsubfeature_ etc. That's why in my solution you can find a **'Companies'** folder which contains a **'Read'** folder. This means we are operating in the feature **'Companies'** and subfeature **'Read'**. In a real wold setting there would be many more subfeatures contained in the feature **'Companies'**.
* UnitTests I generally write with the AAA pattern using a private Setup class that handles all test internals so that the test methods are very concise and easily read- and understandable. For test method names I adhere to the convention *MethodUnderTest_Scenario_ExpectedResult*.

To run the solution navigate to the Companies.Host folder and run the command
> `dotnet run --launch-profile https`

You might once need to run the following command as well
> `dotnet dev-certs https --trust`

You should see an output similar to *"Now listening on: https://localhost:XXXX"*

To test the api you can use the swagger interface at *https://localhost:XXXX/swagger/index.html*


Looking forward to hearing your feedback!

Thanks,  
Alex