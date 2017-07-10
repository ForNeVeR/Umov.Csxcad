module UmovCsxcad.Tests.TestData

open System.IO

let private solutionRoot = Path.Combine ("..", "..", "..")

let Tools = Path.Combine (solutionRoot, "Tools")
let ``CSX.xml`` = Path.Combine (Tools, "CSX.xml")
let ``openEMS.xml`` = Path.Combine (Tools, "openEMS.xml")

let ``Test-Data`` = Path.Combine (solutionRoot, "Test-Data")
let ``Test-Data/CSX.xml`` = Path.Combine (``Test-Data``, "CSX.xml")
let ``Test-Data/issue-16-test-data.csx`` = Path.Combine (``Test-Data``, "issue-16-test-data.csx")
let ``Test-Data/XML.xml`` = Path.Combine (``Test-Data``, "XML.xml")
