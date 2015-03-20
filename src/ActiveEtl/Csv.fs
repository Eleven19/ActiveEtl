namespace Eleven19.ActiveEtl.Writers
//[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
[<System.Runtime.CompilerServices.Extension>]
[<AutoOpen>]
module Csv =
  open System
  open System.Data
  open System.IO
  open CsvHelper

  type ICsvWriter with
    member this.WriteDataRecordHeader(dataReader:IDataReader) =
      let dbSchema = dataReader.GetSchemaTable()
      dbSchema.Columns |> Seq.cast<DataColumn> |> Seq.iter (fun (col:DataColumn)-> 
        this.WriteField(col.ColumnName)
      )      
      this.NextRecord()

    member this.WriteDataRecordHeader(mappings:DataReaderColumnMappings) =
      mappings.ColumnNames |> Seq.iter (fun colName-> 
        this.WriteField(colName)
      )
      this.NextRecord()

    member this.WriteRecords(dataReader:IDataReader) =
      let columnMappings = DataReaderColumnMappings(dataReader)
      match this.Configuration.HasHeaderRecord with 
      | true -> this.WriteDataRecordHeader columnMappings
      | false -> ()
      
      while dataReader.Read() do
        columnMappings.IterateInOrder (fun ordinal colInfo reader ->
          this.WriteField(colInfo.DataType, reader.GetValue(ordinal))
        )

  [<System.Runtime.CompilerServices.Extension>]
  let ToCsvString (records:seq<_>) =
    use writer = new StringWriter() 
    use csv = new CsvWriter(writer)
    csv.WriteRecords(records);
    writer.ToString()

//  let makeMap (rdr : IDataReader) (schema : DataTable) =
//    schema.Columns |> Seq.cast<DataColumn> |> Seq.map (fun col -> (col.ColumnName, rdr.[col.ColumnName])) |> Map.ofSeq

  [<System.Runtime.CompilerServices.Extension>]
  let WriteDataRecordHeader(writer:ICsvWriter, dataReader) =
    writer.WriteDataRecordHeader dataReader

  [<System.Runtime.CompilerServices.Extension>]
  let WriteRecords(writer:ICsvWriter,dataReader:IDataReader) =
      writer.WriteRecords dataReader

[<System.Runtime.CompilerServices.Extension>]
[<AutoOpen>]
module DataWriter =
  open System
  open System.Data
  open System.IO
  open CsvHelper
  [<System.Runtime.CompilerServices.Extension>]
  let ToCsvString(dataReader:IDataReader) = 
    use sw = new StringWriter()
    use csvWriter = new CsvWriter(sw)
    csvWriter.WriteRecords(dataReader)
    csvWriter.ToString()
