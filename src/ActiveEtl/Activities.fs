namespace Eleven19.ActiveEtl

module Activities =
  let createActivity impl = //context:ActivityContext -> Async<Result<ActivityContext, string>>
    {new Activity with
       member this.Run ctx = ctx |> impl}

  let executeSql connection sql = ()  

