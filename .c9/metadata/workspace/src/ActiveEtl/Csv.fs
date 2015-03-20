{"filter":false,"title":"Csv.fs","tooltip":"/src/ActiveEtl/Csv.fs","undoManager":{"mark":18,"position":18,"stack":[[{"group":"doc","deltas":[{"start":{"row":0,"column":0},"end":{"row":66,"column":0},"action":"insert","lines":["","//[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]","[<System.Runtime.CompilerServices.Extension>]","[<AutoOpen>]","module Csv =","  open System","  open System.Data","  open System.IO","  open CsvHelper","","  type ICsvWriter with","    member this.WriteDataRecordHeader(dataReader:IDataReader) =","      let dbSchema = dataReader.GetSchemaTable()","      dbSchema.Columns |> Seq.cast<DataColumn> |> Seq.iter (fun (col:DataColumn)-> ","        this.WriteField(col.ColumnName)","      )      ","      this.NextRecord()","","    member this.WriteDataRecordHeader(mappings:DataReaderColumnMappings) =","      mappings.ColumnNames |> Seq.iter (fun colName-> ","        this.WriteField(colName)","      )","      this.NextRecord()","","    member this.WriteRecords(dataReader:IDataReader) =","      let columnMappings = DataReaderColumnMappings(dataReader)","      match this.Configuration.HasHeaderRecord with ","      | true -> this.WriteDataRecordHeader columnMappings","      | false -> ()","      ","      while dataReader.Read() do","        columnMappings.IterateInOrder (fun ordinal colInfo reader ->","          this.WriteField(colInfo.DataType, reader.GetValue(ordinal))","        )","","  [<System.Runtime.CompilerServices.Extension>]","  let ToCsvString (records:seq<_>) =","    use writer = new StringWriter() ","    use csv = new CsvWriter(writer)","    csv.WriteRecords(records);","    writer.ToString()","","//  let makeMap (rdr : IDataReader) (schema : DataTable) =","//    schema.Columns |> Seq.cast<DataColumn> |> Seq.map (fun col -> (col.ColumnName, rdr.[col.ColumnName])) |> Map.ofSeq","","  [<System.Runtime.CompilerServices.Extension>]","  let WriteDataRecordHeader(writer:ICsvWriter, dataReader) =","    writer.WriteDataRecordHeader dataReader","","  [<System.Runtime.CompilerServices.Extension>]","  let WriteRecords(writer:ICsvWriter,dataReader:IDataReader) =","      writer.WriteRecords dataReader","","[<System.Runtime.CompilerServices.Extension>]","[<AutoOpen>]","module DataWriter =","  open System","  open System.Data","  open System.IO","  open CsvHelper","  [<System.Runtime.CompilerServices.Extension>]","  let ToCsvString(dataReader:IDataReader) = ","    use sw = new StringWriter()","    use csvWriter = new CsvWriter(sw)","    csvWriter.WriteRecords(dataReader)","    csvWriter.ToString()",""]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":0},"end":{"row":0,"column":25},"action":"insert","lines":["module Eleven19.ActiveEtl"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":0},"end":{"row":0,"column":6},"action":"remove","lines":["module"]},{"start":{"row":0,"column":0},"end":{"row":0,"column":1},"action":"insert","lines":["n"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":1},"end":{"row":0,"column":2},"action":"insert","lines":["a"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":2},"end":{"row":0,"column":3},"action":"insert","lines":["m"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":3},"end":{"row":0,"column":4},"action":"insert","lines":["e"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":4},"end":{"row":0,"column":5},"action":"insert","lines":["s"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":5},"end":{"row":0,"column":6},"action":"insert","lines":["p"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":6},"end":{"row":0,"column":7},"action":"insert","lines":["a"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":7},"end":{"row":0,"column":8},"action":"insert","lines":["c"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":8},"end":{"row":0,"column":9},"action":"insert","lines":["e"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":28},"end":{"row":0,"column":29},"action":"insert","lines":["."]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":29},"end":{"row":0,"column":30},"action":"insert","lines":["W"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":30},"end":{"row":0,"column":31},"action":"insert","lines":["r"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":31},"end":{"row":0,"column":32},"action":"insert","lines":["i"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":32},"end":{"row":0,"column":33},"action":"insert","lines":["t"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":33},"end":{"row":0,"column":34},"action":"insert","lines":["e"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":34},"end":{"row":0,"column":35},"action":"insert","lines":["r"]}]}],[{"group":"doc","deltas":[{"start":{"row":0,"column":35},"end":{"row":0,"column":36},"action":"insert","lines":["s"]}]}]]},"ace":{"folds":[],"scrolltop":0,"scrollleft":0,"selection":{"start":{"row":0,"column":10},"end":{"row":0,"column":36},"isBackwards":true},"options":{"guessTabSize":true,"useWrapMode":false,"wrapToView":true},"firstLineState":0},"timestamp":1426885784370,"hash":"88800c355b28f7d8a420209e16e59102c7a1f856"}