# FundApps - Courier Kata

The Courier Kata is a library to calculate the cost of delivery of parcels with different size and weight. Also it is considering the delivery options such as express delivery.

## How to Build

- Install .Net Core SDK 3.1 to be installed. It can be 
  - Either downloaded from [Microsoft Website](https://dotnet.microsoft.com/download/dotnet-core/3.1)
  - Or install using Chocolatey ``` choco install dotnetcore ```
- Clone the repo
- Run command ``` dotnet build ``` in repository root folder
- Run command ``` dotnet test ``` in repository root folder


## Extensibilities & Future Changes

- Add a Money type to handle currency rather than decimal values
- Read the configiration for parcel types and discounts from a repository such as database or json file
- The solution can be re-engineered to use Strategy/Command like pattern to hanle calculation of cost and discount in more generic way
- Create a CLI/Web interface for interactive calculation
