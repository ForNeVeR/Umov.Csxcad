module Tesla.Csxcad.Tests.CxscadFileTests

open System.IO

open Xunit

open Tesla.Csxcad
open Tesla.Csxcad.Tests.TestData

[<Fact>]
let ``CSXCAD file should be categorized as CSXCAD`` () =
    use stream = new FileStream(``CSX.xml``, FileMode.Open, FileAccess.Read)
    let fileType = CsxcadFile.DetermineType stream
    Assert.Equal (CsxcadFile.Type.Cxscad, fileType)

[<Fact>]
let ``OpenEMS file should be categorized as OpenEMS`` () =
    use stream = new FileStream(``openEMS.xml``, FileMode.Open, FileAccess.Read)
    let fileType = CsxcadFile.DetermineType stream
    Assert.Equal (CsxcadFile.Type.OpenEms, fileType)

[<Fact>]
let ``Random XML file should be categorized as Unknown`` () =
    use stream = new FileStream(``Test-Data/XML.xml``, FileMode.Open, FileAccess.Read)
    let fileType = CsxcadFile.DetermineType stream
    Assert.Equal (CsxcadFile.Type.Unknown, fileType)
