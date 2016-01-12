module Tesla.Csxcad.Tests.TestData

open System.IO

let private SolutionRoot = Path.Combine ("..", "..", "..")

let Tools = Path.Combine (SolutionRoot, "Tools")
let ``CSX.xml`` = Path.Combine (Tools, "CSX.xml")
let ``openEMS.xml`` = Path.Combine (Tools, "openEMS.xml")

let ``Test-Data`` = Path.Combine (SolutionRoot, "Test-Data")
let ``Test-Data/CSX.xml`` = Path.Combine (``Test-Data``, "CSX.xml")
