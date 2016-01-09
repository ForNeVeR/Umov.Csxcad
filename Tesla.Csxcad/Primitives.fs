namespace Tesla.Csxcad.Primitives

open Tesla.Csxcad.Geometry

[<AbstractClass>]
type Primitive(priority : int) =
    member __.Priority = priority
    override __.ToString () = sprintf "Primitive{ Priority = %d }" priority

type Box(priority : int, p1 : Point, p2 : Point) =
    inherit Primitive(priority)
    member __.P1 = p1
    member __.P2 = p2
    override __.ToString () =
        base.ToString () + (sprintf ".Box{ P1 = %A, P2 = %A }" p1 p2)
