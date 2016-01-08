module Tesla.Csxcad.Tests.TestData

open System
open System.IO

open Tesla.Csxcad

let Tools = Path.Combine ("..", "..", "..", "Tools")
let ``CSX.xml`` = Path.Combine (Tools, "CSX.xml")
let ``openEMS.xml`` = Path.Combine (Tools, "openEMS.xml")
