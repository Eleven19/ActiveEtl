namespace Eleven19.ActiveEtl.Data
open System
open System.Data
open System.Runtime.CompilerServices

[<Extension>]
[<AutoOpen>]
module DataReader =
  open System
  open System.Data
  open System.Collections.Generic
  open FSharp.Data.DynamicSql

  type DataReaderColumnInfo = {
      ColumnName:string
      Ordinal:int
      DataType:Type
  }

  type IDataReader with
    member this.GetColumnInfo() =
      let dbSchema = this.GetSchemaTable()
      dbSchema.Rows |> Seq.cast<DataRow> 
      |> Seq.map (fun row ->
        {ColumnName=row.Field("ColumnName") ; Ordinal=row.Field("ColumnOrdinal"); DataType=row.Field("DataType")}
      )

    member this.GetColumnMapping() =
      let map =  
        this.GetColumnInfo() |> Seq.map (fun info -> (info.ColumnName, info)) |> Map.ofSeq 

      map :> IDictionary<string,DataReaderColumnInfo>

    member this.GetColumnOrdinalMapping() =
      let map =  
        this.GetColumnInfo() |> Seq.map (fun info -> (info.Ordinal, info)) |> Map.ofSeq 

      map :> IDictionary<int,DataReaderColumnInfo>

  [<Extension>]
  let GetColumnInfo (dataReader:IDataReader) = dataReader.GetColumnInfo()

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