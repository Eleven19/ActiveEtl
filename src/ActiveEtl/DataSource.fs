namespace Eleven19.ActiveEtl

module DataSource =
  open System
  open Eleven19.ActiveEtl

  module Ado =
    open System
    open System.Data
    open System.Data.Common
    open Eleven19.ActiveEtl

    let CreateDbConnection (connectionSettings : ConnectionSettings) =
      let factory = DbProviderFactories.GetFactory(connectionSettings.ProviderName)      
      let connection = factory.CreateConnection()
      connection.ConnectionString <- connectionSettings.ConnectionString
      connection :> IDbConnection

    let TryMakeDbConnection (registry:ConnectionSettingsRegistry) name =
      match (registry.TryFind name) with
      | None -> None
      | Some settings -> settings |> CreateDbConnection |> Option.Some