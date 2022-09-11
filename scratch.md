##AspnetCore Microservices
##DOCKER
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans


dotnet ef database update
If the above command gives you an error, please follow the below steps.

check path %USERPROFILE%.dotnet\ exists or not

if not then run the below command

dotnet tool install -g dotnet-ef
Now again check the path and set the environment variable for the below path

%USERPROFILE%\.dotnet\tool
Now in cmd, set the path where your database context file is present and then run the below command

dotnet ef database update