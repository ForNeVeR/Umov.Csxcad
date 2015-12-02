module Tesla.Csxcad.Reader

open System
open System.IO

open FSharp.Data

open Tesla.Csxcad.Geometry

type CSXCAD = XmlProvider<"..\Tools\CSX.xml", InferTypesFromValues = false>

let private excitationType =
    function
    | "0" -> Soft
    | "1" -> Hard
    | other -> failwithf "Unknown excitation type: %s" other
    
let private vector : double[] -> Vector =
    function
    | [| x; y; z |] -> { X = x; Y = y; Z = z }
    | other -> failwithf "Invalid vector components: %A" other

let private point x y z : Point =
    {
        X = x
        Y = y
        Z = z
    }

let numbers (s : string) =
        s.Split '.'
        |> Array.map Double.Parse

let private box (b : CSXCAD.Box) =
    {
        Corner1 = point (double b.P.X) (double b.P.Y) (double b.P.Z)
        Corner2 = point (double b.P2.X) (double b.P2.Y) (double b.P2.Z)
    }

let private source (excitation : CSXCAD.Excitation) =
    let ``type`` = excitationType excitation.Type
    let direction = vector <| numbers excitation.Excite
    let body = box excitation.Primitives.Box
    {
        Name = excitation.Name
        Type = ``type``
        Direction = direction
        Location = body
    }

let private experimentSpace (grid : CSXCAD.RectilinearGrid) =

    let x = numbers grid.XLines
    let y = numbers grid.YLines
    let z = numbers grid.ZLines
    {
        Location =
            {
                Corner1 = point (Array.min x) (Array.min y) (Array.min z)
                Corner2 = point (Array.max x) (Array.max y) (Array.max z)
            }
        Sources = []
    }

let private experiment (definition : CSXCAD.ContinuousStructure) =
    let excitation = definition.Properties.Excitation
    let grid = definition.RectilinearGrid
    let source = source excitation
    let experiment = experimentSpace grid
    { experiment with Sources = [ source ] }

let read (stream : Stream) : Async<Experiment> =
    async {
        do! Async.SwitchToThreadPool ()
        let definition = CSXCAD.Load stream
        return experiment definition
    }
