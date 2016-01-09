module Tesla.Csxcad.Tests.WriterTests

open System.IO

open Xunit

open Tesla.Csxcad
open Tesla.Csxcad.Tests.TestData

let referenceStructure =
    use stream = new FileStream(``CSX.xml``, FileMode.Open, FileAccess.Read)
    CsxReader.Parse stream

let referenceOpenEms =
    use stream = new FileStream(``openEMS.xml``, FileMode.Open, FileAccess.Read)
    OpenEmsReader.Parse stream

let writeData path writer data =
    use stream = new FileStream (path, FileMode.Create)
    writer (stream, data)

let assertStructuralEquality<'t> (a : 't) (b : 't) =
    // The closest to structural equality in our situation is this:
    let descriptor = sprintf "%A"
    Assert.Equal (descriptor a, descriptor b)

[<Fact>]
let ``CsxWriter should write data successfully`` () =
    let path = Path.GetTempFileName ()
    writeData path CsxWriter.Write referenceStructure
    Assert.NotEqual (0L, FileInfo(path).Length)

[<Fact>]
let ``CsxReader should read the data written by CsxWriter`` () =
    let path = Path.GetTempFileName ()
    writeData path CsxWriter.Write referenceStructure
    use stream = new FileStream(path, FileMode.Open)
    let dataRead = CsxReader.Parse stream

    assertStructuralEquality referenceStructure dataRead

[<Fact>]
let ``OpenEmsWriter should write data successfully`` () =
    let path = Path.GetTempFileName ()
    writeData path OpenEmsWriter.Write referenceOpenEms
    Assert.NotEqual (0L, FileInfo(path).Length)

[<Fact>]
let ``OpenEmsReader should read the data written by OpenEmsWriter`` () =
    let path = Path.GetTempFileName ()
    writeData path OpenEmsWriter.Write referenceOpenEms
    use stream = new FileStream(path, FileMode.Open)
    let dataRead = OpenEmsReader.Parse stream

    assertStructuralEquality referenceOpenEms dataRead
