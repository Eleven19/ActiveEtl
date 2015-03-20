[<AutoOpen>]
module Eleven19.ActiveEtl.Utils.Dates

open System
open System.Text
open System.Globalization
//open FSharp.RegexProvider

[<Literal>]
let DateRegexLiteral = "(?<Date>(?<year>\d\d\d\d)-?(?<month>[01]\d)-?(?<day>[0123]\d))"
  
//type DateRegex = Regex<DateRegexLiteral>

let tryParseDate dateStr =
  match DateTime.TryParseExact(dateStr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None) with
  | false, _ ->
    match DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None) with
    | false, _ -> 
      match DateTime.TryParse(dateStr) with      
      | false,_ -> dateStr |> sprintf "The date must be a valid date. The given value of '%s' was not valid." |> failwith
      | true, date -> date
    | true, date -> date
  | true,date -> date


type DateRange = {
  StartDate:DateTime
  EndDate:DateTime option
}

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module DateRange =
  open System
  let defaults =
    {StartDate= DateTime.Today; EndDate= Some DateTime.Today}

  let createRange startDate endDate =
    {StartDate=startDate; EndDate=endDate;}

  let create startDate =