namespace Eleven19.ActiveEtl
open Eleven19.ActiveEtl.Data

type ActiveEtlConfig = 
  {
    ConnectionSettings:ConnectionSettings list
  }

module Etl =
  open System

  let DefaultEtlConfig = {
    ConnectionSettings= []

  }

  let run (config:ActiveEtlConfig) = ()

  let configureAndRun (configure: ActiveEtlConfig -> ActiveEtlConfig) = 
    DefaultEtlConfig |> configure |> run