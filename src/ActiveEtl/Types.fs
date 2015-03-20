namespace Eleven19.ActiveEtl
open System
open Chessie.ErrorHandling

type ActivityParameter = 
  {
    ParameterName:string
    ParameterType: Type
    Value: obj}
    
  static member Create name (value:'a) = 
    {ParameterName=name; Value=value; ParameterType=typeof<'a>;}

type ActivityParamaterMapping = Map<string,ActivityParameter>

type ActivityContext ={
  Parameters: ActivityParamaterMapping
}

type Activity =
  abstract member Run: context:ActivityContext -> Async<Result<ActivityContext, string>>    

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Activity = 
  open System
