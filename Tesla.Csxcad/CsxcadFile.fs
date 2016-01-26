module Tesla.Csxcad.CsxcadFile

open System.IO

type Type =
    | Unknown = 0
    | Cxscad = 1
    | OpenEms = 2

let DetermineType (stream : Stream) : Type =
    let r = Xml.Load stream
    match r.ContinuousStructure, r.OpenEms with
    | Some _, _ -> Type.Cxscad
    | _, Some _ -> Type.OpenEms
    | _ -> Type.Unknown
