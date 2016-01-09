module Tesla.Csxcad.CsxWriter

open System.IO

open Tesla.Csxcad.Base
open Tesla.Csxcad.Geometry
open Tesla.Csxcad.Primitives
open Tesla.Csxcad.Properties

let private typedProperty<'t when 't :> Property> (properties : Property seq) =
    properties
    |> Seq.filter (fun p -> p :? 't)
    |> Seq.cast<'t>
    |> Seq.exactlyOne

let private makeVector (v : Vector) = sprintf "%f,%f,%f" v.X v.Y v.Z

let private makeP (p : Point) =
    Xml.P (x = string p.X, y = string p.Y, z = string p.Z)

let private makeP2 (p : Point) =
    Xml.P2 (x = string p.X, y = string p.Y, z = string p.Z)

let private makeBox (box : Box) =
    Xml.Box (string box.Priority,
             makeP box.P1,
             makeP2 box.P2)

let private makePrimitives (primitives : Primitive array) =
    let boxes =
        primitives
        |> Seq.cast<Box>
        |> Seq.map makeBox
        |> Seq.toArray
    Xml.Primitives (boxes)

let private makeExcitation (excitation : Excitation) =
    Xml.Excitation (name = Some excitation.Name,
                    ``type`` = Utils.enumCode excitation.Type,
                    excite = Some (makeVector excitation.Vector),
                    f0 = None,
                    primitives = Some (makePrimitives excitation.Zone))

let private makeDumpBox (dumpBox : DumpBox) =
    Xml.DumpBox (name = dumpBox.Name,
                 dumpMode = Utils.enumCode dumpBox.DumpMode,
                 primitives = makePrimitives dumpBox.Zone)

let private makeMetal (metal : Metal) =
    Xml.Metal (name = metal.Name,
               primitives = makePrimitives metal.Zone)

let private makeProperties (properties : Property array) =
    let excitation = makeExcitation (typedProperty<Excitation> properties)
    let dumpBox = makeDumpBox (typedProperty<DumpBox> properties)
    let metals =
        properties
        |> Seq.filter (fun p -> not (p :? Excitation || p :? DumpBox))
        |> Seq.cast<Metal>
        |> Seq.map makeMetal
        |> Seq.toArray
    Xml.Properties (excitation = excitation, dumpBox = dumpBox, metals = metals)

let private makeGridLines lines =
    lines
    |> Seq.map string
    |> String.concat ","

let private makeRectilinearGrid (grid : RectilinearGrid) =
    Xml.RectilinearGrid (deltaUnit = string grid.Delta,
                         coordSystem = Utils.enumCode grid.CoordinateSystem,
                         xLines = makeGridLines grid.XLines,
                         yLines = makeGridLines grid.YLines,
                         zLines = makeGridLines grid.ZLines)

let internal makeContinuousStructure (cs : ContinuousStructure) =
    Xml.ContinuousStructure (coordSystem = Utils.enumCode cs.CoordinateSystem,
                             properties = makeProperties cs.Properties,
                             rectilinearGrid = makeRectilinearGrid cs.Grid)

let Write (stream : Stream, cs : ContinuousStructure) : unit =
    let structure = makeContinuousStructure cs
    structure.XElement.Save stream
