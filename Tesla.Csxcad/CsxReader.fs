module Tesla.Csxcad.CsxReader

open System.IO

open FSharp.Data

open Tesla.Csxcad.Base
open Tesla.Csxcad.Geometry
open Tesla.Csxcad.Primitives
open Tesla.Csxcad.Properties

type private CSXCAD = XmlProvider<"..\Tools\CSX.xml", Global = true, InferTypesFromValues = false>

let private processP1 (p1 : CSXCAD.P) : Point =
    {
        X = double p1.X
        Y = double p1.Y
        Z = double p1.Z
    }

let private processP2 (p2 : CSXCAD.P2) : Point =
    {
        X = double p2.X
        Y = double p2.Y
        Z = double p2.Z
    }

let private processBox (box : CSXCAD.Box) =
    Box (int box.Priority, processP1 box.P, processP2 box.P2)

let private processPrimitives (primitives : CSXCAD.Primitives)
                              : Primitive array =
    Array.map (fun b -> upcast processBox b) primitives.Boxes

let private processDumpType (dumpBox : CSXCAD.DumpBox) =
    DumpType.EFieldTimeDomain // TODO: Parse dump type.

let private processDumpMode (dumpBox : CSXCAD.DumpBox) =
    Utils.parseEnum dumpBox.DumpMode

let private processDumpBox (dumpBox : CSXCAD.DumpBox) : Property =
    upcast DumpBox (dumpBox.Name,
                    processPrimitives dumpBox.Primitives,
                    processDumpType dumpBox,
                    processDumpMode dumpBox)

let private processExcitationType = Utils.parseEnum

let private processExcite (excite : string) =
    let components = excite.Split ','
    {
        X = double components.[0]
        Y = double components.[1]
        Z = double components.[2]
    }

let private processExcitation (excitation : CSXCAD.Excitation) : Property =
    upcast Excitation (excitation.Name,
                       processPrimitives excitation.Primitives,
                       processExcitationType excitation.Type,
                       processExcite excitation.Excite)

let private processMetal (metal : CSXCAD.Metal) : Property =
    upcast Metal (metal.Name, processPrimitives metal.Primitives)

let private processProperties (properties : CSXCAD.Properties) =
    Seq.concat [
        Seq.singleton <| processDumpBox properties.DumpBox
        Seq.singleton <| processExcitation properties.Excitation
        Seq.map processMetal properties.Metals
    ]

let private processGridLines (lines : string) =
    lines.Split ','
    |> Seq.map double

let private processRectilinearGrid (grid : CSXCAD.RectilinearGrid) =
    {
        Delta = double grid.DeltaUnit
        XLines = processGridLines grid.XLines
        YLines = processGridLines grid.YLines
        ZLines = processGridLines grid.ZLines
    }

let private processContinousStructure (structure : CSXCAD.ContinuousStructure) =
    {
        Properties = processProperties structure.Properties
        Grid = processRectilinearGrid structure.RectilinearGrid
    }

let Parse (stream : Stream) : ContinuousStructure =
    let structure = CSXCAD.Load stream
    processContinousStructure structure
