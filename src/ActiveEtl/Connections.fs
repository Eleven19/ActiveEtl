namespace Eleven19.ActiveEtl.Data
open System
open System.Data

type ConnectionSettings = {
  ConnectionName:string
  ConnectionString:string
  ProviderName:string
}

type ConnectionInfo = {
  ConnectionName:string
  ConnectionString:string
  ProviderName:string
}

[<AutoOpen>]
module ConnectionManager =
  open System
  type ConnectionSettingsRegistry = {
    Settings: ConnectionSettings list
  }

  type ConnectionSettingsRegistry with
    member this.TryFind name = 
      this.Settings |> List.tryFind (fun x -> 
        if String.Equals(name, x.ConnectionName) then true else false
      )

  let createRegistry settings =
    {Settings=settings}
    
type ConnectionInfoProvider = unit -> seq<ConnectionInfo>

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Connection =
  open System
  open System.Data

  let tryGetConnectionInfo (connectionInfos:seq<ConnectionInfo>) connectionName =
    let connectionInfoMap = 
      connectionInfos|> Seq.map (fun c->c.ConnectionName,c) |> Map.ofSeq

    connectionInfoMap |> Map.tryFind connectionName      