# upliving


## Publish command
dotnet publish --configuration Release --self-contained --runtime linux-x64

## Swagger in Creator
autorest --input-file=http://localhost:5005/swagger/v1/swagger.json --csharp --output-folder=Services --namespace=Creator.Services
java -jar .\swagger-codegen-cli-2.4.1.jar generate -i http://localhost:5005/swagger/v1/swagger.json -l typescript-angular -o ../ClientApp/src/api
