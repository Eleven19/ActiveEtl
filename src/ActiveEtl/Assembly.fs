namespace Eleven19.ActiveEtl.Reflection
open System
open System.Runtime.CompilerServices

[<Extension>]
[<AutoOpen>]
module Assembly =
  open System
  open System.Reflection
  open System.IO

  type Assembly with
    member this.ReadEmbeddedResourceText(resourceName) =      
      use stream = this.GetManifestResourceStream(resourceName)
      use reader = new StreamReader(stream)
      reader.ReadToEnd()

    member this.ReadEmbeddedResourceText( ``type``, resourceName) =
      use stream = this.GetManifestResourceStream(``type``, resourceName)
      use reader = new StreamReader(stream)
      reader.ReadToEnd()

  [<Extension>]
  let ReadEmbeddedResourceText (assembly:Assembly) resourceName =
    assembly.ReadEmbeddedResourceText(resourceName)

  [<Extension>]
  let ReadEmbeddedResourceTextInNamespace (assembly:Assembly) typ  resourceName =
    assembly.ReadEmbeddedResourceText(typ, resourceName)


