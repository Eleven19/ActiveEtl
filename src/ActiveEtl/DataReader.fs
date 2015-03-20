module Eleven19.ActiveEtl.DataReader
open System
open System.Data

let rec withReader (reader:IDataReader) f =
  let rec withRecord record = f record  =
    match reader.Read() with
    | false -> ()
    | true ->
      let record = reader :> IDataRecord
      f record
      withRecord  record
  


