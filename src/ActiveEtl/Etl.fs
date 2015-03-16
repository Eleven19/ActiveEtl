namespace Eleven19.ActiveEtl

type ActiveEtlConfig = 
  {ConnectionSettings:ConnectionSettings list}

module Etl =
  open System

  let run (config:ActiveEtlConfig) = ()