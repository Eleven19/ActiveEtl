module Eleven19.ActiveEtl.DataReader
open System
open System.Data

[<System.Runtime.CompilerServices.Extension>]
[<AutoOpen>]
module DataReader =
  open System
  open System.Data
  open System.Collections.Generic

  type DataReaderColumnInfo = {
      ColumnName:string
      Ordinal:int
      DataType:Type
  }

  type IDataReader with
    member this.GetColumnInfo() =
      let dbSchema = this.GetSchemaTable()
      dbSchema.Columns |> Seq.cast<DataColumn> 
      |> Seq.map (fun col ->
        {ColumnName=col.ColumnName; Ordinal=col.Ordinal; DataType=col.DataType}
      )

    member this.GetColumnMapping() =
      let map =  
        this.GetColumnInfo() |> Seq.map (fun info -> (info.ColumnName, info)) |> Map.ofSeq 

      map :> IDictionary<string,DataReaderColumnInfo>

    member this.GetColumnOrdinalMapping() =
      let map =  
        this.GetColumnInfo() |> Seq.map (fun info -> (info.Ordinal, info)) |> Map.ofSeq 

      map :> IDictionary<int,DataReaderColumnInfo>

  type DataReaderColumnMappings(dataReader:IDataReader)=
    let byName = lazy(dataReader.GetColumnMapping())
    let byOrdinal = lazy(dataReader.GetColumnOrdinalMapping())

    member this.LookByName name =
      match (byName.Value.TryGetValue name) with
      | false,_ -> None
      | true,col -> Some col

    member this.LookupByOrdinal ordinal = 
      match (byOrdinal.Value.TryGetValue ordinal) with
      | false,_ -> None
      | true,col -> Some col

    member this.Check(ordinal,onMatch,?onNoMatch) =
      match this.LookupByOrdinal ordinal with
      | None -> 
        match onNoMatch with
        | None -> ()
        | Some action -> action ordinal dataReader
      | Some col -> onMatch ordinal col dataReader

    member this.Match(ordinal,onMatch:Action<int,DataReaderColumnInfo,IDataReader>,?onNoMatch:Action<int,IDataReader>) =
      match this.LookupByOrdinal ordinal with
      | None -> 
        match onNoMatch with
        | None -> ()
        | Some action -> action.Invoke(ordinal, dataReader)
      | Some col -> onMatch.Invoke(ordinal, col, dataReader)


    member this.ColumnOrdinals = byOrdinal.Value.Keys
    member this.ColumnNames = byName.Value.Keys

    member this.IterateInOrder(onColumn) =
      this.ColumnOrdinals |> Seq.iter (fun ordinal -> 
        this.Check(ordinal, onColumn)
      )
