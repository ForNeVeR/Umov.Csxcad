module Umov.Csxcad.CsxWriter

open System.IO

open Umov.Csxcad.Base
open Umov.Csxcad.Geometry
open Umov.Csxcad.Primitives
open Umov.Csxcad.Properties

let private zeroVector = "0,0,0"
let private identityVector = "1,1,1"

let private defaultMaterialProperty =
    Xml.Property (epsilon = identityVector,
                  mue = identityVector,
                  kappa = zeroVector,
                  sigma = zeroVector,
                  density = "0")

let private defaultMaterialWeight =
    Xml.Weight (epsilon = identityVector,
                mue = identityVector,
                kappa = identityVector,
                sigma = identityVector,
                density = "1")

let private defaultFillColor =
    Xml.FillColor (r = "41", g = "35", b = "190", a = "123")

let private defaultEdgeColor =
    Xml.EdgeColor (r = "41", g = "35", b = "190", a = "123")

let private typedProperties<'t when 't :> Property> (properties : Property seq) =
    properties
    |> Seq.filter (fun p -> p :? 't)
    |> Seq.cast<'t>

let private typedProperty<'t when 't :> Property> (properties : Property seq) =
    typedProperties properties
    |> Seq.tryHead

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
               id = None,
               primitives = makePrimitives metal.Zone,
               fillColor = None,
               edgeColor = None)

let private makeMaterial (material : Material) =
    Xml.Material (id = null,
                  name = material.Name,
                  isotropy = "1",
                  fillColor = defaultFillColor,
                  edgeColor = defaultEdgeColor,
                  primitives = makePrimitives material.Zone,
                  property = defaultMaterialProperty,
                  weight = defaultMaterialWeight)

let private makeProperties (properties : Property []) =
    let dumpBox = Option.map makeDumpBox (typedProperty properties)
    let excitations =
        typedProperties properties
        |> Seq.map makeExcitation
        |> Seq.toArray

    let metals =
        properties
        |> Utils.ofType<Metal>
        |> Seq.map makeMetal
        |> Seq.toArray
    let materials =
        properties
        |> Utils.ofType<Material>
        |> Seq.map makeMaterial
        |> Seq.toArray
    Xml.Properties (excitations = excitations,
                    dumpBox = dumpBox,
                    metals = metals,
                    materials = materials)

let private makeGridLines lines =
    lines
    |> Seq.map string
    |> String.concat ","

let private makeXLines (lines : double[]) = Xml.XLines (qty = Some (string lines.Length), value = makeGridLines lines)
let private makeYLines (lines : double[]) = Xml.YLines (qty = Some (string lines.Length), value = makeGridLines lines)
let private makeZLines (lines : double[]) = Xml.ZLines (qty = Some (string lines.Length), value = makeGridLines lines)

let private makeRectilinearGrid (grid : RectilinearGrid) =
    Xml.RectilinearGrid (deltaUnit = string grid.Delta,
                         coordSystem = Some (Utils.enumCode grid.CoordinateSystem),
                         xLines = makeXLines grid.XLines,
                         yLines = makeYLines grid.YLines,
                         zLines = makeZLines grid.ZLines)

let internal makeContinuousStructure (cs : ContinuousStructure) =
    Xml.ContinuousStructure (coordSystem = Utils.enumCode cs.CoordinateSystem,
                             properties = makeProperties cs.Properties,
                             rectilinearGrid = makeRectilinearGrid cs.Grid,
                             backgroundMaterial = None,
                             parameterSet = None)

let Write (stream : Stream, cs : ContinuousStructure) : unit =
    let structure = makeContinuousStructure cs
    structure.XElement.Save stream
