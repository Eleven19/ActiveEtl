[<AutoOpen>]
module Collections.Eleven19.ActiveEtl
open System 

type Property<'T,'P> = ('T -> 'P) * ('P -> 'T -> 'T) 

let internal Property<'T,'P> getter setter : Property<'T,'P> = (getter,setter)


