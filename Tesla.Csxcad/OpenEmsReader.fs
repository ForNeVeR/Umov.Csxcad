module Tesla.Csxcad.OpenEmsReader

open System.IO

open FSharp.Data

open Tesla.Csxcad.Base
open Tesla.Csxcad.Geometry
open Tesla.Csxcad.Primitives
open Tesla.Csxcad.Properties

let private parse (openEms : Xml.OpenEms) =
    { ContinousStructure = CsxReader.parse openEms.ContinuousStructure }

let Parse (stream : Stream) : OpenEms =
    let data = Xml.Load stream
    match data.OpenEms with
    | Some openEms -> parse openEms
    | None -> failwithf "Incompatible XML data"
