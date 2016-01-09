module Tesla.Csxcad.Tests.WriterTests

open System.IO

open Xunit

open Tesla.Csxcad
open Tesla.Csxcad.Tests.TestData

let referenceStructure =
    use stream = new FileStream(``CSX.xml``, FileMode.Open, FileAccess.Read)
    CsxReader.Parse stream

let writeReferenceData path =
    use stream = new FileStream (path, FileMode.Create)
    CsxWriter.Write (stream, referenceStructure)

let assertStructuralEquality<'t> (a : 't) (b : 't) =
    // The closest to structural equality in our situation is this:
    let descriptor = sprintf "%A"
    Assert.Equal (descriptor a, descriptor b)

[<Fact>]
let ``CsxWriter should write data successfully`` () =
    let path = Path.GetTempFileName ()
    writeReferenceData path
    Assert.NotEqual (0L, FileInfo(path).Length)

[<Fact>]
let ``CsxReader should be able to read the data written by CsxWriter`` () =
    let path = Path.GetTempFileName ()
    writeReferenceData path
    use stream = new FileStream(``CSX.xml``, FileMode.Open)
    let dataRead = CsxReader.Parse stream

    assertStructuralEquality referenceStructure dataRead
