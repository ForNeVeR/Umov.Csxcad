module Tesla.Csxcad.Primitives

open Tesla.Csxcad.Geometry

type Primitive(priority : int) =
    member __.Priority = priority

type Box(priority : int, p1 : Point, p2 : Point) =
    inherit Primitive(priority)
    member __.P1 = p1
    member __.P2 = p2
