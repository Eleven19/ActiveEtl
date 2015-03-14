namespace Eleven19.ActiveEtl

type ConnectionSettings = {
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
    