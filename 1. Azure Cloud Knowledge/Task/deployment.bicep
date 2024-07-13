param location string = 'northeurope'
param appName string = 'bicepTestWebApp'
param sqlServerName string = 'bicepTestSqlServer'
param sqlDbName string = 'bicepTestSqlDb'
param sqlAdminLogin string
param sqlAdminPassword string

//Create an App Service Plan
resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appName
  location: location
  properties: {
    reserved: true //Linux-based
  }
  sku: {
    name: 'F1' //Free tier
  }
}

//Create a Web App
resource webApp 'Microsoft.Web/sites@2020-06-01' = {
  name: appName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
  }
}

//Create a SQL Server
resource sqlServer 'Microsoft.Sql/servers@2019-06-01-preview' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: sqlAdminLogin
    administratorLoginPassword: sqlAdminPassword
    version: '12.0'
  }
}

//Create a SQL Database
resource sqlDatabase 'Microsoft.Sql/servers/databases@2019-06-01-preview' = {
  name: '${sqlServer.name}/${sqlDbName}'
  location: location
  properties: {
    edition: 'Basic'
  }
}
